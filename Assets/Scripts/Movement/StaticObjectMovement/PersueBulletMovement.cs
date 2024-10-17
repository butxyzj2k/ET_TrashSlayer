using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PersueBulletMovement : StaticObjectMovement
{

    public override void PerformMovement()
    {
        rb2d.transform.up = (targetTransform.position - transform.position).normalized;
        base.PerformMovement();
    }

    public override void ObjectMovementAnim(){
        ObjectMovementChangeScale();
    }
}
