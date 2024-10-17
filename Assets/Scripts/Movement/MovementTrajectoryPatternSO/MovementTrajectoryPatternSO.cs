using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementTrajectoryPatternSO : ScriptableObject
{
    public abstract void PerformMovement(Rigidbody2D _rb2d, float _speed, Vector3 _dir, ref bool firstTimeMove);
}
