using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement/ThunderMovement")]
public class ThunderMovementSO : SineMovementPatternSO
{
    public override void PerformMovement(Rigidbody2D _rb2d, float _speed, Vector3 _dir, ref bool firstTimeMove)
    {
        Vector3 dirNor = new Vector3(-_dir.y, _dir.x, 0).normalized;
        if(Time.time % frequently <= 0.02f){
            int intensity = 2;
            if(firstTimeMove){
                intensity = 1;
                firstTimeMove = false;
            }
            _rb2d.position = _rb2d.position 
                                + amplitude * Mathf.Sign(Mathf.Sin((Mathf.PI/frequently)*Time.time)) * intensity * (Vector2)dirNor 
                                +(Vector2)_dir.normalized;
        }       
    }
}