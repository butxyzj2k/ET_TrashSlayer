using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;
    public PlayerHitting playerHitting;
    public PlayerSkill playerSkill;
    private void Awake() {
        if(instance == null){
            instance = this;
        }

        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        playerHitting = GetComponent<PlayerHitting>();
        playerSkill = GetComponent<PlayerSkill>();
    }

    private void OnDestroy() {
        instance = null;
    }

    private void Update() {
        if(playerHitting.IsHurt || playerHitting.IsDeath)return;
        
        //Movement        
        playerMovement.ObjectPlayMovementSFX();
        playerMovement.ObjectMovementAnim();

        //Attack
        playerAttack.PerformAttack();
        playerAttack.ChangeBullet();
    }
    private void FixedUpdate() {
        playerMovement.PerformMovement();
    }
}
