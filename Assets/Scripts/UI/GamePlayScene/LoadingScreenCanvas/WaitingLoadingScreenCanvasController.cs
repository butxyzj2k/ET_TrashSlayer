using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitingLoadingScreenCanvasController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waitingNumberText;

    Action<KeyValuePair<string, object>> WaitingLoadingScreenCanvasDelegate;

    private void Start() {

        WaitingLoadingScreenCanvasDelegate = (param) => {
            if(param.Key == null) return;
            if(!param.Key.Equals("timeWaitingToLoadNextScene")) return;
            gameObject.SetActive(true);
            StartCoroutine(WaitingLoadingScreenCanvasCourotine((int)param.Value));
        };

        Obsever.AddListener(EventID.SCENE_EndScene, WaitingLoadingScreenCanvasDelegate);
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.SCENE_EndScene, WaitingLoadingScreenCanvasDelegate);
    }

    IEnumerator WaitingLoadingScreenCanvasCourotine(int timeWaitingToLoadNextScene){
        float time = 0;
        while(time <= timeWaitingToLoadNextScene){
            time += Time.deltaTime;
            waitingNumberText.text = ((int)(timeWaitingToLoadNextScene - time)).ToString();
            yield return null;
        }
        gameObject.SetActive(false);
    }
}