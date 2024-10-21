using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyBossController _enemyController, Animator _animator) : base(_enemyController, _animator){}

    public override void OnEnterState()
    {
        animator.Play(DeathAnimationState);
    }

    public override void StateUpdate()
    {
        enemyController.enemyMovement.SetObjectIdelding();
    }
}