using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HittingEffectsSO/StunHittingEffectsSO")]
public class StunHittingEffectsSO : TimedHittingEffectSO
{
    public override void HittingEffectsPerform(GameObject sender, GameObject _receiver)
    {
        //Nếu Object đã chết hoặc Object không phải là DynamicObject
        if(_receiver.GetComponent<IHaveHealth>().CurrentHealth <= 0 ||!_receiver.GetComponent<ObjectAttack>() || !_receiver.GetComponent<ObjectMovement>()) return;
        //Nếu đang bị stun sẵn thì không áp dụng nữa
        if(! _receiver.GetComponent<ObjectAttack>().CanResetCanAttack) return;
        
        base.HittingEffectsPerform(sender, _receiver);
    }

    protected override IEnumerator ControlHittedObjectInHittingEffectCourotine(GameObject hittedObject){
        CreateStateVFX(hittedObject);

        float time = 0;
        while(time <= timeEffect){
            if(!hittedObject.activeInHierarchy) break;
            time += Time.deltaTime;
            var objectAttack = hittedObject.GetComponent<ObjectAttack>();
            if( objectAttack.CanAttack) objectAttack.CanAttack = false;
            else objectAttack.CanResetCanAttack = false;

            var objectMovement =  hittedObject.GetComponent<ObjectMovement>();
            objectMovement.SetObjectIdelding();
            objectMovement.CanMove = false;
            yield return null;
        }
        SceneGameManager.instance.StartCoroutine(ResetHittingEffects(hittedObject));
    }

    protected override IEnumerator ResetHittingEffects(GameObject _receiver)
    {
        var objectAttack = _receiver.GetComponent<ObjectAttack>();
        objectAttack.CanResetCanAttack = true;
        objectAttack.CanAttack = true;

        _receiver.GetComponent<ObjectMovement>().CanMove = true;

        yield return null;
    }
}
