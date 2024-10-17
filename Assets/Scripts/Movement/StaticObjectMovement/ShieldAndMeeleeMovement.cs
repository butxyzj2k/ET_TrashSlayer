using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldAndMeeleeMovement : StaticObjectMovement
{
    [SerializeField] protected bool isMotionless;

    public bool IsMotionless { get => isMotionless; set => isMotionless = value; }

    public override void PerformMovement()
    {
        rb2d.transform.position = targetTransform.transform.position;
    }

    public override void ObjectMovementAnim(){
        transform.localScale = targetTransform.transform.localScale;
    }
}