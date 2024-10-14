using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HittingEffectsSO/SlowDownHittingEffectSO")]
public class SlowDownHittingEffectSO : TimedHittingEffectSO
{
    [SerializeField] float slowRate;

    public override void HittingEffectsPerform(GameObject sender, GameObject _receiver)
    {
        if(_receiver.GetComponent<IHaveHealth>().CurrentHealth <= 0 || !_receiver.GetComponent<ObjectMovement>()) return;
        
        base.HittingEffectsPerform(sender, _receiver);
    }

    protected override IEnumerator ControlHittedObjectInHittingEffectCourotine(GameObject hittedObject){
        CreateStateVFX(hittedObject);

        float time = 0;
        while(time <= timeEffect){
            if(!hittedObject.activeInHierarchy) break;
            time += Time.deltaTime;
            
            var objectMovement =  hittedObject.GetComponent<ObjectMovement>();
            objectMovement.CurrentSpeed = objectMovement.BaseSpeed / slowRate;
            
            yield return null;
        }
        SceneGameManager.instance.StartCoroutine(ResetHittingEffects(hittedObject));
    }

    protected override IEnumerator ResetHittingEffects(GameObject _receiver)
    {
        _receiver.GetComponent<ObjectMovement>().CurrentSpeed =  _receiver.GetComponent<ObjectMovement>().BaseSpeed;
        yield return null;
    }
}
