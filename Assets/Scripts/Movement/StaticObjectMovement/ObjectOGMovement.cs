using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOGMovement : StaticObjectMovement
{
    private void OnEnable() {
        canMove = false;
    }

     public override void PerformMovement()
    {
        if(canMove){
            Vector3 dir = PlayerController.instance.transform.position - transform.position;
            movementTrajectoryPatternSO.PerformMovement(rb2d, currentSpeed, dir, ref firstTimeMove);
        }
    }

    public override void ObjectMovementAnim()
    {
        if(canMove) RotateObjectInMovement();
    }
}
