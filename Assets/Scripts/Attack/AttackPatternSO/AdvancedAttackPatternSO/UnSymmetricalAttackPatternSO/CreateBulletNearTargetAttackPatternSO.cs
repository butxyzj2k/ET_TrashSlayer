using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/CreateBulletNearTargetAttackPattern")]
public class CreateBulletNearTargetAttackPatternSO : UnSymmetricalAttackPatternSO
{
    // [SerializeField] float numBullet = 1;
    [SerializeField] float radiusToCreateBullet = 0;

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
                    { "startPoint", startPoint}
                };
            return data;
        });   
        yield return null;
        PerformAttackFlexibleAction(bullet);
    }
}
