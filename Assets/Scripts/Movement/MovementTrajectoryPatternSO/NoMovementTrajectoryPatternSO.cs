using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MovementTrajectory/NoMovementTrajectoryPattern")]
public class NoMovementTrajectoryPatternSO : MovementTrajectoryPatternSO
{
    public override void PerformMovement(Rigidbody2D _rb2d, float _speed, Vector3 _dir , ref bool firstTimeMove)
    {
       
    }
}
