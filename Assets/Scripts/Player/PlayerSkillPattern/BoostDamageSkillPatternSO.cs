using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff/Damage")]
public class BoostDamageSkillPatternSO : SkillPatternSO
{
    public float BoostDamageRate;
    public override void PerformSkill()
    {
        PlayerController.instance.playerAttack.DamageRate = PlayerController.instance.playerAttack.DefaultDamageRate + PlayerController.instance.playerAttack.DefaultDamageRate * BoostDamageRate;

        Debug.Log("Boost " +BoostDamageRate + " Player Damage");
    }
}
