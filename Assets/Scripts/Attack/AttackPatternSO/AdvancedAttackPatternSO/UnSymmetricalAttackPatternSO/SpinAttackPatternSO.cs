using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/SpinAttackPatternSO")]
public class SpinAttackPatternSO : UnSymmetricalAttackPatternSO
{
    protected override IEnumerator CreateAndControlOneBullet(Action<GameObject> PerformAttackFlexibleAction, Transform startPoint, List<MultiDirAttackExtensionCode.NewTransform> newBarrels, int indexBarrel, Vector3 target, PoolObject weaponPoolObject, int bulletIndex){
        GameObject bullet = weaponPoolObject.GetObjectInPool(newBarrels[indexBarrel].position, newBarrels[indexBarrel].rotation * Quaternion.Euler(0, 0, distOrAngleBetweenBullet), () => {
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
