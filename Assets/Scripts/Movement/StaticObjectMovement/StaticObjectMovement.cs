using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjectMovement : ObjectMovement
{

    // private void FixedUpdate() {
    //     PerformMovement();
    // }
    // private void Update() {
    //     ObjectMovementAnim();
    // }
    public override void PerformMovement()
    {
        if(canMove) movementPatternSO.PerformMovement(rb2d, currentSpeed ,rb2d.transform.up, ref firstTimeMove);
    }

    public override void ObjectMovementAnim(){
        
    }
}
