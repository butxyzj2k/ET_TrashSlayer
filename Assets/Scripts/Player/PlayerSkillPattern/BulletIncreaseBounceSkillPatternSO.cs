using System;
using System.Collections;
using System.Collections.Generic;
// using UnityEditor.SearchService;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff/BulletIncreaseBounce")]
public class BulletIncreaseBounceSkillPatternSO : SkillPatternSO
{
    [SerializeField] int numberBounceIncrese;

    Action<KeyValuePair<string, object>> IncreaseBounceAllBulletBounceDelegate;

    public override void PerformSkill()
    {
        IncreaseBounceAllBulletBounce();

        IncreaseBounceAllBulletBounceDelegate = (param) => {
            IncreaseBounceAllBulletBounce();
        };

        Obsever.AddListener(EventID.SCENE_OnStart, IncreaseBounceAllBulletBounceDelegate);
    }

    public void IncreaseBounceAllBulletBounce(){
        SceneGameManager.instance.StartCoroutine(IncreaseBounceAllBulletBounceCourotine());
    }

    IEnumerator IncreaseBounceAllBulletBounceCourotine(){
        GameObject bottleBulletPoolObject = null;

        while (bottleBulletPoolObject == null)
        {
            bottleBulletPoolObject = FindObjectOfType<BottleBulletPoolObject>().gameObject;
            if (bottleBulletPoolObject == null)
            {
                Debug.LogWarning("GlassesAreaPoolObject not found, retrying...");
                yield return null;
            }
        }
        foreach(Transform child in bottleBulletPoolObject.transform){
            child.GetComponent<MeeleeBulletHitting>().MaxHealth += numberBounceIncrese;
        }
    }
}
