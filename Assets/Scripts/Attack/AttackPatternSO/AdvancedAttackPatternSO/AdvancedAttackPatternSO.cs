using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedAttackPatternSO : AttackPatternSO
{
    [Header("Bullet")]
    [SerializeField] protected int numBullet;
    [SerializeField] protected float distOrAngleBetweenBullet;
    [SerializeField] protected float attackRate;

    [Header("Wave")]
    [SerializeField] protected int numWaveAttack = 1; 
    [SerializeField] protected float timeBetweenWaveAttack;

    [Header("Direction")]
    [SerializeField] protected bool newDirRotatable = false;
    [SerializeField] protected float numDir = 1;
    [SerializeField] protected float angleBetweenDir;
    [SerializeField] protected float distBetweenNewBarrelAndOrinBarel = 0;

    protected virtual void SetAdvancedAttackPatternVariable(){
        Debug.Log("No override SetAdvancedAttackPatternVariable");
    }

    protected override IEnumerator CreateBulletsInAttack(Transform startPoint, Vector3 target, PoolObject weaponPoolObject, Action resetAttack){
        //Thiết lập các biến
        SetAdvancedAttackPatternVariable();
        //Tạo danh sách các NewBarrels để bắt đầu tấn công        
        List<MultiDirAttackExtensionCode.NewTransform> newBarrels = MultiDirAttackExtensionCode.CreateMultiDir(startPoint, target, numDir, angleBetweenDir, distBetweenNewBarrelAndOrinBarel, this);
        if(newDirRotatable){
            SceneGameManager.instance.StartCoroutine(MultiDirAttackExtensionCode.RotateNewBarrels(startPoint, distOrAngleBetweenBullet, distBetweenNewBarrelAndOrinBarel, attackRate, newBarrels));
        } 
        //Tấn công theo từng wave
        for(int u = 0; u < numWaveAttack; u++){
            //Nếu Object thực hiện Attack không còn hoạt động, ngừng tấn công
            if(!startPoint.gameObject.activeInHierarchy) break;
            //Chờ thực hiện xong trong wave rồi qua wave tiếp theo
            yield return SceneGameManager.instance.StartCoroutine(CreateBulletsBetweenWave(startPoint, newBarrels,target, weaponPoolObject, resetAttack));
            yield return new WaitForSeconds(timeBetweenWaveAttack);
        };
    }

    protected virtual IEnumerator CreateBulletsBetweenWave(Transform startPoint, List<MultiDirAttackExtensionCode.NewTransform> newBarrels ,Vector3 target, PoolObject weaponPoolObject, Action resetAttack){
        Debug.Log("This attack pattern didnt override CreateBulletsBetweenWave");
        yield return null;
    }

    protected virtual IEnumerator CreateAndControlOneBullet(Action<GameObject> PerformAttackFlexibleAction, Transform startPoint, List<MultiDirAttackExtensionCode.NewTransform> newBarrels, int indexBarrel, Vector3 target, PoolObject weaponPoolObject, int bulletIndex){
        Debug.Log("This attack pattern didnt override CreateAndControlOneBullet");
        yield return null;
    }
}