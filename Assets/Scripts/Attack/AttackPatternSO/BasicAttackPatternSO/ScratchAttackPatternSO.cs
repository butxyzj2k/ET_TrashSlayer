using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/ScratchAttackPatternSO")]
public class ScratchAttackPatternSO : BasicAttackPatternSO
{
    [SerializeField] float boostSpeed;
    //Ghe đè lại toàn bộ PerformAttack
    public override void PerformAttack(Transform startPoint, Vector3 target, PoolObject weaponPoolObject, Action resetAttack)
    {
        //Lấy các Component cần thiết bên trong EnemyBoss
        EnemyBossMovement objectMovement = startPoint.GetComponentInParent<EnemyBossMovement>();
        EnemyBossAttack objectAttack = startPoint.GetComponentInParent<EnemyBossAttack>(); 
        //Nếu các Component null thì bỏ qua, ngừng tấn công
        if(objectMovement == null || objectAttack == null){
            return;
        }
        //Cho EnemyBoss thực hiện việc di chuyển tới Player
        objectMovement.IsMovingToPlayer = true;
        objectMovement.CanMove = true;

        SceneGameManager.instance.StartCoroutine(ControlScratch(objectMovement, objectAttack, objectMovement.BaseSpeed, startPoint, target, weaponPoolObject, resetAttack));
    }
    
    IEnumerator ControlScratch(EnemyBossMovement objectMovement, EnemyBossAttack objectAttack, float oldSpeed, Transform startPoint, Vector3 target, PoolObject weaponPoolObject, Action resetAttack){
        //Bắt đầu vòng lặp Update
        while(true){
            //Nếu như EnemyBoss phát hiện Target đang trong tầm tấn công
            if(objectAttack.CheckSightAttack()){
                //Ngừng việc di chuyển
                objectMovement.SetObjectIdelding();
                //Reset tốc độ play của animator về 1
                objectAttack.gameObject.GetComponent<Animator>().speed = 1;
                //Thực hiện Animation MeleeAttack của Object
                objectAttack.gameObject.GetComponent<Animator>().Play("MeleeAttack");
                //Lấy hướng tấn công
                startPoint.rotation = GetDir(startPoint, objectAttack.TargetAttackPosition);
                //Thực hiện Flexible Attack tại StartPoint 
                //Ở trong đòn tấn công này, bởi vì target = startPoint.position + startPoint.rotation * Quaternion.Euler(0, 0, flexibleAttack.offsetAngle) * new Vector3(0, 1, 0)
                //Nên chúng ta phải xoay StartPoint về hướng của Target trước đồng thời  flexibleAttack.offsetAngle phải bằng 0 để thực hiện chính xác đòn đánh về phía target
                PerformAttackFlexible(startPoint, true, resetAttack);
                //Chờ cho thực hiện xong hành động Attack
                yield return new WaitForSeconds(objectAttack.gameObject.GetComponent<ObjectGetClipIn4>().meeleeAttackTime - (2/9)*objectAttack.gameObject.GetComponent<ObjectGetClipIn4>().meeleeAttackTime);
                //Thực hiện Animation Idel của Object trong lúc chờ đợi Object được ResetAttack
                objectAttack.gameObject.GetComponent<Animator>().Play("Idel");
                //Reset các thông số trong ObjectMovement
                objectMovement.IsMovingToPlayer = false;
                objectMovement.CurrentSpeed = oldSpeed;
                break;
            }
            else{
                //Thực hiện việc di chuyển Object và tăng tốc độ của Animator
                objectMovement.CurrentSpeed = boostSpeed;
                objectAttack.gameObject.GetComponent<Animator>().speed = boostSpeed/oldSpeed;
                objectMovement.PerformMovement();
            }
            yield return null; 
        }
        
    }
}
