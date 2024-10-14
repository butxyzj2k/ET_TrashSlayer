using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesAreaPoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<GlassesAreaPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
