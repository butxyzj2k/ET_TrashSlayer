using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceUICanvasController : MonoBehaviour
{
    Action<KeyValuePair<string, object>> PopUpSourceUICanvasDelegate;
    Action<KeyValuePair<string, object>> PopOffSourceUICanvasDelegate;

    private void Start() {
        PopUpSourceUICanvasDelegate = (param) => {
            PopUpSourceCanvas();
        };

        PopOffSourceUICanvasDelegate = (param)=> {
            PopOffSourceCanvas();
        };

        Obsever.AddListener(EventID.UI_SourceButton_OnClick, PopUpSourceUICanvasDelegate);
        Obsever.AddListener(EventID.UI_ReturnToMenuSceneButton_OnClick, PopOffSourceUICanvasDelegate);

        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.UI_SourceButton_OnClick, PopUpSourceUICanvasDelegate);
        Obsever.RemoveListener(EventID.UI_ReturnToMenuSceneButton_OnClick, PopOffSourceUICanvasDelegate);
    }

    public void PopUpSourceCanvas(){
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void PopOffSourceCanvas(){
        if(!gameObject.transform.GetChild(0).gameObject.activeInHierarchy) return;
        StartCoroutine(PopOffAudioSettingCanvasCourotine());
    }

    IEnumerator PopOffAudioSettingCanvasCourotine(){
        gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<Animator>().SetTrigger("PopOff");
        yield return new WaitForSecondsRealtime(0.7f);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
