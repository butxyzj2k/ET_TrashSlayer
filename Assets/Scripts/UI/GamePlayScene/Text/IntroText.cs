using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroText : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    string contentInUI;
    [SerializeField] float typeRate;

    [Serializable]
    public struct UIContent{
        public float typeRate;
        public string text;
    }
    [SerializeField] List<UIContent> contentList;
    int currentIndex = 0;

    private void OnEnable() {
        SetTypeRate(contentList[currentIndex].typeRate);
        SetTypewriteEffectController(contentList[currentIndex].text);
        currentIndex++;
    }

    private void OnDisable() {
        contentInUI = "";
        textMeshProUGUI.text = contentInUI;
    }

    private void Awake() {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        contentInUI = "";
    }

    public void SetTypeRate(float _typeRate){
        typeRate = _typeRate;
    }

    public void SetTypewriteEffectController(string text){
        contentInUI = "";
        if (textMeshProUGUI != null) {
            textMeshProUGUI.text = contentInUI;
        } else {
            Debug.Log("SetTypewriteEffectController called: TextMeshProUGUI is null.");
        }
        // Debug.Log("SetTypewriteEffectController called with text: " + text);
        // textMeshProUGUI.text = contentInUI;
        // Debug.Log("SetTypewriteEffectController called with text: " + text);
        SceneGameManager.instance.StartCoroutine(EffectTypewritter(text));
    }

    IEnumerator EffectTypewritter(string text){
        foreach(char character in text){
            contentInUI += character;
            textMeshProUGUI.text = contentInUI;
            yield return new WaitForSeconds(typeRate);
        }
        
    }
}
