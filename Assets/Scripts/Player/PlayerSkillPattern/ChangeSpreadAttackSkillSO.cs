using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff/Spread3")]
public class ChangeSpreadAttackSkillSO : SkillPatternSO
{
    [SerializeField] AttackPatternSO spread3AttackSO;
    public override void PerformSkill()
    {
        ObjectAttack.ObjectSkill objectSkill = PlayerController.instance.playerAttack.CurrentObjectSkill;
        objectSkill.attackPatternSO = spread3AttackSO;
        PlayerController.instance.playerAttack.CurrentObjectSkill = objectSkill;
    }
}
