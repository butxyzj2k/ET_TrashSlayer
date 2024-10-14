using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleBulletOGPoolObject : PoolObject
{
     public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<BottleBulletOGPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
