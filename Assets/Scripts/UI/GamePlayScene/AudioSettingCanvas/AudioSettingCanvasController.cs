using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingCanvasController : MonoBehaviour
{
    Action<KeyValuePair<string, object>> PopUpAudioSettingCanvasDelegate;
    Action<KeyValuePair<string, object>> PopOffAudioSettingCanvasDelegate;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;

    private void Start() {
        PopUpAudioSettingCanvasDelegate = (param) => {
            PopUpAudioSettingCanvas();
        };

        PopOffAudioSettingCanvasDelegate = (param) => {
            PopOffAudioSettingCanvas();
        };

        Obsever.AddListener(EventID.UI_AudioSettingButton_OnClick, PopUpAudioSettingCanvasDelegate);
        Obsever.AddListener(EventID.UI_AudioSettingToPauseGameButton_OnClick, PopOffAudioSettingCanvasDelegate);
        Obsever.AddListener(EventID.UI_ReturnToMenuSceneButton_OnClick, PopOffAudioSettingCanvasDelegate);

        InvokeExtensionCode.Invoke(SceneGameManager.instance, () => gameObject.transform.GetChild(0).gameObject.SetActive(false), Time.unscaledDeltaTime);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.UI_AudioSettingButton_OnClick, PopUpAudioSettingCanvasDelegate);
        Obsever.RemoveListener(EventID.UI_AudioSettingToPauseGameButton_OnClick, PopOffAudioSettingCanvasDelegate);
        Obsever.RemoveListener(EventID.UI_ReturnToMenuSceneButton_OnClick, PopOffAudioSettingCanvasDelegate);
    }

    private void OnEnable() {
        float musicVolume;
        float sfxVolume;

        if(audioMixer.GetFloat("MusicVolume", out musicVolume)){
            musicVolumeSlider.value = Mathf.Pow(10, musicVolume/20);
        }

        if(audioMixer.GetFloat("SFXVolume", out sfxVolume)){
            sfxVolumeSlider.value = Mathf.Pow(10, sfxVolume/20);
        }
    }

    void PopUpAudioSettingCanvas(){
        Obsever.PostEvent(EventID.UI_AudioSettingCanvas_PopUpAudioSettingCanvas, new KeyValuePair<string, object>(null, null));
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    void PopOffAudioSettingCanvas(){
        if(!gameObject.transform.GetChild(0).gameObject.activeInHierarchy) return;
        StartCoroutine(PopOffAudioSettingCanvasCourotine());
    }

    IEnumerator PopOffAudioSettingCanvasCourotine(){
        gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<Animator>().SetTrigger("PopOff");
        yield return new WaitForSecondsRealtime(0.7f);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void SetMusicVolume(){
        float volume = musicVolumeSlider.value;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(){
        float volume = sfxVolumeSlider.value;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
}
