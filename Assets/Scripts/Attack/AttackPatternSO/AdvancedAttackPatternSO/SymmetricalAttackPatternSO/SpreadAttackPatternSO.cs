using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/SpreadAttackPatternSO")]
public class SpreadAttackPatternSO : SymmetricalAttackPatternSO
{
    protected override IEnumerator CreateAndControlOneBullet(Action<GameObject> PerformAttackFlexibleAction, Transform startPoint, List<MultiDirAttackExtensionCode.NewTransform> newBarrels, int indexBarrel, Vector3 target, PoolObject weaponPoolObject, int bulletIndex){
        //Tạo ra 1 bullet mới với có vị trí từ newBarrel và có hướng xoay bằng newBarrel + với độ lệch
        // GameObject bullet = CreateProjectile(startPoint, newBarrels[indexBarrel].position, newBarrels[indexBarrel].rotation * Quaternion.Euler(0, 0, bulletIndex * distOrAngleBetweenBullet), false, target, weaponPoolObject);
        GameObject bullet = weaponPoolObject.GetObjectInPool(newBarrels[indexBarrel].position, newBarrels[indexBarrel].rotation * Quaternion.Euler(0, 0, bulletIndex * distOrAngleBetweenBullet), () => {
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
