using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CalculateResultCanvasController : MonoBehaviour
{
    Action<KeyValuePair<string, object>> TurnOffBackgroundMusicDelegate;
    Action<KeyValuePair<string, object>> ActiveEndgameCanvasDelegate;

    private void Start() {
        TurnOffBackgroundMusicDelegate = (param) => {
            GameObject.FindGameObjectWithTag("BG").GetComponent<AudioSource>().Stop();
        };

        ActiveEndgameCanvasDelegate = (param) => {
            ActiveEndgameCanvas();
        };

        Obsever.AddListener(EventID.SCENE_EndGame, TurnOffBackgroundMusicDelegate);
        Obsever.AddListener(EventID.SCENE_EndGame, ActiveEndgameCanvasDelegate);

        InvokeExtensionCode.Invoke(SceneGameManager.instance, () => gameObject.SetActive(false), Time.unscaledDeltaTime);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.SCENE_EndGame, TurnOffBackgroundMusicDelegate);
        Obsever.RemoveListener(EventID.SCENE_EndGame, ActiveEndgameCanvasDelegate);
    }

    public void ActiveEndgameCanvas(){
        SceneGameManager.instance.StartCoroutine(ActiveEndgameCanvasCourotine());
    }

    IEnumerator ActiveEndgameCanvasCourotine(){
        yield return new WaitForSecondsRealtime(1.5f);
        gameObject.SetActive(true);
        Obsever.PostEvent(EventID.UI_CalculateResultCanvas_OnActive, new KeyValuePair<string, object>(null, null));
    }
}
