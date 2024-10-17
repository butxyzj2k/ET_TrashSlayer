using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjectMovement : ObjectMovement
{
    public override void ObjectMovementAnim()
    {
        Debug.Log("No override movement anim");
    }

    public override void PerformMovement()
    {
        if(canMove) movementTrajectoryPatternSO.PerformMovement(rb2d, currentSpeed ,rb2d.transform.up, ref firstTimeMove);
    }
}
