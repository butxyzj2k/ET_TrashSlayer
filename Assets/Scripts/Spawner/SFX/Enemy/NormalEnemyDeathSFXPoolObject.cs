using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyDeathSFXPoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<NormalEnemyDeathSFXPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
