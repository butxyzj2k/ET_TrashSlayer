using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff/BoostDamageInAreaDamage")]
public class BoostDamageInAreaDamageSkillPatternSO : SkillPatternSO
{
    [SerializeField] float BoostDamageInAreaDamageRate;
    Action<KeyValuePair<string, object>> IncreaseDamgeInAreaDamageRateDelegate;

    public override void PerformSkill()
    {
        IncreaseDamgeInAreaDamageRate();
        
        IncreaseDamgeInAreaDamageRateDelegate = (param) => {
            IncreaseDamgeInAreaDamageRate();
        };

        Obsever.AddListener(EventID.SCENE_OnStart, IncreaseDamgeInAreaDamageRateDelegate);
    }

    public void IncreaseDamgeInAreaDamageRate(){
        SceneGameManager.instance.StartCoroutine(IncreaseDamgeInAreaDamageRateCourotine());
    }

    IEnumerator IncreaseDamgeInAreaDamageRateCourotine(){
        GlassesAreaPoolObject glassesAreaPoolObject = null;

        while (glassesAreaPoolObject == null)
        {
            glassesAreaPoolObject = FindObjectOfType<GlassesAreaPoolObject>();
            if (glassesAreaPoolObject == null)
            {
                Debug.LogWarning("GlassesAreaPoolObject not found, retrying...");
                yield return null;
            }
        }
        foreach(Transform child in glassesAreaPoolObject.transform){
            float curentDmg = child.GetComponent<AreaEffectHitting>().Damage;
            curentDmg = curentDmg + curentDmg * BoostDamageInAreaDamageRate;
            child.GetComponent<AreaEffectHitting>().Damage = curentDmg;
        }
    }
}
