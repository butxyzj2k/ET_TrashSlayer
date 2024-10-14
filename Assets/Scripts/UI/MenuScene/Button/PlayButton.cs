using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : BaseButton
{
    private void Start() {
        AddOnClickEvent();
    }

    private void OnDestroy(){
        RemoveOnClickEvent();
    }

    public override void OnClickButton()
    {
        Obsever.PostEvent(EventID.UI_PlayButton_OnClick, new KeyValuePair<string, object>(null, null));
    }
}
