using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpBulletOGPoolObject : PoolObject
{
     public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<ShrimpBulletOGPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
