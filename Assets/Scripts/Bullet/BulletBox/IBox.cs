using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBox 
{
    public void AddObjectToBox(object objectAdded);

    public void MinusObjectInBox();

    public void ChangeObjectInBox();

    public void ChangeSripteObjectInBox();

    public GameObject GetObjectInBox();
}