using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillImages : MonoBehaviour
{
    List<Image> SkillImageUIs = new();
    Action<KeyValuePair<string, object>> SetSkillButtonImageDelegate;

    private void Start() {
        foreach(Transform child in transform){
            SkillImageUIs.Add(child.GetComponent<Image>());
        }

        SetSkillButtonImageDelegate = (param) => {
            if(param.Key == null) return;
            if(!param.Key.Equals("skillImage")) return;
        
            SkillImageUIs[PlayerController.instance.playerSkill.CurrentLevel - 1].sprite = (Sprite)param.Value;

            if(!SkillImageUIs[PlayerController.instance.playerSkill.CurrentLevel - 1].gameObject.activeInHierarchy) SkillImageUIs[PlayerController.instance.playerSkill.CurrentLevel - 1].gameObject.SetActive(true);
        };

        Obsever.AddListener(EventID.UI_SkillButton_OnClick, SetSkillButtonImageDelegate);
    } 

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.UI_SkillButton_OnClick, SetSkillButtonImageDelegate);
    }
}