using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected readonly PlayerController playerController;
    protected readonly Animator animator;

    protected static readonly string IdelAnimationState = "Idel";
    protected static readonly string MovementAnimationState = "Movement";
    protected static readonly string HurtAnimationState = "Hurt";
    protected static readonly string DeathAnimationState = "Dead_Player";

    public PlayerBaseState(PlayerController _playerController, Animator _animator){
        playerController = _playerController;
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