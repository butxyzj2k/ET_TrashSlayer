using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : PlayerBaseState
{
    public PlayerMovementState(PlayerController _playerController, Animator _animator) : base(_playerController, _animator){}

    public override void OnEnterState()
    {
        animator.Play(MovementAnimationState);
    }

    
}