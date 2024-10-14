using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenCanvasController : MonoBehaviour
{
    Action<KeyValuePair<string, object>> ActiveLoadingScreenCanvasDelegate;
    Action<KeyValuePair<string, object>> UnActiveLoadingScreenCanvasDelegate;

    private void Start() {
        ActiveLoadingScreenCanvasDelegate = (param) => {
            gameObject.SetActive(true);
        };

        UnActiveLoadingScreenCanvasDelegate = (param)=> {
            gameObject.SetActive(false);
        };

        Obsever.AddListener(EventID.SCENE_WaitingLoadingScreen_OnComplete, ActiveLoadingScreenCanvasDelegate);
        Obsever.AddListener(EventID.SCENE_OnStart, UnActiveLoadingScreenCanvasDelegate);

        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.SCENE_WaitingLoadingScreen_OnComplete, ActiveLoadingScreenCanvasDelegate);
        Obsever.RemoveListener(EventID.SCENE_OnStart, UnActiveLoadingScreenCanvasDelegate);
    }
}