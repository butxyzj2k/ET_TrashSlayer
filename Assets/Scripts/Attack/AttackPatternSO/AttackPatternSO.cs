using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackPatternSO : ScriptableObject
{
    //Lưu trữ thời gian hồi chiêu của mỗi loại AttackPattern
    [SerializeField] protected float attackDelay;
    //Lưu trữ thông tin của những đòn tấn công có khả năng tạo thêm AttackPattern ở từng bullet trong đợt tấn công
    [Serializable]
    public struct FlexibleAttack{
        public bool isFlexible;
        public float offsetAngle;
        public GameObject flexibleProjectilePrefab;
        public PoolObject flexibleProjectilePoolObject;
        public AttackPatternSO attackPatternSOFlexible;
    }
    //Chỉnh sửa trong Inspector
    [SerializeField] protected FlexibleAttack flexibleAttack;

    public float AttackDelay { get => attackDelay; private set => attackDelay = value; }

    public virtual Quaternion GetDir(Transform startPoint,Vector3 target){
        Vector3 dir = (target - startPoint.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) *  Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90);
        return rotation;
    
    }

    //PerformAttack được gọi trong các đòn đánh thường
    public virtual void PerformAttack(Transform startPoint ,Vector3 target, PoolObject weaponPoolObject, Action resetAttack)
    {
        SceneGameManager.instance.StartCoroutine(PerformAttackCourotine(startPoint, target, weaponPoolObject, resetAttack));
    }
    //PerformAttack được gọi từ các PerformAttackFlexible
    public virtual void PerformAttack(Transform startPoint ,Vector3 target, PoolObject weaponPoolObject, bool lastStartPointInFlexibleAttack, Action resetAttack)
    {
        SceneGameManager.instance.StartCoroutine(PerformAttackCourotine(startPoint, target, weaponPoolObject, lastStartPointInFlexibleAttack, resetAttack));
    }

    IEnumerator PerformAttackCourotine(Transform startPoint ,Vector3 target, PoolObject weaponPoolObject, Action resetAttack){
        yield return SceneGameManager.instance.StartCoroutine(CreateBulletsInAttack(startPoint, target, weaponPoolObject, resetAttack));
        //Nếu PerformAttack được gọi trong các đòn đánh thường, startPoint sẽ là người hoặc quái => lastStartPointInFlexibleAttack không có ý nghĩa, truyền đại
        SceneGameManager.instance.StartCoroutine(ResetObjectAttack(startPoint, false, resetAttack));
    }

    IEnumerator PerformAttackCourotine(Transform startPoint ,Vector3 target, PoolObject weaponPoolObject, bool lastStartPointInFlexibleAttack, Action resetAttack){
        yield return SceneGameManager.instance.StartCoroutine(CreateBulletsInAttack(startPoint, target, weaponPoolObject, resetAttack));
        //Nếu PerformAttack được gọi trong các đòn đánh flexible, startPoint sẽ bullet => lastStartPointInFlexibleAttack truyền vào dựa theo từng bullet được instantiate
        SceneGameManager.instance.StartCoroutine(ResetObjectAttack(startPoint, lastStartPointInFlexibleAttack, resetAttack));
    }

    //Courotine được dùng để tạo các bullet theo pattern của từng AttackPattern
    protected virtual IEnumerator CreateBulletsInAttack(Transform startPoint, Vector3 target, PoolObject weaponPoolObject, Action resetAttack){
        yield return null;
        // default sẽ là tạo 1 viên đạn 
        startPoint.rotation = GetDir(startPoint, target);
        GameObject bullet = weaponPoolObject.GetObjectInPool(startPoint.position, startPoint.rotation, () => {
            Dictionary<string, object> data = new()
                {
                    { "activeBullet", true},
                    { "startPoint", startPoint}
                };
            return data;
        });
        PerformAttackFlexible(bullet.transform, true, resetAttack);
    }

    public virtual void SetFlexibleProjectilePoolObject(){
        //Nếu flexibleProjectilePrefab == null => bỏ qua
        if(flexibleAttack.flexibleProjectilePrefab == null) return;
        //Nếu flexibleProjectilePoolObject đã được gán trước đó => bỏ qua
        if(flexibleAttack.flexibleProjectilePoolObject != null) return;
        //Thực hiện việc gán PoolObject
        PoolObject[] poolObjects = FindObjectsOfType<PoolObject>();
        foreach(PoolObject poolObject in poolObjects){
            if(string.Compare(poolObject.PoolObjectName, flexibleAttack.flexibleProjectilePrefab.name) == 0){
                flexibleAttack.flexibleProjectilePoolObject = poolObject;
            }
        }  
    }

    public virtual void PerformAttackFlexible(Transform startPoint, bool lastStartPointInFlexibleAttack, Action resetAttack){
        //Nếu AttackPattern này flexible  thì bắt đầu tấn công
        if(flexibleAttack.isFlexible){
            //Thử gán flexibleProjectilePoolObject
            SetFlexibleProjectilePoolObject();
            if(flexibleAttack.flexibleProjectilePoolObject != null){
                //Thực hiện attackPatternSOFlexible theo từng bullet-startPoint trong attackPattern cùng với tham số lastStartPointInFlexibleAttack được truyền vô
                flexibleAttack.attackPatternSOFlexible.PerformAttack(startPoint, startPoint.position + startPoint.rotation * Quaternion.Euler(0, 0, flexibleAttack.offsetAngle) * new Vector3(0, 1, 0), flexibleAttack.flexibleProjectilePoolObject, lastStartPointInFlexibleAttack, resetAttack);
            }
        }
    }

    protected virtual IEnumerator ResetObjectAttack(Transform startPoint, bool lastStartPointInFlexibleAttack, Action resetAttack){
        //Nếu là flexibleAttack thì bỏ qua không Reset
        if(flexibleAttack.isFlexible){
            yield return null;
        } 
        //Nếu không phải => Kiểm tra xem đây có phải là attack gốc không hay là attack từ flexibleAttack 
        else{

            //Nếu là Bullet cuối hoặc là vật thể động thì resetAttack
            if(lastStartPointInFlexibleAttack || startPoint.GetComponentInParent<ObjectAttack>() || startPoint.GetComponent<ObjectAttack>()){
                resetAttack();
            }
        }
    }


}
