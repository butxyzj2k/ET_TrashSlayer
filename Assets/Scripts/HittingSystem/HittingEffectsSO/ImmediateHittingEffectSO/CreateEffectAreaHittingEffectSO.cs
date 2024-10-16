using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(menuName = "HittingEffectsSO/CreateEffectAreaHittingEffectSO")]
public class CreateEffectAreaHittingEffectSO : ImmediateHittingEffectSO
{
    [SerializeField] GameObject areaEffectPrefab;

    public override void HittingEffectsPerform(GameObject sender, GameObject _receiver)
    {

        PoolObject.GetPoolObject(areaEffectPrefab).GetObjectInPool(sender.transform.position, quaternion.identity, () => {
            Dictionary<string, object> data = new()
            {
                { "activeBullet", true },
                { "startPoint", sender.transform }
            };
            return data;
        });

    }
}
