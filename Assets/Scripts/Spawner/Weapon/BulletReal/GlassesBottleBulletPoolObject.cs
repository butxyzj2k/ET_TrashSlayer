using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesBottleBulletPoolObject : PoolObject
{
     public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<GlassesBottleBulletPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
