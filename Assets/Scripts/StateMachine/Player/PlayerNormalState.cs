using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalState : PlayerBaseState
{
    //PlayerNormalState là state chung của Idel, Movement và Attack
    public PlayerNormalState(PlayerController _playerController, Animator _animator) : base(_playerController, _animator){}

    public override void OnEnterState()
    {
        animator.Play(IdelAnimationState);
    }

    public override void StateUpdate()
    {
        //Movement        
       playerController.playerMovement.ObjectPlayMovementSFX();
       playerController.playerMovement.ObjectMovementAnim();

        //Attack
       playerController.playerAttack.PerformAttack();
       playerController.playerAttack.ChangeBullet();
    }

    public override void StateFixedUpdate()
    {
       playerController.playerMovement.PerformMovement();
    }
}