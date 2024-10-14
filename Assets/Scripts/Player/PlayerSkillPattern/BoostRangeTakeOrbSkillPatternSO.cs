using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff/BoostRangeTakeOrb")]
public class BoostRangeTakeOrbSkillPatternSO : SkillPatternSO
{
    private float radiusDefault = 1.15f;
    [SerializeField] float BoostRangeTakeOrbRate;

    public override void PerformSkill()
    {
        
        GameObject rangeTakeOrbObject = null;

        foreach(Transform child in PlayerController.instance.playerMovement.transform){
            if(string.Compare(child.name, "RangeTakeOrb") != 0) continue;
            rangeTakeOrbObject = child.gameObject;
        }

        CircleCollider2D rangeTakeOrbCollider = rangeTakeOrbObject.GetComponentInChildren<CircleCollider2D>();

        rangeTakeOrbCollider.radius = radiusDefault + radiusDefault * BoostRangeTakeOrbRate;    
        Debug.Log("Boost " + BoostRangeTakeOrbRate + "RangeTakeOrb");
    }
}
