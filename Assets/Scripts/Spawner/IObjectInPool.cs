using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectInPool
{
    public void ReleaseObject(Vector3 position, Quaternion rotation, Func<Dictionary<string, object>> data);
}