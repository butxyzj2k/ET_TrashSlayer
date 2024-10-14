using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSFXPoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<PlayerAttackSFXPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
