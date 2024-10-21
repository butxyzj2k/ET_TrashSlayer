using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[CreateAssetMenu(menuName = "Attack/CreatePersueBulletAttackPatternSO")]
public class CreatePersueBulletAttackPatternSO : UnSymmetricalAttackPatternSO
{

    protected override void SetAdvancedAttackPatternVariable()
    {
        numDir = 1;
        numWaveAttack = 1;
    }

    protected override IEnumerator CreateAndControlOneBullet(Action<GameObject> PerformAttackFlexibleAction, Transform startPoint, List<MultiDirAttackExtensionCode.NewTransform> newBarrels, int indexBarrel, Vector3 target, PoolObject weaponPoolObject, int bulletIndex){
        int randomAngle = UnityEngine.Random.Range(0, 360);
        //Tạo ra 1 persueBullet
        GameObject persueBullet = weaponPoolObject.GetObjectInPool(startPoint.position, startPoint.rotation * Quaternion.Euler(0, 0, randomAngle), () => {
            Dictionary<string, object> data = new()
                {
                    { "startPoint", startPoint}
                };
            return data;
        });
        if(persueBullet.TryGetComponent<PersueBulletMovement>(out var persueBulletMovement)){
            //tempTarget có được sử dụng làm 1 trong ba điểm để tạo ra đường cong Bezier làm quỹ đạo cho PersueBullet
            Vector3 posTempTarget = startPoint.position + startPoint.rotation * Quaternion.Euler(0, 0, randomAngle) * new Vector3(0, 3, 0);
            Vector3 posOrin = startPoint.position;
            Vector3 posTarget = startPoint.position + (target - startPoint.position).normalized * 6;
            float time = 0;
            //Tạo ra một newTargetTransform có tác dụng lưu trữ transform của các điểm trong đường cong Bezier để persueBullet có thể di chuyển theo
            GameObject newTargetTransform = new();
            //Thực hiện việc di chuyển PersueBullet theo BezierCurve
            while(time <= 1){
                time += Time.deltaTime;
                newTargetTransform.transform.position = CalculateBezierPoint(time, posOrin, posTempTarget, posTarget);
                persueBulletMovement.TargetTransform = newTargetTransform.transform;
                yield return null;        
            }
            Destroy(newTargetTransform);
            persueBulletMovement.TargetTransform = PlayerController.instance.transform;
        }
        PerformAttackFlexibleAction(persueBullet);
    }

    //Tính toán đường cong Bezier tuyến tính
    public static Vector3 Lerp(Vector3 a, Vector3 b, float t){
        return a + (b - a) * t;
    }

    //Tính toán đường cong Bezier bậc 2
    public Vector3 CalculateBezierPoint(float t, Vector3 originalPos, Vector3 tempTarget, Vector3 target)
    {
        Vector3 p0 = Lerp(originalPos, tempTarget, t);
        Vector3 p1 = Lerp(tempTarget, target, t);

        return Lerp(p0, p1, t);
    }
}
