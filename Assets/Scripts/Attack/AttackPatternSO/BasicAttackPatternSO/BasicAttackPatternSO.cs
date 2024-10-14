using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackPatternSO : AttackPatternSO
{
    //Ghi đè lại phương thức PerformAttack flexible để không bị làm đòn tấn công Flexible
    public override void PerformAttack(Transform startPoint, Vector3 target, PoolObject weaponPoolObject, bool lastStartPointInFlexibleAttack, Action resetAttack)
    {
        Debug.Log("This Attack is not flexible attack pattern!");
    }

    protected override IEnumerator ResetObjectAttack(Transform startPoint, bool lastStartPointInFlexibleAttack, Action resetAttack)
    {
        yield return new WaitForSeconds(0.8f);
        SceneGameManager.instance.StartCoroutine(base.ResetObjectAttack(startPoint, lastStartPointInFlexibleAttack, resetAttack));
    }
}