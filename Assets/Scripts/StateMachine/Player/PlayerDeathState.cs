using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    public PlayerDeathState(PlayerController _playerController, Animator _animator) : base(_playerController, _animator){}

    public override void OnEnterState()
    {
        animator.Play(DeathAnimationState);
    }
}