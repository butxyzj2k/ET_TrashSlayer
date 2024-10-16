using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedHittingEffectSO : HittingEffectsSO
{
    [SerializeField] protected float timeEffect;
    [SerializeField] protected float defaultTimeEffect;
    [SerializeField] protected GameObject stateVFXPrefab;
    public float TimeEffect { get => timeEffect; set => timeEffect = value;}
    public float DefaultTimeEffect { get => defaultTimeEffect; set => defaultTimeEffect = value; }

    public override void HittingEffectsPerform(GameObject sender, GameObject _receiver)
    {
        SceneGameManager.instance.StartCoroutine(ControlHittedObjectInHittingEffectCourotine(_receiver));
    }

    protected virtual IEnumerator ControlHittedObjectInHittingEffectCourotine(GameObject hittedObject){
        Debug.Log("Not override ControlHittedObjectInHittingEffect");
        yield return null;
    }

    protected virtual IEnumerator ResetHittingEffects(GameObject _receiver){
        yield return null;
        Debug.Log("Not override ResetHittingEffects");
    }

    protected void CreateStateVFX(GameObject hittedObject){
        PoolObject.GetPoolObject(stateVFXPrefab).GetObjectInPool(hittedObject.transform.position, Quaternion.identity, () => {
            Dictionary<string, object> data = new()
                {
                    { "targetTransform", hittedObject.transform},
                    { "timeToPlayVFX", timeEffect}
                };
            return data;
        });
    }
}