using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class CalculateResultComponentText : MonoBehaviour
{
    public enum TextIndexInCalculateResultCanvas{
        None = 0,
        One,
        Two,
        Three,
    }
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] protected TextIndexInCalculateResultCanvas textIndexInCalculateResultCanvas;
    protected string textResult;
    protected Action<KeyValuePair<string, object>> ActiveCalculateResultComponentTextDelegate;

    private void Start() {
        ActiveCalculateResultComponentTextDelegate = (param) => {
            SceneGameManager.instance.StartCoroutine(ActiveTextCourotine());
        };

        Obsever.AddListener(EventID.UI_CalculateResultCanvas_OnActive, ActiveCalculateResultComponentTextDelegate);

        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.UI_CalculateResultCanvas_OnActive, ActiveCalculateResultComponentTextDelegate);
    }

    IEnumerator ActiveTextCourotine(){
        yield return new WaitForSecondsRealtime(1.5f * ((int)textIndexInCalculateResultCanvas - 1));
        SetTextResult();
        textMeshProUGUI.gameObject.SetActive(true);
        float time = 0;
        while(time < 1.5f){
            time += Time.unscaledDeltaTime;
            float randomNumber = UnityEngine.Random.Range(0, 1000);
            textMeshProUGUI.text = randomNumber.ToString();
            yield return null;
        }
        textMeshProUGUI.text = textResult;
    }

    protected abstract void SetTextResult();
}