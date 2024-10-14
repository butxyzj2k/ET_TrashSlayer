using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCanvasController : MonoBehaviour
{
    public enum TutorialNumber{
        None = 0,
        Tuto1,
        Tuto2,
        Tuto3,
    }

    [SerializeField] TutorialNumber tutorialNumber;
    Action<KeyValuePair<string, object>> PopUpTutorialCanvasDelegate;
    Action<KeyValuePair<string, object>> PopOffTutorialCanvasDelegate;

    private void Start() {
        PopUpTutorialCanvasDelegate = (param) => {
            if(param.Key != null) return;
            if((TutorialNumber)param.Value == tutorialNumber) PopUpTutorialCanvas();
        };

        PopOffTutorialCanvasDelegate = (param) => {
            if(!gameObject.activeInHierarchy) return;
            PopOffTutoCanvas();
        };

        Obsever.AddListener(EventID.UI_CloseIntroTutorialButton_OnClick, PopUpTutorialCanvasDelegate);
        Obsever.AddListener(EventID.Tutorial_ColliderMoveCamera_OnTrigger, PopUpTutorialCanvasDelegate);
        Obsever.AddListener(EventID.SCENE_ReturnToGameScene, PopOffTutorialCanvasDelegate);

        if(tutorialNumber != TutorialNumber.Tuto1) gameObject.SetActive(false);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.UI_CloseIntroTutorialButton_OnClick, PopUpTutorialCanvasDelegate);
        Obsever.RemoveListener(EventID.Tutorial_ColliderMoveCamera_OnTrigger, PopUpTutorialCanvasDelegate);
        Obsever.RemoveListener(EventID.SCENE_ReturnToGameScene, PopOffTutorialCanvasDelegate);
    }
    
     public void PopUpTutorialCanvas(){
        if(gameObject.activeInHierarchy) return;
        Obsever.PostEvent(EventID.UI_TutorialCanvas_PopUpTutorialCanvas, new KeyValuePair<string, object>(null, null));
        gameObject.SetActive(true);
    }

    public void PopOffTutoCanvas(){
        if(!gameObject.activeInHierarchy) return;
        StartCoroutine(PopOffTutoContentCourotine());
    }

    IEnumerator PopOffTutoContentCourotine(){
        GetComponent<Animator>().SetTrigger("PopOff");
        yield return new WaitForSecondsRealtime(0.7f);
        gameObject.SetActive(false);
    }
}