using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;     
    private float shakeTimer = 0;
    [SerializeField] float shakeTime;
    private float shakeAmplitude;
    Action<KeyValuePair<string, object>> ShakeCameraPerformDelegate;
    Action<KeyValuePair<string, object>> MoveStaticCameraDelegate;
    
    private void Awake() {
        if (instance == null) {
            instance = this;
        }

        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start() {
        ShakeCameraPerformDelegate = (pram) => ShakeCameraPerform(3.5f);
        MoveStaticCameraDelegate = (param) => {
            if(param.Key == null) return;
            if(!param.Key.Equals("position")) return;
            transform.position = (Vector3)param.Value;
        };

        Obsever.AddListener(EventID.Player_HURT, ShakeCameraPerformDelegate);
        Obsever.AddListener(EventID.Tutorial_ColliderMoveCamera_OnTrigger, MoveStaticCameraDelegate);

        cinemachineVirtualCamera.Follow = PlayerController.instance.transform;
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.Player_HURT, ShakeCameraPerformDelegate);
        Obsever.RemoveListener(EventID.Tutorial_ColliderMoveCamera_OnTrigger, MoveStaticCameraDelegate);
    }

    private void Update() {
        ResetShakeCamera();
    }

    public void ShakeCameraPerform(float _shakeAmplitude){
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = _shakeAmplitude;
        shakeAmplitude = _shakeAmplitude;
        shakeTimer = shakeTime;
    }

    public void ResetShakeCamera(){
        if(shakeTimer >= 0){
            shakeTimer -= Time.deltaTime;
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = (shakeTimer/shakeTime)*shakeAmplitude;
            if(shakeTimer < 0){
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
            }
        }
    }
}
