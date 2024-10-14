using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBulletSpawnSFXPoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<YellowBulletSpawnSFXPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}