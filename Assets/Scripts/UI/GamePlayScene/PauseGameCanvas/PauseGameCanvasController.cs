using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameCanvasController : MonoBehaviour
{
    Action<KeyValuePair<string, object>> PopUpPauseGameCanvasDelegate;
    Action<KeyValuePair<string, object>> PopOffPauseGameCanvasDelegate;
    private void Start() {
        PopUpPauseGameCanvasDelegate = (param) => {
            PopUpPauseGameCanvas();
        };

        PopOffPauseGameCanvasDelegate = (param) => {
            PopOffPauseGameCanvas();
        };

        
        Obsever.AddListener(EventID.UI_AudioSettingToPauseGameButton_OnClick, PopUpPauseGameCanvasDelegate);
        Obsever.AddListener(EventID.UI_AudioSettingButton_OnClick, PopOffPauseGameCanvasDelegate);
        Obsever.AddListener(EventID.SCENE_ReturnToGameScene, PopOffPauseGameCanvasDelegate);

        // gameObject.transform.GetChild(0).gameObject.SetActive(false);   
        InvokeExtensionCode.Invoke(SceneGameManager.instance, () => gameObject.transform.GetChild(0).gameObject.SetActive(false), Time.unscaledDeltaTime);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.UI_AudioSettingToPauseGameButton_OnClick, PopUpPauseGameCanvasDelegate);
        Obsever.RemoveListener(EventID.UI_AudioSettingButton_OnClick, PopOffPauseGameCanvasDelegate);
        Obsever.RemoveListener(EventID.SCENE_ReturnToGameScene, PopOffPauseGameCanvasDelegate);
    }

    private void Update() {
        ControlPauseGameCanvas();
    }

    void ControlPauseGameCanvas(){
        if(InputManager.instance.GetIsPressButtonPause()){
            PopUpPauseGameCanvas();
        }
        else if(InputManager.instance.GetIsPressButtonUnpause()){
            PopOffPauseGameCanvas();
            Obsever.PostEvent(EventID.SCENE_ReturnToGameScene, new KeyValuePair<string, object>(null, null));
        }
    }
    
    void PopUpPauseGameCanvas(){
        Obsever.PostEvent(EventID.UI_PauseGameCanvas_PopUpPauseGameCanvas, new KeyValuePair<string, object>(null, null));
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    void PopOffPauseGameCanvas(){
        if(!gameObject.transform.GetChild(0).gameObject.activeInHierarchy) return;
        StartCoroutine(PopOffPauseGameCanvasCourotine());
    }

    IEnumerator PopOffPauseGameCanvasCourotine(){
        gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<Animator>().SetTrigger("UnPauseGame");
        yield return new WaitForSecondsRealtime(0.7f);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        // if(isReturnToGameScene){
        //     Obsever.PostEvent(EventID.SCENE_ReturnToGameScene, new KeyValuePair<string, object>(null, null));
        // } 
    }
}
