using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOGMovement : StaticObjectMovement, IRotatableObjectMovement
{
    [SerializeField] protected bool canRotate = false;
    [SerializeField] protected float rotateSpeed = 0.75f;
    [SerializeField] protected Transform objectSprite;

    public bool CanRotate { get => canRotate;}
    public float RotateSpeed { get => rotateSpeed;}
    public Transform ObjectSprite { get => objectSprite; set => objectSprite = value; }
    private void OnEnable() {
        canMove = false;
        canRotate = false;
    }

     public override void PerformMovement()
    {
        if(canMove){
            canRotate = true;
            Vector3 dir = PlayerController.instance.transform.position - transform.position;
            movementPatternSO.PerformMovement(rb2d, currentSpeed, dir, ref firstTimeMove);
        }
    }

    public override void ObjectMovementAnim()
    {
        if(canMove) ((IRotatableObjectMovement)this).RotateObjectInMovement();
    }
}
