using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColliderInTutoRoomHitting : ObstacleObjectHitting{
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Do nothing");
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(!other.GetComponent<PlayerController>()) return;
        if(other.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0){
            GetComponent<BoxCollider2D>().isTrigger = false;
            ((IHitablee)this).HittingEffectsPerform(gameObject, other.gameObject, hittingObjectEffectSOs);
        }
    }
}