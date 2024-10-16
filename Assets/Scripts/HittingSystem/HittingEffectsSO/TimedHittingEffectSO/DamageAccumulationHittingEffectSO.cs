using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HittingEffectsSO/DamageAccumulationHittingEffectSO")]
public class DamageAccumulationHittingEffectSO : TimedHittingEffectSO
{

    public override void HittingEffectsPerform(GameObject sender, GameObject _receiver)
    {
        if(_receiver.GetComponent<IHaveHealth>().CurrentHealth <= 0 || !_receiver.GetComponent<DynamicObjectHitting>()) return;
        
        base.HittingEffectsPerform(sender, _receiver);
    }

    protected override IEnumerator ControlHittedObjectInHittingEffectCourotine(GameObject hittedObject){
        DynamicObjectHitting dynamicObjectHitting = hittedObject.GetComponent<DynamicObjectHitting>();
        float hurtTime = hittedObject.GetComponent<ObjectGetClipIn4>().hurtTime;
        float oldHealth = dynamicObjectHitting.CurrentHealth;
        yield return new WaitForSeconds(hurtTime + 0.05f);
        
        CreateStateVFX(hittedObject);

        float time = 0;
        while(time <= timeEffect){
            time += Time.deltaTime;
            if(dynamicObjectHitting.IsHurt){
                float newHealth = dynamicObjectHitting.CurrentHealth;
                float additionalDamage = oldHealth - newHealth;
                ((IHaveHealth)dynamicObjectHitting).GetHit(additionalDamage);

                DamagePopUpVisualEffect.CreateDamagePopUp(hittedObject.transform.position, Quaternion.identity, () => {
                    Dictionary<string, object> data = new()
                        {
                            { "isCritical", true },
                            { "value", additionalDamage }
                        };
                    return data;
                });
                break;
            }
            yield return null;
        }
        
        SceneGameManager.instance.StartCoroutine(ResetHittingEffects(hittedObject));
    }    
}
