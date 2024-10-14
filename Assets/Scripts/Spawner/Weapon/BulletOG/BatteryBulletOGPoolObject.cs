using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBulletOGPoolObject : PoolObject
{
     public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<BatteryBulletOGPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
