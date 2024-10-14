using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnSymmetricalAttackPatternSO : AdvancedAttackPatternSO
{

    protected override IEnumerator CreateBulletsBetweenWave(Transform startPoint, List<MultiDirAttackExtensionCode.NewTransform> newBarrels ,Vector3 target, PoolObject weaponPoolObject, Action resetAttack){
        //Duyệt qua từng viên đạn
        for(int i = 0; i < numBullet; i++){
            //Nếu Object thực hiện Attack không còn hoạt động, ngừng tấn công
            if(!startPoint.gameObject.activeInHierarchy) break;
            for(int j = 0; j < newBarrels.Count; j++){
                //Thực hiện tạo đạn 
                SceneGameManager.instance.StartCoroutine(CreateAndControlOneBullet((bullet) => {
                    if(bullet == null){
                        Debug.Log("NULL");
                        return;
                    } 
                     //Nếu i là viên đạn cuối cùng trong đợt tấn công thì tham số lastStartPointInFlexibleAttack == true, ngược lại là false
                    if(i == numBullet-1) PerformAttackFlexible(bullet.transform, true, resetAttack);
                    else PerformAttackFlexible(bullet.transform, false, resetAttack);
                }, startPoint, newBarrels, j, target, weaponPoolObject, i));
            yield return new WaitForSeconds(attackRate);
            }
        } 
    }
}