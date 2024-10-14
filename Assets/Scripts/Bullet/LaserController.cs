using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : WeaponController
{
    private void Update() {
        weaponHealth.DestroyObjectAfterTimeExit(gameObject);
        weaponHealth.DestroyObjectWhenOwnerDead(gameObject);
        weaponMovement.ObjectMovementAnim();
    }
}