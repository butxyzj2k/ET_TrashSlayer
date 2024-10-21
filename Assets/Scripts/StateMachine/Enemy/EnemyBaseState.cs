using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected readonly EnemyController enemyController;
    protected readonly Animator animator;

    protected static readonly string IdelAnimationState = "Idel";
    protected static readonly string AttackAnimationState = "Attack";
    protected static readonly string HurtAnimationState = "Hurt";
    protected static readonly string DeathAnimationState = "Dead";

    public EnemyBaseState(EnemyBossController _enemyController, Animator _animator){
        enemyController = _enemyController;
        animator = _animator;
    }

    public virtual void OnEnterState()
    {
        //noop
    }

    public virtual void OnExitState()
    {
        //noop
    }

    public virtual void StateFixedUpdate()
    {
        //noop
    }

    public virtual void StateUpdate()
    {
        //noop
    }
}