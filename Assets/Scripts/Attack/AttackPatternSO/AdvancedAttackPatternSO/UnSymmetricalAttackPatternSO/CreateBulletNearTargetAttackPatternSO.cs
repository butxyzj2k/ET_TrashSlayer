using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/CreateBulletNearTargetAttackPattern")]
public class CreateBulletNearTargetAttackPatternSO : UnSymmetricalAttackPatternSO
{
    // [SerializeField] float numBullet = 1;
    [SerializeField] float radiusToCreateBullet = 0;

    // protected override IEnumerator CreateBulletsInAttack(Transform startPoint, Vector3 target, PoolObject weaponPoolObject){
    //     //Với từng viên đạn
    //     for(int i = 0; i < numBullet; i++){
            
    //         //Tạo 1 vị trí để spawn đạn bất kỳ trong vùng cho phép theo radiusToCreateBullet xung quanh target
    //         Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-radiusToCreateBullet, radiusToCreateBullet), UnityEngine.Random.Range(-radiusToCreateBullet, radiusToCreateBullet), 0);
    //         //Spawn đạn tại vị trí vừa tạo và đạn có hướng quay bằng không
    //         GameObject bullet = CreateProjectile(startPoint, target + spawnPos ,Quaternion.identity , false, target,  weaponPoolObject);
    //         //Nếu bullet hiện tại là bullet cuối trong đợt tấn công thì tham số lastStartPointInFlexibleAttack == true, ngược lại là false
    //         if(i == numBullet-1) PerformAttackFlexible(bullet.transform, true);
    //         else PerformAttackFlexible(bullet.transform, false);
            
    //     }
    //     yield return new WaitForSeconds(1.5f);
    // }

    protected override void SetAdvancedAttackPatternVariable()
    {
        numDir = 1;
        numWaveAttack = 1;
    }
    
    protected override IEnumerator CreateAndControlOneBullet(Action<GameObject> PerformAttackFlexibleAction, Transform startPoint, List<MultiDirAttackExtensionCode.NewTransform> newBarrels, int indexBarrel, Vector3 target, PoolObject weaponPoolObject, int bulletIndex){
        //Tạo 1 vị trí để spawn đạn bất kỳ trong vùng cho phép theo radiusToCreateBullet xung quanh target
        Vector3 offsetVector = new(UnityEngine.Random.Range(-radiusToCreateBullet, radiusToCreateBullet), UnityEngine.Random.Range(-radiusToCreateBullet, radiusToCreateBullet), 0);
        //Spawn đạn tại vị trí vừa tạo và đạn có hướng quay bằng không
        GameObject bullet = weaponPoolObject.GetObjectInPool(target + offsetVector, Quaternion.identity, () => {
            Dictionary<string, object> data = new()
                {
                    { "activeBullet", true},
                    { "startPoint", startPoint}
                };
            return data;
        });   
        yield return null;
        PerformAttackFlexibleAction(bullet);
    }
}
