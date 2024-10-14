using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOGController : MonoBehaviour, IObjectInPool
{ 
    private ObjectOGMovement objectOGMovement;
    private ObjectOGHitting objectOGHitting;

    private void Awake() {
        objectOGMovement = GetComponent<ObjectOGMovement>();
        objectOGHitting = GetComponent<ObjectOGHitting>();
    }

    private void Update() {
        objectOGMovement.ObjectMovementAnim();
    }

    private void FixedUpdate() {
        objectOGMovement.PerformMovement();
    }

    public void ReleaseObject(Vector3 position, Quaternion rotation, Func<Dictionary<string, object>> data){
        gameObject.transform.SetPositionAndRotation(position, rotation);
        gameObject.SetActive(true);
    }
}
