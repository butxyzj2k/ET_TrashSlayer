using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Attack/BasicMeleeWeaponAttack")]
public class BasicMeleeWeaponAttackSO  : BasicAttackPatternSO
{

    public override Quaternion GetDir(Transform startPoint ,Vector3 target){
        Vector3 dir = startPoint.position - target;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
 
        if(-45 < angle && angle < 45){
            angle = -angle;
        }
        else if(angle < -135){
            angle = 180 - Mathf.Abs(angle);
        }
        else if(angle > 135){
            angle = -(180 - angle);
        }
        else if(angle > 45){
            angle = angle < 90 ? 45 : -45;
        }
        else if(angle < -45){
            angle = angle > -90 ? -45 : 45;
        }      
        
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        return rotation;
    }
}
