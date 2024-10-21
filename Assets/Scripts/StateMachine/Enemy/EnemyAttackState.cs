using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyBossController _enemyController, Animator _animator) : base(_enemyController, _animator){}

    public override void OnEnterState()
    {
        enemyController.enemyAttack.PerformAttack(); 
    }

    public override void StateUpdate()
    {
        enemyController.enemyMovement.SetObjectIdelding();
    }
}