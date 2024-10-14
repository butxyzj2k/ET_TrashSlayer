using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCakeAreaPoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<PanCakeAreaPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
