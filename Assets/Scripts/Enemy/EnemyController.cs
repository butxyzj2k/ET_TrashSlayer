using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IObjectInPool
{
    public EnemyAttack enemyAttack;
    public EnemyMovement enemyMovement;
    public EnemyHitting enemyHitting;
    public GameObject targetTransform = null;

    public GameObject TargetTransform { get => targetTransform; set => targetTransform = value; }

    private void Awake() {
        enemyAttack = GetComponent<EnemyAttack>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyHitting = GetComponent<EnemyHitting>();
    }

    private void Start() {
        //Lấy target ban đầu chính là Player
        targetTransform = PlayerController.instance.gameObject;
        SettingEnemyTargetTransform();
    }

    public void SettingEnemyTargetTransform(){
        enemyMovement.TargetTransform = targetTransform.transform;
        enemyAttack.TargetTransform = targetTransform.transform;
    }

    private void Update() {
        EnemyManage();
    }

    private void LateUpdate() {
        enemyMovement.ObjectMovementChangeScale();
    }

    public virtual void EnemyManage(){

        if(enemyAttack.IsAttack || enemyHitting.IsHurt || enemyHitting.IsDeath){
            enemyMovement.SetObjectIdelding();
            return;
        }
        else{
           enemyMovement.PerformMovement(); 
        } 
        if(enemyAttack.CheckSightAttack()){
            enemyAttack.PerformAttack();
        }

    }

    public void ReleaseObject(Vector3 position, Quaternion rotation, Func<Dictionary<string, object>> data)
    {
        // targetTransform = FindObjectOfType<PlayerManager>().gameObject;
        // SettingEnemyTargetTransform();
        gameObject.transform.SetPositionAndRotation(position, rotation);
        gameObject.SetActive(true);
    }
}
