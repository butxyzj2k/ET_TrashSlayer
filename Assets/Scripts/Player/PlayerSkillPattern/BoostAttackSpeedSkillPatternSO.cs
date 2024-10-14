using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff/AttackSpeed")]
public class BoostAttackSpeedSkillPatternSO : SkillPatternSO
{
    public float BoostAttackSpeedRate;
    public override void PerformSkill()
    {
        PlayerController.instance.playerAttack.AttackSpeedRate = PlayerController.instance.playerAttack.DefaultAttackSpeedRate - PlayerController.instance.playerAttack.DefaultAttackSpeedRate * BoostAttackSpeedRate;
        Debug.Log("Boost " + BoostAttackSpeedRate + " AttackSpeed");
    }
}
