using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDamageAccumulationVFXPoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<StateDamageAccumulationVFXPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
