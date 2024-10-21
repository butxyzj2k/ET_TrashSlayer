using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyBaseState
{
    public EnemyMoveState(EnemyBossController _enemyController, Animator _animator) : base(_enemyController, _animator){}

    public override void OnEnterState()
    {
        animator.Play(IdelAnimationState);
    }

    public override void StateUpdate()
    {
        enemyController.enemyMovement.PerformMovement(); 
    }
}