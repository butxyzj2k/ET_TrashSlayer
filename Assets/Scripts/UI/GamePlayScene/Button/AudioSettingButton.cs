using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingButton : BaseButton
{
    private void Start() {
        AddOnClickEvent();
    }

    private void OnDestroy(){
        RemoveOnClickEvent();
    }

    public override void OnClickButton()
    {
        Obsever.PostEvent(EventID.UI_AudioSettingButton_OnClick, new KeyValuePair<string, object>(null, null));
    }
}
