using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/SpreadAttackPatternSO")]
public class SpreadAttackPatternSO : SymmetricalAttackPatternSO
{
    // [SerializeField] private int numBullet;
    // [SerializeField] private float angle;

    // [SerializeField] private int numWaveAttack; 
    // [SerializeField] private float timeBetweenWaveAttack;

    // [SerializeField] private float numDir;
    // [SerializeField] private float angleBetweenDir;
    // [SerializeField] private float distBetweenNewBarrelAndOrinBarel = 0;

    // public void SetNumBullet(){
    //     if(numBullet % 2 == 0){
    //         numBullet += 1;
    //     }
    // }
    // //bỏ
    // public override Quaternion GetDir(Transform startPoint, Vector3 target)
    // {
    //     Vector3 dir = (target - startPoint.position).normalized;
    //     float angle = Mathf.Atan2(dir.y, dir.x) *  Mathf.Rad2Deg;
    //     Quaternion rotation = Quaternion.Euler(0, 0, angle - 90);
    //     return rotation;
    // }
    // //Ghi đè lại phương thức PerformAttack để thiết lập số đạn bắn ra luôn là một số lẻ
    // public override void PerformAttack(Transform startPoint, Vector3 target, PoolObject weaponPoolObject)
    // {
    //     SetNumBullet();
    //     base.PerformAttack(startPoint, target, weaponPoolObject);
    // }
    // //Ghi đè lại phương thức PerformAttack để thiết lập số đạn bắn ra luôn là một số lẻ
    // public override void PerformAttack(Transform startPoint, Vector3 target, PoolObject weaponPoolObject, bool lastStartPointInFlexibleAttack)
    // {
    //     SetNumBullet();
    //     base.PerformAttack(startPoint, target, weaponPoolObject, lastStartPointInFlexibleAttack);
    // }

    // protected override IEnumerator CreateBulletsInAttack(Transform startPoint, Vector3 target, PoolObject weaponPoolObject){
    //     //Tạo danh sách các NewBarrels để bắt đầu tấn công
    //     List<MultiDirAttackExtensionCode.NewTransform> newBarrels = MultiDirAttackExtensionCode.CreateMultiDir(startPoint, target, numDir, angleBetweenDir, distBetweenNewBarrelAndOrinBarel);
    //     //Tấn công theo từng wave
    //     for(int u = 0; u < numWaveAttack; u++){
    //         //Nếu Object thực hiện Attack không còn hoạt động, ngừng tấn công
    //         if(!startPoint.gameObject.activeInHierarchy) break;
    //         //Duyệt qua từng cặp đạn 2 bên đạn ở center
    //         for(int i = 0; i <= numBullet/2; i++){
    //             //Nếu Object thực hiện Attack không còn hoạt động, ngừng tấn công
    //             if(!startPoint.gameObject.activeInHierarchy) break;
    //             //Duyệt qua từng Barrel
    //             for(int j = 0; j < newBarrels.Count; j++){
    //                 //Nếu i==0 tức là ở trung tâm
    //                 if(i == 0){
    //                     //Tạo bullet trung tâm tương ứng từng barrel
    //                     GameObject bullet = CreateProjectile(startPoint, newBarrels[j].position, newBarrels[j].rotation, false, target, weaponPoolObject);
    //                     //Nếu số đạn chỉ có 1 <=> bullet center cũng là bullet cuối thì tham số lastStartPointInFlexibleAttack == true, ngược lại là false
    //                     if(numBullet/2 < 1) PerformAttackFlexible(bullet.transform, true);
    //                     else PerformAttackFlexible(bullet.transform, false);
    //                 }
    //                 else{
    //                      //Tạo mỗi phía 1 bullet có vị trí bằng vị trí newBarrel, có hướng bằng newBarreal + với độ lệch
    //                     GameObject bullet1 = CreateProjectile(startPoint, newBarrels[j].position ,newBarrels[j].rotation * Quaternion.Euler(0, 0, -i * angle), false, target, weaponPoolObject);
    //                     GameObject bullet2 = CreateProjectile(startPoint, newBarrels[j].position, newBarrels[j].rotation * Quaternion.Euler(0, 0, i * angle), false, target, weaponPoolObject);
    //                     //Nếu i là số thứ tự của cặp đạn cuối cùng trong đợt tấn công thì tham số lastStartPointInFlexibleAttack == true, ngược lại là false
    //                     if(i == numBullet/2 - 0.5f){
    //                         PerformAttackFlexible(bullet1.transform, true);
    //                         PerformAttackFlexible(bullet2.transform, true);
    //                     }
    //                     else{
    //                         PerformAttackFlexible(bullet1.transform, false);
    //                         PerformAttackFlexible(bullet2.transform, false);
    //                     }
    //                 } 
    //             }
    //         }
    //         yield return new WaitForSeconds(timeBetweenWaveAttack);
    //     }
    // }

    protected override IEnumerator CreateAndControlOneBullet(Action<GameObject> PerformAttackFlexibleAction, Transform startPoint, List<MultiDirAttackExtensionCode.NewTransform> newBarrels, int indexBarrel, Vector3 target, PoolObject weaponPoolObject, int bulletIndex){
        //Tạo ra 1 bullet mới với có vị trí từ newBarrel và có hướng xoay bằng newBarrel + với độ lệch
        // GameObject bullet = CreateProjectile(startPoint, newBarrels[indexBarrel].position, newBarrels[indexBarrel].rotation * Quaternion.Euler(0, 0, bulletIndex * distOrAngleBetweenBullet), false, target, weaponPoolObject);
        GameObject bullet = weaponPoolObject.GetObjectInPool(newBarrels[indexBarrel].position, newBarrels[indexBarrel].rotation * Quaternion.Euler(0, 0, bulletIndex * distOrAngleBetweenBullet), () => {
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
