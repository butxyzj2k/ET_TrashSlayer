using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : EnemyBaseState
{
    public EnemyHurtState(EnemyBossController _enemyController, Animator _animator) : base(_enemyController, _animator){}

    public override void OnEnterState()
    {
        animator.Play(HurtAnimationState);
    }

    public override void StateUpdate()
    {
        enemyController.enemyMovement.SetObjectIdelding();
    }
}