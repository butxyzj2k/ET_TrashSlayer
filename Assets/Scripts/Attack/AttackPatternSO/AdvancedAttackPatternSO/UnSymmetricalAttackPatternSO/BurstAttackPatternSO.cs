using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/BurstAttackPattern")]
public class BurstAttackPatternSO : UnSymmetricalAttackPatternSO
{
    [SerializeField] private bool rotatableBullet;

    // [SerializeField] private float numBullet;
    // [SerializeField] private float distBetweenEachBullet;
    // [SerializeField] private float attackRate;

    // [SerializeField] private float numDir;
    // [SerializeField] private float angleBetweenDir;
    // [SerializeField] private float distBetweenNewBarrelAndOrinBarel = 0.5f;


    // protected override IEnumerator CreateBulletsInAttack(Transform startPoint, Vector3 target, PoolObject weaponPoolObject){
    //     //Tạo 1 List offsetVectors rỗng, sau đó tạo các newBarrels từ barrel gốc và target
    //     // List<Vector3> offsetVectors = new();
    //     List<MultiDirAttackExtensionCode.NewTransform> newBarrels = MultiDirAttackExtensionCode.CreateMultiDir(startPoint, target, numDir, angleBetweenDir, distBetweenNewBarrelAndOrinBarel);
    //     //Với mỗi Barrel, chúng ta tạo cho nó 1 offset vector theo hướng hiện tại của từng Barrel
    //     // for(int i = 0; i < newBarrels.Count; i++){
    //     //     offsetVectors.Add(newBarrels[i].rotation * new Vector3(0, distBetweenEachBullet, 0));
    //     // }

    //     if(!rotatableBullet){
    //         //Với từng viên đạn
    //         for(int i = 0; i < numBullet; i++){
    //             //Nếu phát hiện startPoint không còn hoạt động nữa thì break, không thực hiện tấn công nữa
    //             if(!startPoint.gameObject.activeInHierarchy) break;
    //             //Duyệt qua từng barrels
    //             for(int j = 0; j < newBarrels.Count; j++){
    //                 //Tạo ra 1 bullet mới có phép quay bằng 0
    //                 //vị trí = vị trí barrel hiện tại + offsetVector tương ứng nhân với lại số thứ tự của bullet trong đợt tấn công (càng về cuối thì càng xa)
    //                 GameObject bullet = CreateProjectile(startPoint, newBarrels[j].position + i * (newBarrels[i].rotation * new Vector3(0, distBetweenEachBullet, 0)), quaternion.identity, false, target, weaponPoolObject);
    //                 //Nếu bullet hiện tại là bullet cuối trong đợt tấn công thì tham số lastStartPointInFlexibleAttack == true, ngược lại là false
    //                 if(i == numBullet-1) PerformAttackFlexible(bullet.transform, true);
    //                 else PerformAttackFlexible(bullet.transform, false);
    //             }
    //             yield return new WaitForSeconds(attackRate);
    //         } 
    //     }
    //     else{
    //         //Với từng viên đạn
    //         for(int i = 0; i < numBullet; i++){
    //             //Nếu phát hiện startPoint không còn xuất hiện trên Scene nữa thì break, không thực hiện tấn công nữa
    //             if(!startPoint.gameObject.activeInHierarchy) break;
    //             //Duyệt qua từng barrels
    //             for(int j = 0; j < newBarrels.Count; j++){
    //                 //Tạo ra 1 bullet mới có hướng trùng với barrel hiện tại
    //                 //vị trí = vị trí barrel hiện tại + offsetVector tương ứng nhân với lại số thứ tự của bullet trong đợt tấn công (càng về cuối thì càng xa)
    //                 GameObject bullet = CreateProjectile(startPoint, newBarrels[j].position + i * (newBarrels[i].rotation * new Vector3(0, distBetweenEachBullet, 0)), newBarrels[j].rotation, false, target, weaponPoolObject);
    //                 //Nếu bullet hiện tại là bullet cuối trong đợt tấn công thì tham số lastStartPointInFlexibleAttack == true, ngược lại là false
    //                 if(i == numBullet-1) PerformAttackFlexible(bullet.transform, true);
    //                 else PerformAttackFlexible(bullet.transform, false);
    //             }
                
    //             yield return new WaitForSeconds(attackRate);
    //         }
    //     }
    // }

    protected override void SetAdvancedAttackPatternVariable()
    {
        numWaveAttack = 1;
    }

    protected override IEnumerator CreateAndControlOneBullet(Action<GameObject> PerformAttackFlexibleAction, Transform startPoint, List<MultiDirAttackExtensionCode.NewTransform> newBarrels, int indexBarrel, Vector3 target, PoolObject weaponPoolObject, int bulletIndex){
        GameObject bullet;
        if(rotatableBullet) bullet =  weaponPoolObject.GetObjectInPool(newBarrels[indexBarrel].position + bulletIndex * (newBarrels[indexBarrel].rotation * new Vector3(0, distOrAngleBetweenBullet, 0)), newBarrels[indexBarrel].rotation, () => {
            Dictionary<string, object> data = new()
                {
                    { "activeBullet", true},
                    { "startPoint", startPoint}
                };
            return data;
        });
        // CreateProjectile(startPoint, newBarrels[indexBarrel].position + bulletIndex * (newBarrels[indexBarrel].rotation * new Vector3(0, distOrAngleBetweenBullet, 0)), newBarrels[indexBarrel].rotation, false, target, weaponPoolObject);        
        else bullet = weaponPoolObject.GetObjectInPool(newBarrels[indexBarrel].position + bulletIndex * (newBarrels[indexBarrel].rotation * new Vector3(0, distOrAngleBetweenBullet, 0)), quaternion.identity, () => {
            Dictionary<string, object> data = new()
                {
                    { "activeBullet", true},
                    { "startPoint", startPoint}
                };
            return data;
        });        
        // CreateProjectile(startPoint, newBarrels[indexBarrel].position + bulletIndex * (newBarrels[indexBarrel].rotation * new Vector3(0, distOrAngleBetweenBullet, 0)), quaternion.identity, false, target, weaponPoolObject);
        yield return null;
        PerformAttackFlexibleAction(bullet);
    }
}
