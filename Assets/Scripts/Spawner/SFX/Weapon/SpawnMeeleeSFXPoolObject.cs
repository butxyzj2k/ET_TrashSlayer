using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeeleeSFXPoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<SpawnMeeleeSFXPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
