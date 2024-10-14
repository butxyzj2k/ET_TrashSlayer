using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CalculateResultComponentImage : MonoBehaviour
{
    public enum ImageIndexInCalculateResultCanvas{
        None = 0,
        One,
        Two,
        Three,
    }

    [SerializeField] protected Image image;

    [SerializeField] protected ImageIndexInCalculateResultCanvas imageIndexInCalculateResultCanvas;

    protected Action<KeyValuePair<string, object>> ActiveCalculateResultComponentImageDelegate;

    private void Start() {
        ActiveCalculateResultComponentImageDelegate = (param) => {
            ChangeImageColor();
        };

        Obsever.AddListener(EventID.UI_CalculateResultCanvas_OnActive, ActiveCalculateResultComponentImageDelegate);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.UI_CalculateResultCanvas_OnActive, ActiveCalculateResultComponentImageDelegate);
    }

    public void ChangeImageColor(){
        int sceneIndex = SceneManager.GetActiveScene().buildIndex - 1;

        if((int)imageIndexInCalculateResultCanvas < sceneIndex) image.color = new Color(1f, 1f, 1f);
        else if((int)imageIndexInCalculateResultCanvas == sceneIndex){
            if(EnemyBossController.instance.enemyHitting.IsDeath) image.color = new Color(1f, 1f, 1f);
            else image.color = new Color(0, 0, 0);
        }  
        else image.color = new Color(0, 0, 0);
    }
}