using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement/BasicMovement")]
public class BasicMovementPattern : MovementPatternSO
{
    public override void PerformMovement(Rigidbody2D _rb2d, float _speed, Vector3 _dir, ref bool firstTimeMove)
    {
        _rb2d.velocity = (Vector2) (_speed * Time.fixedDeltaTime * _dir);
    }
}
