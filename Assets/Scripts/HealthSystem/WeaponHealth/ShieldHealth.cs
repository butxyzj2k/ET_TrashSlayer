using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHealth : WeaponHealth
{
    public override void WeaponTakeHit(float damage, GameObject receiver)
    {
        if(receiver.GetComponent<WeaponHealth>() && !(string.Compare(objectOwner, receiver.GetComponent<WeaponHealth>().ObjectOwner) == 0)){
            receiver.GetComponent<Health>().CurentHealth -= damage;
            receiver.GetComponent<Health>().HealthManager();
            //SpawnVFX In Collide Pos
            weaponHittingVFX.SpawnHitVFX(receiver.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<IHitttable>() != null && other.gameObject.GetComponent<Health>()){
            TakeHit(1, other.gameObject);
        }
    }
}
