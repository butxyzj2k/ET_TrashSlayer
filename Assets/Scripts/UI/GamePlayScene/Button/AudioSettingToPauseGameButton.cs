using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettingToPauseGameButton : BaseButton
{
    private void Start() {
        AddOnClickEvent();
    }

    private void OnDestroy(){
        RemoveOnClickEvent();
    }

    public override void OnClickButton()
    {
        Obsever.PostEvent(EventID.UI_AudioSettingToPauseGameButton_OnClick, new KeyValuePair<string, object>(null, null));
    }
}
