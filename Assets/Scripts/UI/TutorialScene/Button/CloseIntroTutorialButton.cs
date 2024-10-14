using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CloseIntroTutorialButton : BaseButton
{
    private void Start() {
        AddOnClickEvent();
    }

    private void OnDestroy(){
        RemoveOnClickEvent();
    }

    public override void OnClickButton()
    {
        Obsever.PostEvent(EventID.SCENE_ReturnToGameScene, new KeyValuePair<string, object>(null, null));
        Obsever.PostEvent(EventID.UI_CloseIntroTutorialButton_OnClick, new KeyValuePair<string, object>(null, TutorialCanvasController.TutorialNumber.Tuto2));
    }
}