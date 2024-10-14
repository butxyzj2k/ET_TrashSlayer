using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PersueBulletMovement : StaticObjectMovement, IPersueObjectMovement
{
    protected Transform targetTransform;

    protected bool canChangeScale = true;

    public bool CanChangeScale => canChangeScale;

    public Transform TargetTransform { get => targetTransform; set => targetTransform = value; }

    private void Awake() {
        targetTransform = PlayerController.instance.transform;
    }

    public Vector3 GetDirToTargetTransform()
    {
        return (targetTransform.position - transform.position).normalized;
    }

    public override void PerformMovement()
    {
        rb2d.transform.up = GetDirToTargetTransform();
        base.PerformMovement();
    }

    public override void ObjectMovementAnim(){
        ((IPersueObjectMovement)this).PersueObjectChangeScale(gameObject);
    }
}
