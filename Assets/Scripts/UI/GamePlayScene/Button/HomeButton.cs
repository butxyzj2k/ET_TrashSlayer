using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeButton : BaseButton
{

    private void Start() {
        AddOnClickEvent();
    }

    private void OnDestroy(){
        RemoveOnClickEvent();
    }

    public override void OnClickButton()
    {
        button.interactable = false;
        Obsever.PostEvent(EventID.UI_HomeButton_OnClick, new KeyValuePair<string, object>(null, null));
        Obsever.RemoveAllListener(EventID.SCENE_OnStart);
        Obsever.RemoveAllListener(EventID.UI_HomeButton_OnClick);
    }
}