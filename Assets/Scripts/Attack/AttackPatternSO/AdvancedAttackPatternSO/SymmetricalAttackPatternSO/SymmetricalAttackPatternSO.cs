using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymmetricalAttackPatternSO : AdvancedAttackPatternSO
{
    public void SetNumBullet(){
        if(numBullet % 2 == 0){
            numBullet += 1;
        }
    }
    //Ghi đè lại phương thức PerformAttack để thiết lập số đạn bắn ra luôn là một số lẻ
    public override void PerformAttack(Transform startPoint, Vector3 target, PoolObject weaponPoolObject, Action resetAttack)
    {
        SetNumBullet();
        base.PerformAttack(startPoint, target, weaponPoolObject, resetAttack);
    }
    //Ghi đè lại phương thức PerformAttack để thiết lập số đạn bắn ra luôn là một số lẻ
    public override void PerformAttack(Transform startPoint, Vector3 target, PoolObject weaponPoolObject, bool lastStartPointInFlexibleAttack, Action resetAttack)
    {
        SetNumBullet();
        base.PerformAttack(startPoint, target, weaponPoolObject, lastStartPointInFlexibleAttack, resetAttack);
    }

    // protected override IEnumerator CreateBulletsInAttack(Transform startPoint, Vector3 target, PoolObject weaponPoolObject){
    //     //Tạo danh sách các NewBarrels để bắt đầu tấn công
    //     List<MultiDirAttackExtensionCode.NewTransform> newBarrels = MultiDirAttackExtensionCode.CreateMultiDir(startPoint, target, numDir, angleBetweenDir, distBetweenNewBarrelAndOrinBarel);
    //     //Tấn công theo từng wave
    //     for(int u = 0; u < numWaveAttack; u++){
    //         //Nếu Object thực hiện Attack không còn hoạt động, ngừng tấn công
    //         if(!startPoint.gameObject.activeInHierarchy) break;
    //         //Chờ thực hiện xong trong wave rồi qua wave tiếp theo
    //         yield return SceneGameManager.sceneGameManager.StartCoroutine(CreateBulletsBetweenWave(startPoint, newBarrels,target, weaponPoolObject));
    //         yield return new WaitForSeconds(timeBetweenWaveAttack);
    //     }
    // }

    protected override IEnumerator CreateBulletsBetweenWave(Transform startPoint, List<MultiDirAttackExtensionCode.NewTransform> newBarrels ,Vector3 target, PoolObject weaponPoolObject, Action resetAttack){
            //Duyệt qua từng cặp đạn 2 bên đạn ở center
            for(int i = 0; i <= numBullet/2; i++){
                //Nếu Object thực hiện Attack không còn hoạt động, ngừng tấn công
                if(!startPoint.gameObject.activeInHierarchy) break;
                //Duyệt qua từng Barrel
                for(int j = 0; j < newBarrels.Count; j++){
                    //Nếu i==0 tức là ở trung tâm
                    if(i == 0){
                        //Thực hiện tạo 1 viên đạn ở trung tâm barrel
                        SceneGameManager.instance.StartCoroutine(CreateAndControlOneBullet((bullet) => {
                            if(bullet == null) return;
                            //Nếu số đạn chỉ có 1 <=> bullet center cũng là bullet cuối thì tham số lastStartPointInFlexibleAttack == true, ngược lại là false
                            if(numBullet/2 < 1) PerformAttackFlexible(bullet.transform, true, resetAttack);
                            else PerformAttackFlexible(bullet.transform, false, resetAttack);
                        }, startPoint, newBarrels, j, target, weaponPoolObject, i));
                    }
                    else{
                        //Thực hiện tạo cặp viên đạn ở 2 bên barrel
                        SceneGameManager.instance.StartCoroutine(CreateAndControlOneBullet((bullet) => {
                            if(bullet == null) return;
                            //Nếu i là số thứ tự của cặp đạn cuối cùng trong đợt tấn công thì tham số lastStartPointInFlexibleAttack == true, ngược lại là false
                            if(i == numBullet/2 - 0.5f) PerformAttackFlexible(bullet.transform, true, resetAttack);
                            else PerformAttackFlexible(bullet.transform, false, resetAttack);
                        }, startPoint, newBarrels, j, target, weaponPoolObject, i));

                        SceneGameManager.instance.StartCoroutine(CreateAndControlOneBullet((bullet) => {
                            if(bullet == null) return;
                            //Nếu i là số thứ tự của cặp đạn cuối cùng trong đợt tấn công thì tham số lastStartPointInFlexibleAttack == true, ngược lại là false
                            if(i == numBullet/2 - 0.5f) PerformAttackFlexible(bullet.transform, true, resetAttack);
                            else PerformAttackFlexible(bullet.transform, false, resetAttack);
                        }, startPoint, newBarrels, j, target, weaponPoolObject, -i));
                    } 
                }
                yield return new WaitForSeconds(attackRate);
            }
    }
}