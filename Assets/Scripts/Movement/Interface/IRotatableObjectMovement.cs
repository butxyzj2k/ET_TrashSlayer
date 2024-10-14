using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotatableObjectMovement{
    public float RotateSpeed { get; }
    public bool CanRotate { get; }
    public Transform ObjectSprite { get; set; }

    public void RotateObjectInMovement(){
        if(CanRotate) ObjectSprite.Rotate(0, 0, RotateSpeed);
    }
}