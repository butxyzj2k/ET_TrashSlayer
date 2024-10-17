using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : StaticObjectMovement
{
    [SerializeField] private bool canRotate = true;

    public override void ObjectMovementAnim()
    {
        if(canRotate) RotateObjectInMovement();
    }
}