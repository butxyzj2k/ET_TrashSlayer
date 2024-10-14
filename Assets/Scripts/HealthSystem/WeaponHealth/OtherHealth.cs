using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherHealth : ShieldHealth
{

    public bool canChangeScale = false;
    public override void WeaponTakeHit(float damage, GameObject receiver)
    {
        if(canChangeScale){
            Vector3 scale = gameObject.transform.localScale;
            if(receiver.transform.position.x > gameObject.transform.position.x){
                scale.x = -1;
            }
            else{
                scale.x = 1;
            }
            gameObject.transform.localScale = scale;
            gameObject.GetComponentInParent<Animator>().SetTrigger("Kicked");
        }
        base.WeaponTakeHit(damage, receiver);
    }



    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<IHitttable>() != null && other.gameObject.GetComponent<Health>() && !other.gameObject.CompareTag("Player")){
            TakeHit(1, other.gameObject);
        }
    }
}