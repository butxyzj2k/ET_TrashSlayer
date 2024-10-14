using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff/BoostRangeTakeBulletOG")]
public class BoostRangeTakeBulletOGSkillPatternSO : SkillPatternSO
{
    private float radiusDefault = 1.15f;
    [SerializeField] float BoostRangeTakeBulletOGRate;
    [SerializeField] float BoostRangeTakeOrbs;

    public override void PerformSkill()
    {
        
        GameObject rangeTakeBulletOGObject = null;

        foreach(Transform child in PlayerController.instance.playerMovement.transform){
            if(string.Compare(child.name, "RangeTakeBulletOG") != 0) continue;
            rangeTakeBulletOGObject = child.gameObject;
        }

        if(rangeTakeBulletOGObject.activeInHierarchy){
            CircleCollider2D rangeTakeBulletOGCollider = rangeTakeBulletOGObject.GetComponentInChildren<CircleCollider2D>();
            rangeTakeBulletOGCollider.radius = radiusDefault + radiusDefault * BoostRangeTakeBulletOGRate;   
            Debug.Log("Boost " + BoostRangeTakeBulletOGRate + "RangeTakeBulletOG");
        } 
        else{
            GameObject rangeTakeOrbObject = null;

            foreach(Transform child in PlayerController.instance.playerMovement.transform){
                if(string.Compare(child.name, "RangeTakeOrb") != 0) continue;
                    rangeTakeOrbObject = child.gameObject;
            }

            CircleCollider2D rangeTakeOrbCollider = rangeTakeOrbObject.GetComponentInChildren<CircleCollider2D>();

            rangeTakeOrbCollider.radius = radiusDefault + radiusDefault * BoostRangeTakeOrbs;    
            Debug.Log("Boost " + BoostRangeTakeOrbs + "RangeTakeOrb");
        }
    }
}
