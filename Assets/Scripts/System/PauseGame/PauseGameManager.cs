using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameManager : MonoBehaviour
{
    public static PauseGameManager instance;

    Action<KeyValuePair<string, object>> PauseGameDelegate;
    Action<KeyValuePair<string, object>> UnPauseGameDelegate;

    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }

    private void Start() {
        PauseGameDelegate = (param) => PauseGame();
        UnPauseGameDelegate = (param)=> UnPauseGame();

        Obsever.AddListener(EventID.Player_LEVELUP, PauseGameDelegate);
        Obsever.AddListener(EventID.UI_PauseGameCanvas_PopUpPauseGameCanvas, PauseGameDelegate);
        Obsever.AddListener(EventID.SCENE_ReturnToGameScene, UnPauseGameDelegate);
        Obsever.AddListener(EventID.BOSS_SPAWN, PauseGameDelegate);
        Obsever.AddListener(EventID.UI_BossBanner_OnComplete, UnPauseGameDelegate);
        Obsever.AddListener(EventID.SCENE_EndGame, PauseGameDelegate);
        Obsever.AddListener(EventID.UI_TutorialCanvas_PopUpTutorialCanvas, PauseGameDelegate);

        UnPauseGame();
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.Player_LEVELUP, PauseGameDelegate);
        Obsever.RemoveListener(EventID.UI_PauseGameCanvas_PopUpPauseGameCanvas, PauseGameDelegate);
        Obsever.RemoveListener(EventID.SCENE_ReturnToGameScene, UnPauseGameDelegate);
        Obsever.RemoveListener(EventID.BOSS_SPAWN, PauseGameDelegate);
        Obsever.RemoveListener(EventID.UI_BossBanner_OnComplete, UnPauseGameDelegate);
        Obsever.RemoveListener(EventID.SCENE_EndGame, PauseGameDelegate);
        Obsever.RemoveListener(EventID.UI_TutorialCanvas_PopUpTutorialCanvas, PauseGameDelegate);

        instance = null;
    }

    public void PauseGame(){
        if(Time.timeScale != 0) Time.timeScale = 0;
    }

    public void UnPauseGame(){
        if(Time.timeScale != 1) Time.timeScale = 1;
            
    }
}
