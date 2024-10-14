using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpCanvasController : MonoBehaviour
{

    [SerializeField] private List<SkillPatternSO> ListSkillPatternSOsLv1_2 = new();
    [SerializeField] private List<SkillPatternSO> ListSkillPatternSOsLv3_4 = new();
    [SerializeField] private List<SkillPatternSO> ListSkillPatternSOsLv5_8 = new();

    Animator anim;

    Action<KeyValuePair<string, object>> LevelUpCanvasActiveDelegate;
    Action<KeyValuePair<string, object>> LevelUpCanvasUnActiveDelegate;
    Action<KeyValuePair<string, object>> RemoveAttackPatternSOInListDelegate;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void Start() {
        LevelUpCanvasActiveDelegate = (param) => {
            if(gameObject.transform.GetChild(0).gameObject.activeInHierarchy) return;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(ActiveLevelUpCanvas());
        };

        LevelUpCanvasUnActiveDelegate = (param) => {
            if(!gameObject.transform.GetChild(0).gameObject.activeInHierarchy) return;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        };

        RemoveAttackPatternSOInListDelegate = (param) => {
            if(param.Key == null) return;
            if(!param.Key.Equals("skillPatternSO")) return;
            RemoveSkillPatternInList((SkillPatternSO)param.Value);
        };

        Obsever.AddListener(EventID.Player_LEVELUP, LevelUpCanvasActiveDelegate);
        Obsever.AddListener(EventID.UI_SkillButton_OnClick, RemoveAttackPatternSOInListDelegate);
        Obsever.AddListener(EventID.UI_SkillButton_OnClick, LevelUpCanvasUnActiveDelegate);

        // gameObject.transform.GetChild(0).gameObject.SetActive(false);
        InvokeExtensionCode.Invoke(SceneGameManager.instance, () => gameObject.transform.GetChild(0).gameObject.SetActive(false), Time.unscaledDeltaTime);        
    }

    private void OnDestroy(){
        Obsever.RemoveListener(EventID.Player_LEVELUP, LevelUpCanvasActiveDelegate);
        Obsever.RemoveListener(EventID.UI_SkillButton_OnClick, RemoveAttackPatternSOInListDelegate);
        Obsever.RemoveListener(EventID.UI_SkillButton_OnClick, LevelUpCanvasUnActiveDelegate);
    }

    List<SkillPatternSO> GetCurrentListSkillPatternSOs(){
        int currentLevel = FindObjectOfType<PlayerSkill>().CurrentLevel;

        if(currentLevel <= 2){
            return ListSkillPatternSOsLv1_2;
        }
        else if(currentLevel <= 4){
            return ListSkillPatternSOsLv3_4;
        }
        else{
            return ListSkillPatternSOsLv5_8;
        }
    }

    IEnumerator ActiveLevelUpCanvas(){
        RollingSkill();

        yield return StartCoroutine(ButtonAfterRolling());
    }

    void RollingSkill(){
        Obsever.PostEvent(EventID.UI_LevelUpCanvas_StarRolling, new KeyValuePair<string, object>(null, null));
        anim.enabled = true;
    }

    IEnumerator ButtonAfterRolling(){
        yield return new WaitForSecondsRealtime(0.7f);
        anim.enabled = false;
        Obsever.PostEvent(EventID.UI_LevelUpCanvas_FinishRolling, new KeyValuePair<string, object>("skillPatternSOs", GetCurrentListSkillPatternSOs()));
        yield return null;
        
    }

    void RemoveSkillPatternInList(SkillPatternSO skillPatternSO) {
        GetCurrentListSkillPatternSOs().Remove(skillPatternSO);
    }
}
