using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtSFXPoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<PlayerHurtSFXPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
