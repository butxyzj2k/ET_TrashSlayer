using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement/NoMovementPattern")]
public class NoMovementPatternSO : MovementPatternSO
{
    public override void PerformMovement(Rigidbody2D _rb2d, float _speed, Vector3 _dir , ref bool firstTimeMove)
    {
       
    }
}
