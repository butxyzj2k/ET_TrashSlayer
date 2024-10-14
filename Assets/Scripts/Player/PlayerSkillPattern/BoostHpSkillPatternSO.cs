using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff/Hp")]
public class BoostHpSkillPatternSO : SkillPatternSO
{
    public int HealthRate;
        public override void PerformSkill()
    {
        PlayerController.instance.playerHitting.MaxHealth += HealthRate;
        PlayerController.instance.playerHitting.CurrentHealth += HealthRate; 
        Obsever.PostEvent(EventID.PlayerHpBar_OnChangeHp, new KeyValuePair<string, object>("maxHealth", PlayerController.instance.playerHitting.MaxHealth));
        Obsever.PostEvent(EventID.PlayerHpBar_OnChangeHp, new KeyValuePair<string, object>("health", PlayerController.instance.playerHitting.CurrentHealth));
        Debug.Log("Add " + HealthRate + " Hp");
    }
}
