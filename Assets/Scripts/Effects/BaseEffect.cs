using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffect : MonoBehaviour, IObjectInPool
{
    public virtual void ReleaseObject(Vector3 position, Quaternion rotation, Func<Dictionary<string, object>> data){
        Debug.Log("Not Override");
    }
}
