using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarCalculateResultComponentImage : CalculateResultComponentImage
{

    private void Start() {
        ActiveCalculateResultComponentImageDelegate = (param) => {
            ChangeImageColor();
            SceneGameManager.instance.StartCoroutine(SpawnStartImage());
        };

        Obsever.AddListener(EventID.UI_CalculateResultCanvas_OnActive, ActiveCalculateResultComponentImageDelegate);
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.UI_CalculateResultCanvas_OnActive, ActiveCalculateResultComponentImageDelegate);
    }

    IEnumerator SpawnStartImage(){
        //1.5 * 3 là thời gian chờ text trong CalculateResultCanvas thực hiện animation xong
        yield return new WaitForSecondsRealtime(1.5f * 3 + ((int)imageIndexInCalculateResultCanvas - 1) * 0.5f );
        gameObject.SetActive(true);
    }
}