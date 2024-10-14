using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour
{
    protected Button button;

    private void Awake() {
        button = GetComponent<Button>();
    }

    public abstract void OnClickButton();

    public virtual void AddOnClickEvent(){
        button.onClick.AddListener(OnClickButton);
    }

    public virtual void RemoveOnClickEvent(){
        button.onClick.RemoveAllListeners();
    }
}