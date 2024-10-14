using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryHitVFXPoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<BatteryHitVFXPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
