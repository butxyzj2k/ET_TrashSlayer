using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : StaticObjectMovement, IRotatableObjectMovement
{
    [SerializeField] protected bool canRotate = true;
    [SerializeField] protected float rotateSpeed = 0.75f;
    [SerializeField] protected Transform objectSprite;

    public bool CanRotate { get => canRotate;}
    public float RotateSpeed { get => rotateSpeed;}
    public Transform ObjectSprite { get => objectSprite; set => objectSprite = value; }

    public override void ObjectMovementAnim()
    {
        ((IRotatableObjectMovement)this).RotateObjectInMovement();
    }
}