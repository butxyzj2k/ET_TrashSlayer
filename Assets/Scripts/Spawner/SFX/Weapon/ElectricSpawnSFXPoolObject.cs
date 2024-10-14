using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSpawnSFXPoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<ElectricSpawnSFXPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
