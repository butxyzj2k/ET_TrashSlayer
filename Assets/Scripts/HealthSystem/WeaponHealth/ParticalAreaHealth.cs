using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalAreaHealth : AreaEffectHealth
{
    ParticleSystem particleDamageSystem;
    bool canTakeDamage = true;

    private void Awake() {
        particleDamageSystem = GetComponent<ParticleSystem>();
        particleDamageSystem.trigger.AddCollider(PlayerController.instance.transform);
    }

    private void OnParticleTrigger() {
        if(particleDamageSystem == null) return;
        TakeHit(damgeEffect, particleDamageSystem.trigger.GetCollider(0).gameObject);
    }

    public override void WeaponTakeHit(float damage, GameObject receiver)
    {
        if(canTakeDamage && receiver.GetComponent<ObjectAttack>() && !receiver.CompareTag(objectOwner)){
            canTakeDamage = false;
            receiver.GetComponent<Health>().CurentHealth -= damage;
                foreach(HittingEffectsSO hittingEffectsSO in hittingObjectEffectSOs){
                    HittingEffectsPerform(gameObject, receiver, hittingEffectsSO);
                }
            receiver.GetComponent<Health>().HealthManager();  
            StartCoroutine(resetCanTakeDamage()); 
        }
    }

    IEnumerator resetCanTakeDamage(){
        yield return new WaitForSeconds(1);
        canTakeDamage = true;
    }
}

