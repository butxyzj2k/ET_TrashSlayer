using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossController : EnemyController
{
    public static EnemyBossController instance;

    private void Awake() {
        if(instance == null){
            instance = this;
        }

        enemyAttack = GetComponent<EnemyBossAttack>();
        enemyMovement = GetComponent<EnemyBossMovement>();
        enemyHitting = GetComponent<EnemyBossHitting>();
    }

    private void OnDestroy() {
        instance = null;
    }

    private void Start() {
        //Lấy target ban đầu chính là Player
        targetTransform = PlayerController.instance.gameObject;
        SettingEnemyTargetTransform();
    }

    public override void EnemyManage()
    {
        if(enemyHitting.IsDeath){
           enemyMovement.SetObjectIdelding();
           return; 
        }
        if(enemyAttack.IsAttack|| enemyAttack.CanAttack){
            enemyMovement.SetObjectIdelding();
            enemyAttack.PerformAttack();
            return;
        }
        enemyMovement.PerformMovement();  
    }

}
