using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff/AddRangeTakeBulletOG")]
public class AddRangeTakeBulletOGSkillPatternSO : SkillPatternSO
{
    public override void PerformSkill()
    {
        GameObject rangeTakeBulletOGObject = null;

        foreach(Transform child in PlayerController.instance.playerMovement.transform){
            if(string.Compare(child.name, "RangeTakeBulletOG") != 0) continue;
            rangeTakeBulletOGObject = child.gameObject;
        }

        rangeTakeBulletOGObject.SetActive(true);
    }
}
