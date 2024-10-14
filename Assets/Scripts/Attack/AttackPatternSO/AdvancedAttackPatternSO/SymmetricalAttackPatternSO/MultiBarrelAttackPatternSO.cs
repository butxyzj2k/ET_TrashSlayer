using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/MultiBarrelAttackPatternSO")]
public class MultiBarrelAttackPatternSO : SymmetricalAttackPatternSO
{
    // [SerializeField] private float distBetweenEachBullet;
    // [SerializeField] private int numBullet;
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
    // //Bỏ
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
    //     //Tạo một Vector3 lưu tọa độ lệch nhau của từng Bullet
    //     Vector3 offsetVector = new Vector3(distBetweenEachBullet, 0, 0);
    //     //Tấn công theo từng wave
    //     for(int u = 0; u < numWaveAttack; u++){
    //         //Nếu Object thực hiện Attack không còn hoạt động, ngừng tấn công
    //         if(!startPoint.gameObject.activeInHierarchy) break;
    //         //Tạo danh sách các NewBarrels để bắt đầu tấn công
    //         List<MultiDirAttackExtensionCode.NewTransform> newBarrels = MultiDirAttackExtensionCode.CreateMultiDir(startPoint, target, numDir, angleBetweenDir, distBetweenNewBarrelAndOrinBarel);
    //         //Duyệt qua từng Barrel
    //         for(int j = 0; j < newBarrels.Count; j++){
    //             //Nếu Object thực hiện Attack không còn hoạt động, ngừng tấn công
    //             if(!startPoint.gameObject.activeInHierarchy) break;
    //             //Duyệt qua từng cặp đạn 2 bên đạn ở center
    //             for(int i = 1; i <= numBullet/2; i++){
    //                 //Tạo mỗi phía 1 bullet có vị trí bằng vị trí newBarrel + với độ lệch, có hướng trùng với newBarrel
    //                 GameObject bullet1 = CreateProjectile(startPoint,  newBarrels[j].position + (newBarrels[j].rotation * (i * offsetVector)),newBarrels[j].rotation, false,target, weaponPoolObject);
    //                 GameObject bullet2 = CreateProjectile(startPoint, newBarrels[j].position - (newBarrels[j].rotation * (i * offsetVector)), newBarrels[j].rotation, false,target, weaponPoolObject);
    //                 //Nếu i là số thứ tự của cặp đạn cuối cùng trong đợt tấn công thì tham số lastStartPointInFlexibleAttack == true, ngược lại là false
    //                 if(i == numBullet/2 - 0.5f){
    //                     PerformAttackFlexible(bullet1.transform, true);
    //                     PerformAttackFlexible(bullet2.transform, true);
    //                 }
    //                 else{
    //                     PerformAttackFlexible(bullet1.transform, false);
    //                     PerformAttackFlexible(bullet2.transform, false);
    //                 } 
    //             }
    //             //Tạo bullet trung tâm tương ứng từng barrel
    //             GameObject bullet = CreateProjectile(startPoint, newBarrels[j].position, newBarrels[j].rotation, false, target, weaponPoolObject);
    //             //Nếu số đạn chỉ có 1 <=> bullet center cũng là bullet cuối thì tham số lastStartPointInFlexibleAttack == true, ngược lại là false
    //             if(numBullet/2 < 1) PerformAttackFlexible(bullet.transform, true);
    //             else PerformAttackFlexible(bullet.transform, false);
    //         }
    //         yield return new WaitForSeconds(timeBetweenWaveAttack);
    //     }
    // }

    protected override IEnumerator CreateAndControlOneBullet(Action<GameObject> PerformAttackFlexibleAction, Transform startPoint, List<MultiDirAttackExtensionCode.NewTransform> newBarrels, int indexBarrel, Vector3 target, PoolObject weaponPoolObject, int bulletIndex){
        //Tạo mỗi phía 1 bullet có vị trí bằng vị trí newBarrel + với độ lệch, có hướng trùng với newBarrel
        // GameObject bullet = CreateProjectile(startPoint, newBarrels[indexBarrel].position + (newBarrels[indexBarrel].rotation * (bulletIndex * new Vector3(distOrAngleBetweenBullet, 0, 0))), newBarrels[indexBarrel].rotation, false, target, weaponPoolObject);
        GameObject bullet = weaponPoolObject.GetObjectInPool(newBarrels[indexBarrel].position + (newBarrels[indexBarrel].rotation * (bulletIndex * new Vector3(distOrAngleBetweenBullet, 0, 0))), newBarrels[indexBarrel].rotation, () => {
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
