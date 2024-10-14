using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeBulletWeaponHealth : WeaponHealth
{
    // [SerializeField] float normalDamage = 1;
    // [SerializeField] float extraDamage = 2;

    // [SerializeField] string enemyTarget;

    // public override void WeaponTakeHit(float damage, GameObject receiver)
    // {
    //     if(receiver.gameObject.GetComponent<Health>()){
    //         if(receiver.GetComponent<ObjectAttack>() && !receiver.CompareTag(objectOwner)){
    //             ObjectAttack objectAttack = currentOwner.GetComponent<ObjectAttack>();
    //             if(objectAttack != null){
    //                 damage = string.Compare(enemyTarget, receiver.name) != 0 
    //                 ? normalDamage * objectAttack.DamageRate
    //                 : extraDamage * objectAttack.DamageRate;
    //             }

    //             if(string.Compare(objectOwner, "Player") == 0){
    //                 DamagePopUp.CreateDamagePopUp(damage, receiver.transform.position + new Vector3(-1 * 0.7f * receiver.transform.localScale.x, 0.4f), damage > normalDamage);
    //             }
                
    //             receiver.GetComponent<Health>().CurentHealth -= damage;
                
    //             foreach(HittingEffectsSO hittingEffectsSO in hittingObjectEffectSOs){
    //                 HittingEffectsPerform(gameObject, receiver, hittingEffectsSO);
    //             }
    //             receiver.GetComponent<Health>().HealthManager();
    //             //SpawnVFX In Collide Pos
    //             weaponHittingVFX.SpawnHitVFX(transform.position);
    //         }
            
    //         else if(receiver.GetComponent<WeaponHealth>() && string.Compare(objectOwner, receiver.GetComponent<WeaponHealth>().ObjectOwner) != 0){
    //             receiver.GetComponent<Health>().CurentHealth -= damage;
    //             receiver.GetComponent<Health>().HealthManager();
    //             //SpawnVFX In Collide Pos
    //             weaponHittingVFX.SpawnHitVFX(transform.position);
    //             foreach(HittingEffectsSO hittingEffectsSO in hittingOtherEffectSOs){
    //                 HittingEffectsPerform(gameObject, receiver, hittingEffectsSO);
    //             }
    //         }
    //     }

    //     else if(!receiver.GetComponent<Health>() && receiver.GetComponent<IHitttable>() != null && !receiver.CompareTag(objectOwner)){
    //         foreach(HittingEffectsSO hittingEffectsSO in hittingOtherEffectSOs){
    //             HittingEffectsPerform(gameObject, receiver, hittingEffectsSO);
    //         }
    //     }
    // }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.gameObject.GetComponent<IHitttable>() != null){
    //         TakeHit(1, other.gameObject);
    //     }
        
    //     else if(other.gameObject.transform.parent != null){
    //         GameObject otherParent = other.gameObject.transform.parent.gameObject;
    //         if(otherParent.GetComponent<IHitttable>() != null){
    //             TakeHit(1, otherParent);
    //         }
    //     }
    // }
}
