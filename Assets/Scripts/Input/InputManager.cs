using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInputAction playerInputAction;
    public static InputManager instance;

    Action<KeyValuePair<string, object>> SwitchToMapPauseGameCanvasDelegate;
    Action<KeyValuePair<string, object>> SwitchToMapPlayerDelegate;
    Action<KeyValuePair<string, object>> SwitchToMapLevelUpDelegate;
    Action<KeyValuePair<string, object>> SwitchToMapTimelineDelegate;


    private void Awake() {
        int inputManagerCount = FindObjectsOfType<InputManager>().Length;
        if (inputManagerCount > 1) {
            Destroy(this.gameObject);
        }
        if(instance == null){
            instance = this;
        }

        playerInputAction = new PlayerInputAction();
        playerInputAction.Enable();
    }

    private void Start() {
        SwitchToMapPauseGameCanvasDelegate = (param) => SwitchToMapPauseGameCanvas();
        SwitchToMapPlayerDelegate = (param)=> SwitchToMapPlayer();
        SwitchToMapTimelineDelegate = (param)=> SwitchToMapTimeline();
        SwitchToMapLevelUpDelegate = (param)=> SwitchToMapLevelUp();

        Obsever.AddListener(EventID.Player_LEVELUP, SwitchToMapLevelUpDelegate);
        Obsever.AddListener(EventID.UI_PauseGameCanvas_PopUpPauseGameCanvas, SwitchToMapPauseGameCanvasDelegate);
        Obsever.AddListener(EventID.SCENE_ReturnToGameScene, SwitchToMapPlayerDelegate);
        Obsever.AddListener(EventID.UI_AudioSettingCanvas_PopUpAudioSettingCanvas, SwitchToMapLevelUpDelegate);
        Obsever.AddListener(EventID.SCENE_OnStart, SwitchToMapTimelineDelegate);
        Obsever.AddListener(EventID.SCENE_OnCompleteIntro, SwitchToMapPlayerDelegate);
        Obsever.AddListener(EventID.SCENE_EndGame, SwitchToMapLevelUpDelegate);
        Obsever.AddListener(EventID.UI_TutorialCanvas_PopUpTutorialCanvas, SwitchToMapLevelUpDelegate);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.Player_LEVELUP, SwitchToMapLevelUpDelegate);
        Obsever.RemoveListener(EventID.UI_PauseGameCanvas_PopUpPauseGameCanvas, SwitchToMapPauseGameCanvasDelegate);
        Obsever.RemoveListener(EventID.SCENE_ReturnToGameScene, SwitchToMapPlayerDelegate);
        Obsever.RemoveListener(EventID.UI_AudioSettingCanvas_PopUpAudioSettingCanvas, SwitchToMapLevelUpDelegate);
        Obsever.RemoveListener(EventID.SCENE_OnStart, SwitchToMapTimelineDelegate);
        Obsever.RemoveListener(EventID.SCENE_OnCompleteIntro, SwitchToMapPlayerDelegate);
        Obsever.RemoveListener(EventID.SCENE_EndGame, SwitchToMapLevelUpDelegate);
        Obsever.RemoveListener(EventID.UI_TutorialCanvas_PopUpTutorialCanvas, SwitchToMapLevelUpDelegate);
    }

    //Movement
    public Vector2 GetInputMovement(){
        Vector2 input = playerInputAction.Player.Movement.ReadValue<Vector2>();
        return input;
    }

    //attack
    public Vector3 GetMousePoint(){
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePoint.Set(mousePoint.x, mousePoint.y, 0);
        return mousePoint; 
    }

    public bool GetIsPressButtonAttack(){
        return playerInputAction.Player.Attack.triggered;
    }

    //pauseGame
    public bool GetIsPressButtonPause(){
        return playerInputAction.Player.PauseGame.triggered;
    }

    //UnPauseGame
    public bool GetIsPressButtonUnpause(){
        return playerInputAction.UICanvas.UnPauseGame.triggered;
    }

    //skip timeline
    public bool GetIsSkipTimeline() {
        return playerInputAction.Timeline.SkipTimeline.triggered;
    }

    //switch input map
    public void SwitchToMapPauseGameCanvas(){
            playerInputAction.Timeline.Disable();
            playerInputAction.Player.Disable();
            playerInputAction.PauseCanvas.Disable();
            playerInputAction.UICanvas.Enable();
    }

    public void SwitchToMapPlayer(){
            playerInputAction.Timeline.Disable();
            playerInputAction.UICanvas.Disable();
            playerInputAction.PauseCanvas.Disable();
            playerInputAction.Player.Enable();
    }

    public void SwitchToMapLevelUp(){
        playerInputAction.Timeline.Disable();
        playerInputAction.UICanvas.Disable();
        playerInputAction.Player.Disable();
        playerInputAction.PauseCanvas.Enable();
    }

    public void SwitchToMapTimeline(){
        playerInputAction.Timeline.Enable();
        playerInputAction.UICanvas.Disable();
        playerInputAction.Player.Disable();
        playerInputAction.PauseCanvas.Disable();
    }
     
}
