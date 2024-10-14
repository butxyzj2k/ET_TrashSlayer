using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPersueObjectMovement{
    public bool CanChangeScale { get; }
    public Transform TargetTransform { get; set; }

    public Vector3 GetDirToTargetTransform();

    public void PersueObjectChangeScale(GameObject gameObject){
        if(!CanChangeScale) return;

        Vector3 objectScale = gameObject.transform.localScale;

        if(gameObject.GetComponent<Rigidbody2D>().velocity.x > 0){
            objectScale.x = 1;
        }
        else if(gameObject.GetComponent<Rigidbody2D>().velocity.x < 0){
            objectScale.x = -1;
        }

        gameObject.transform.localScale = objectScale;
    }
}