using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/BurstAttackPattern")]
public class BurstAttackPatternSO : UnSymmetricalAttackPatternSO
{
    [SerializeField] private bool rotatableBullet;

    protected override void SetAdvancedAttackPatternVariable()
    {
        numWaveAttack = 1;
    }

    protected override IEnumerator CreateAndControlOneBullet(Action<GameObject> PerformAttackFlexibleAction, Transform startPoint, List<MultiDirAttackExtensionCode.NewTransform> newBarrels, int indexBarrel, Vector3 target, PoolObject weaponPoolObject, int bulletIndex){
        GameObject bullet;
        if(rotatableBullet) bullet =  weaponPoolObject.GetObjectInPool(newBarrels[indexBarrel].position + bulletIndex * (newBarrels[indexBarrel].rotation * new Vector3(0, distOrAngleBetweenBullet, 0)), newBarrels[indexBarrel].rotation, () => {
            Dictionary<string, object> data = new()
                {
                    { "startPoint", startPoint}
                };
            return data;
        });
        // CreateProjectile(startPoint, newBarrels[indexBarrel].position + bulletIndex * (newBarrels[indexBarrel].rotation * new Vector3(0, distOrAngleBetweenBullet, 0)), newBarrels[indexBarrel].rotation, false, target, weaponPoolObject);        
        else bullet = weaponPoolObject.GetObjectInPool(newBarrels[indexBarrel].position + bulletIndex * (newBarrels[indexBarrel].rotation * new Vector3(0, distOrAngleBetweenBullet, 0)), quaternion.identity, () => {
            Dictionary<string, object> data = new()
                {
                    { "startPoint", startPoint}
                };
            return data;
        });        
        // CreateProjectile(startPoint, newBarrels[indexBarrel].position + bulletIndex * (newBarrels[indexBarrel].rotation * new Vector3(0, distOrAngleBetweenBullet, 0)), quaternion.identity, false, target, weaponPoolObject);
        yield return null;
        PerformAttackFlexibleAction(bullet);
    }
}
