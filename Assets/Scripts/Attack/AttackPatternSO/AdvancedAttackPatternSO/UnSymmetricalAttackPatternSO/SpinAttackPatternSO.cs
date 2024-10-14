using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/SpinAttackPatternSO")]
public class SpinAttackPatternSO : UnSymmetricalAttackPatternSO
{
    // [SerializeField] private int numBullet;
    // [SerializeField] private float angle;
    // [SerializeField] private float attackRate;
    // [SerializeField] private int numWaveAttack; 
    // [SerializeField] private float timeBetweenWaveAttack;

    // [SerializeField] private float numDir;
    // [SerializeField] private float angleBetweenDir;
    // [SerializeField] private float distBetweenNewBarrelAndOrinBarel = 0;

    // protected override IEnumerator CreateBulletsInAttack(Transform startPoint, Vector3 target, PoolObject weaponPoolObject){
    //          //Tạo danh sách các NewBarrels để bắt đầu tấn công
    //         List<MultiDirAttackExtensionCode.NewTransform> newBarrels = MultiDirAttackExtensionCode.CreateMultiDir(startPoint, target, numDir, angleBetweenDir, distBetweenNewBarrelAndOrinBarel);
    //         //Bắt đầu xoay các NewBarrels theo thời gian với góc bằng với góc xoay giữa các Bullet => Đòn tấn công liền mạch giữa những đợt tấn công
    //         SceneGameManager.sceneGameManager.StartCoroutine(MultiDirAttackExtensionCode.RotateNewBarrels(startPoint, angle, distBetweenNewBarrelAndOrinBarel, attackRate, newBarrels));
    //         //Tấn công theo từng wave
    //         for(int i = 0; i < numWaveAttack; i++){
    //             //Nếu Object thực hiện Attack không còn hoạt động, ngừng tấn công
    //             if(!startPoint.gameObject.activeInHierarchy) break;
    //             //Tạo từng Bullet theo AttackRate ở từng barrel
    //             //Duyệt qua từng viên đạn
    //             for(int j = 0; j < numBullet; j++){
    //                 //Nếu Object thực hiện Attack không còn hoạt động, ngừng tấn công
    //                 if(!startPoint.gameObject.activeInHierarchy) break;
    //                 for(int u = 0; u < newBarrels.Count; u++){
    //                     //Với mỗi lần tạo Bullet ở từng barrel, góc xoay của barrel đó sẽ xoay theo 1 góc angle
    //                     GameObject bullet = CreateProjectile(startPoint, newBarrels[u].position, newBarrels[u].rotation * Quaternion.Euler(0, 0, angle), false, target, weaponPoolObject);
    //                      //Nếu i là viên đạn cuối cùng trong đợt tấn công thì tham số lastStartPointInFlexibleAttack == true, ngược lại là false
    //                     if(i == numBullet-1) PerformAttackFlexible(bullet.transform, true);
    //                     else PerformAttackFlexible(bullet.transform, false);
    //                     yield return new WaitForSeconds(attackRate);
    //                 }
    //             } 
    //             yield return new WaitForSeconds(timeBetweenWaveAttack);
    //     }
    // }

    protected override IEnumerator CreateAndControlOneBullet(Action<GameObject> PerformAttackFlexibleAction, Transform startPoint, List<MultiDirAttackExtensionCode.NewTransform> newBarrels, int indexBarrel, Vector3 target, PoolObject weaponPoolObject, int bulletIndex){
        GameObject bullet = weaponPoolObject.GetObjectInPool(newBarrels[indexBarrel].position, newBarrels[indexBarrel].rotation * Quaternion.Euler(0, 0, distOrAngleBetweenBullet), () => {
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
