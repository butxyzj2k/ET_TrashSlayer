using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/ShieldAttackPatternSO")]
public class ShieldAttackPatternSO : BasicAttackPatternSO
{
    // [SerializeField] private bool isMotionless;

    protected override IEnumerator CreateBulletsInAttack(Transform startPoint, Vector3 target, PoolObject weaponPoolObject, Action resetAttack)
    {
        Transform parentTransform = startPoint.transform.parent.transform;
        //Tạo shield tại parentTransform của startPoint
        GameObject shield = weaponPoolObject.GetObjectInPool(parentTransform.position, parentTransform.rotation, () => {
            Dictionary<string, object> data = new()
                {
                    { "activeBullet", true},
                    { "startPoint", parentTransform}
                };
            return data;
        });
        while (shield == null)
        {
            shield = weaponPoolObject.GetObjectInPool(parentTransform.position, parentTransform.rotation, () => {
                Dictionary<string, object> data = new()
                    {
                        { "activeBullet", true},
                        { "startPoint", parentTransform}
                    };
                return data;
            });
            yield return null;
        }
        
        if (!shield.GetComponent<ShieldAndMeeleeMovement>().IsMotionless)
        {
            yield break;
        }

        while (true)
        {
            if(!parentTransform.gameObject.activeInHierarchy){
                shield.SetActive(false);
                break;
            }
            if (!shield.activeInHierarchy)
            {
                break;
            }
            yield return null;
        }
    }


}
