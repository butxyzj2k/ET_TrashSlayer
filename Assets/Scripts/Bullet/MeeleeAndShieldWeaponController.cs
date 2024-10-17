using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleeAndShieldWeaponController : WeaponController
{
    private void Update() {
        weaponHealth.DestroyObjectAfterTimeExit(gameObject);
        weaponHealth.DestroyObjectWhenOwnerDead(gameObject);
    }

    public override void ReleaseObject(Vector3 position, Quaternion rotation, Func<Dictionary<string, object>> data)
    {
        base.ReleaseObject(position, rotation, data);
       weaponMovement.TargetTransform = currentOwner.transform;
        gameObject.transform.localScale = currentOwner.transform.localScale;
    }
}