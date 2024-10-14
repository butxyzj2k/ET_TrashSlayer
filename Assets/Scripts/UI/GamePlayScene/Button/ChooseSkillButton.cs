using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseSkillButton : BaseButton
{
    private SkillPatternSO skillPatternSO;
    [SerializeField] private Image skillImage;
    [SerializeField] private TextMeshProUGUI skillDescription;
    Action<KeyValuePair<string, object>> SetSkillButtonNonInteractiveDelegate;
    Action<KeyValuePair<string, object>> SetSkillButtonInteractiveDelegate;
    Action<KeyValuePair<string, object>> SetButtonSkillDelegate;

    private void Start() {
        AddOnClickEvent();

        SetSkillButtonInteractiveDelegate = (pram) => {
            if(button.interactable) return;
            button.interactable = true;
        };
        SetSkillButtonNonInteractiveDelegate = (pram) => {
            if(!button.interactable) return;
            button.interactable = false;
        };
        SetButtonSkillDelegate = (pram) => {
            if(pram.Key == null) return;
            if(!pram.Key.Equals("skillPatternSOs")) return;
            SetSkillOnButton((List<SkillPatternSO>)pram.Value);
        };

        Obsever.AddListener(EventID.UI_LevelUpCanvas_StarRolling, SetSkillButtonNonInteractiveDelegate);
        Obsever.AddListener(EventID.UI_LevelUpCanvas_FinishRolling, SetSkillButtonInteractiveDelegate);
        Obsever.AddListener(EventID.UI_LevelUpCanvas_FinishRolling, SetButtonSkillDelegate);
    }

    private void OnDestroy(){
        RemoveOnClickEvent();

        Obsever.RemoveListener(EventID.UI_LevelUpCanvas_StarRolling, SetSkillButtonNonInteractiveDelegate);
        Obsever.RemoveListener(EventID.UI_LevelUpCanvas_FinishRolling, SetSkillButtonInteractiveDelegate);
        Obsever.RemoveListener(EventID.UI_LevelUpCanvas_FinishRolling, SetButtonSkillDelegate);
    }

    void SetSkillOnButton(List<SkillPatternSO> skillPatternSOs){
        int randomNumber = UnityEngine.Random.Range(0,  skillPatternSOs.Count);
        skillPatternSO = skillPatternSOs[randomNumber];
        skillImage.sprite = skillPatternSO.SkillImage;
        skillDescription.text = skillPatternSO.SkillDescription;
    }

    public override void OnClickButton()
    {
        PerformSkillPattern();
        Obsever.PostEvent(EventID.UI_SkillButton_OnClick, new KeyValuePair<string, object>("skillPatternSO", skillPatternSO));
        Obsever.PostEvent(EventID.UI_SkillButton_OnClick, new KeyValuePair<string, object>("skillImage", skillPatternSO.SkillImage));
        Obsever.PostEvent(EventID.SCENE_ReturnToGameScene, new KeyValuePair<string, object>(null, null));
    }

    void PerformSkillPattern(){ 
        skillPatternSO.PerformSkill();
    }
}
