using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOG : MonoBehaviour
{
    // [SerializeField] protected BulletSO bulletSO;
    // private ObjectOGMovement orbMovement; 

    // private void Start() {
    //     orbMovement = GetComponent<ObjectOGMovement>();
    // }

    // private void Update() {
    //     if(orbMovement.CanMove){
    //         orbMovement.PerformMovement();
    //     }
    // }
    
    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(string.Compare(other.gameObject.name, "RangeTakeBulletOGCollider") == 0){
    //         orbMovement.CanMove = true;
    //     }
    //     else if(other.gameObject.GetComponentInChildren<BulletBox>()){
    //         BulletColleted(other.gameObject.GetComponentInChildren<BulletBox>());
    //         orbMovement.CanMove = false;
    //     }
    // }
    // public void BulletColleted(BulletBox playerBulletBox){
    //     gameObject.SetActive(false);
    //     playerBulletBox.AddObjectToBox(bulletSO);
    // }
}
