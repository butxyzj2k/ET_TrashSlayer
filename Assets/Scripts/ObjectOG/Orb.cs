using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    // private ObjectOGMovement orbMovement;    
    // private OrbHitting orbHitting;

    // private void Start() {
    //     orbMovement = GetComponent<ObjectOGMovement>();
    //     orbHitting = GetComponent<OrbHitting>();
    // }

    // private void Update() {
    //     if(orbMovement.CanMove){
    //         orbMovement.PerformMovement();
    //     }
    // }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(string.Compare(other.gameObject.name, "RangeTakeOrbCollider") == 0){
    //         orbMovement.CanMove = true;
    //     }
    //     else if(other.gameObject.CompareTag("Player")){
    //         orbHitting.CreatePlayerTakingOrbSFXObject();
    //         orbMovement.CanMove = false;
    //         orbHitting.TakeHit(0, other.gameObject);
    //         gameObject.SetActive(false);
    //     }
    // }
}
