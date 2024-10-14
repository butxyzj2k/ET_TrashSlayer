using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldAndMeeleeMovement : StaticObjectMovement, IPersueObjectMovement
{
    [SerializeField] protected bool isMotionless;

    protected Transform targetTransform;

    protected bool canChangeScale = true;

    public bool CanChangeScale => canChangeScale;

    public Transform TargetTransform { get => targetTransform; set => targetTransform = value; }

    public bool IsMotionless { get => isMotionless; set => isMotionless = value; }

    public Vector3 GetDirToTargetTransform()
    {
        throw new System.NotImplementedException();
    }

    public override void PerformMovement()
    {
        if(isMotionless) return;
        rb2d.transform.position = targetTransform.transform.position;
    }

    public override void ObjectMovementAnim(){
        transform.localScale = targetTransform.transform.localScale;
    }
}