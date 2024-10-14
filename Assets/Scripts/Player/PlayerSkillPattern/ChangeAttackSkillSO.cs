using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff/ChangeAttackSkillSO")]
public class ChangeAttackSkillSO : SkillPatternSO
{
    [SerializeField] AttackPatternSO newAttackPatternSO;
    public override void PerformSkill()
    {
        ObjectAttack.ObjectSkill objectSkill = PlayerController.instance.playerAttack.CurrentObjectSkill;
        objectSkill.attackPatternSO = newAttackPatternSO;
        PlayerController.instance.playerAttack.CurrentObjectSkill = objectSkill;
    }
}
