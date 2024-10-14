using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEnemyAttack : EnemyAttack
{
    [SerializeField] private float sightMeeleeAttackDist;
    [SerializeField] private float sightBulletAttackDist;
    [SerializeField] private float rangeToAttackMeelee;


    public void CheckTypeAttack(){
        //Nếu khoảng cách giữa target và quái bé hơn rangeToAttackMeelee thì quái sẽ tấn công gần, nếu không sẽ tấn công xa
        if(Vector3.Distance(targetTransform.position, gameObject.transform.position) <= rangeToAttackMeelee){
            sightAttackDist = sightMeeleeAttackDist;
            SetCurrentObjectSkill("Meelee");
        }
        else{
            sightAttackDist = sightBulletAttackDist;
            SetCurrentObjectSkill("Bullet");
        }
    }

    public override bool CheckSightAttack()
    {
        CheckTypeAttack();
        return base.CheckSightAttack();
    }
}
