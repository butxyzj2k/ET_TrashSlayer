using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/CreateLaserBeamAttackPatternSO")]
public class CreateLaserBeamAttackPatternSO : UnSymmetricalAttackPatternSO
{
    protected override void SetAdvancedAttackPatternVariable()
    {
        numBullet = 1;
        numWaveAttack = 1;
    }

    protected override IEnumerator CreateBulletsInAttack(Transform startPoint, Vector3 target, PoolObject weaponPoolObject, Action resetAttack)
    {
        yield return SceneGameManager.instance.StartCoroutine(base.CreateBulletsInAttack(startPoint, target, weaponPoolObject, resetAttack) );
        //Chờ 1 khoảng TimeExit của Laser sau đó mới kết thúc Courotine (Để resetObjectAttack)
        yield return new WaitForSeconds(weaponPoolObject.GetObjectInPool(new Vector3(0, 0, 0), Quaternion.identity, () => {
            Dictionary<string, object> data = new()
                {
                    { "activeBullet", false},
                };
            return data;
        }).GetComponent<IWeaponHealth>().TimeExist);
    }

    protected override IEnumerator CreateAndControlOneBullet(Action<GameObject> PerformAttackFlexibleAction, Transform startPoint, List<MultiDirAttackExtensionCode.NewTransform> newBarrels, int indexBarrel, Vector3 target, PoolObject weaponPoolObject, int bulletIndex){
        //Tạo 1 laser theo vị trí và hướng của barrel có index i trong danh sách newBarrels
        GameObject laserBeam = weaponPoolObject.GetObjectInPool(newBarrels[indexBarrel].position, Quaternion.identity, () => {
            Dictionary<string, object> data = new()
                {
                    { "activeBullet", true},
                    { "startPoint", startPoint}
                };
            return data;
        });   
        //Thực hiện vòng lặp Update
        while(true){
            //Nếu Object thực hiện Attack không còn hoạt động
            if(!startPoint.gameObject.activeInHierarchy) {
                //Thực hiện việc tắt hoạt động của laser sau đó thoát khỏi courotine
                laserBeam.SetActive(false);
                break;
                //Sau khi thoát khỏi Courotine, startPoint có thể vẫn đang IsAttack, tuy nhiên vì nó đã ngưng hoạt động nên sẽ không có ảnh hưởng nhiều 
            }
            if(!laserBeam.activeInHierarchy) break;
            //Cập nhật vị trí và hướng xoay của laser theo vị trí và hướng của barrel có index i trong danh sách newBarrels 
            laserBeam.transform.SetPositionAndRotation(newBarrels[indexBarrel].position, newBarrels[indexBarrel].rotation);
            yield return null;
        }
        yield return null;
        PerformAttackFlexibleAction(laserBeam);
    }
}
