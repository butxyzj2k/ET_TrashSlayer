using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/MultiBarrelAttackPatternSO")]
public class MultiBarrelAttackPatternSO : SymmetricalAttackPatternSO
{
    protected override IEnumerator CreateAndControlOneBullet(Action<GameObject> PerformAttackFlexibleAction, Transform startPoint, List<MultiDirAttackExtensionCode.NewTransform> newBarrels, int indexBarrel, Vector3 target, PoolObject weaponPoolObject, int bulletIndex){
        //Tạo mỗi phía 1 bullet có vị trí bằng vị trí newBarrel + với độ lệch, có hướng trùng với newBarrel
        // GameObject bullet = CreateProjectile(startPoint, newBarrels[indexBarrel].position + (newBarrels[indexBarrel].rotation * (bulletIndex * new Vector3(distOrAngleBetweenBullet, 0, 0))), newBarrels[indexBarrel].rotation, false, target, weaponPoolObject);
        GameObject bullet = weaponPoolObject.GetObjectInPool(newBarrels[indexBarrel].position + (newBarrels[indexBarrel].rotation * (bulletIndex * new Vector3(distOrAngleBetweenBullet, 0, 0))), newBarrels[indexBarrel].rotation, () => {
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
