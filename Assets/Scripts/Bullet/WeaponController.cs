using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour, IObjectInPool
{
    protected StaticObjectMovement weaponMovement; 
    protected IWeaponHealth weaponHealth;  
    protected GameObject currentOwner = null;
    public GameObject CurrentOwner { get => currentOwner; set => currentOwner = value; }

    private void Awake() {
        weaponMovement = GetComponent<StaticObjectMovement>();
        weaponHealth = GetComponent<IWeaponHealth>();
    }

    private void Update() {
        weaponMovement.ObjectMovementAnim();
        weaponHealth.DestroyObjectAfterTimeExit(gameObject);
    }

    private void FixedUpdate() {
        weaponMovement.PerformMovement();
    }
    
    public virtual void ReleaseObject(Vector3 position, Quaternion rotation, Func<Dictionary<string, object>> data)
    {
        gameObject.transform.SetPositionAndRotation(position, rotation);

        //Nếu startPoint là quái hoặc người => gán objectOwner của projectile là startPoint
        Transform startPoint = (Transform)data()["startPoint"];
        if(startPoint.GetComponent<ObjectAttack>()){
            currentOwner = startPoint.gameObject;
        }
        else if(startPoint.GetComponentInParent<ObjectAttack>()){
            currentOwner = startPoint.parent.gameObject;
        }

        //Nếu starPoint là đạn (trong flexibleAttack) => gán objectOwner của projectile là objectOwner startPoint
        else{
            currentOwner = startPoint.GetComponent<IWeaponHealth>().CurrentOwner;
        }
        weaponHealth.CurrentOwner = currentOwner;
        gameObject.SetActive(true);
    }
}
