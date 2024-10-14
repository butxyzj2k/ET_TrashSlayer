using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffectHealth : WeaponHealth
{
    [SerializeField] protected float damgeEffect;
    [SerializeField] private Collider2D areaCollider2D;

    public float DamgeEffect { get => damgeEffect; set => damgeEffect = value; }

    public override void WeaponTakeHit(float damage, GameObject receiver)
    {
        if(receiver.GetComponent<ObjectAttack>() && !receiver.CompareTag(objectOwner)){
            StartCoroutine(AreaDamageTakeHit(damage, receiver));
        }
    }

    IEnumerator AreaDamageTakeHit(float damage, GameObject receiver){
        while(true){
            Collider2D receiverCollider;
            if(receiver.GetComponent<Collider2D>()){
                receiverCollider = receiver.GetComponent<Collider2D>();
            }
            else{
                receiverCollider = receiver.GetComponentInChildren<Collider2D>();
            }
            if(areaCollider2D.IsTouching(receiverCollider)){
                if(string.Compare(objectOwner, "Player") == 0 && damage > 0){
                    // DamagePopUp.CreateDamagePopUp(damage, receiver.transform.position + new Vector3(-1 * 0.7f * receiver.transform.localScale.x, 0.4f), false);
                }
                receiver.GetComponent<Health>().CurentHealth -= damage;
                foreach(HittingEffectsSO hittingEffectsSO in hittingObjectEffectSOs){
                    HittingEffectsPerform(gameObject, receiver, hittingEffectsSO);
                }
                receiver.GetComponent<Health>().HealthManager();                
            }
            else{
                break;
            }
            yield return new WaitForSeconds(2);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<IHitttable>() != null && other.gameObject.GetComponent<Health>()){
            TakeHit(damgeEffect, other.gameObject);
        }
        else{
            if(other.gameObject.transform.parent != null){
                GameObject otherParent = other.gameObject.transform.parent.gameObject;
                if(otherParent.GetComponent<IHitttable>() != null && otherParent.GetComponent<Health>()){
                    TakeHit(damgeEffect, otherParent);
                }
            }
        }
    }
}
