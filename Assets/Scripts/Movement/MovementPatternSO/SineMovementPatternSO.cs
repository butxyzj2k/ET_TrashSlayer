using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement/SineMovement")]
public class SineMovementPatternSO : MovementPatternSO
{
    [SerializeField] protected float amplitude;
    [SerializeField] protected float frequently;
    public override void PerformMovement(Rigidbody2D _rb2d, float _speed, Vector3 _dir, ref bool firstTimeMove)
    {
        Vector3 dirNor = new Vector3(-_dir.y, _dir.x, 0).normalized;
        float offsetY = Mathf.Sin(Time.time * frequently) * amplitude;
        Vector3 dir = dirNor * offsetY + _dir;
        _rb2d.velocity = _speed * Time.fixedDeltaTime * dir;
    }
}
