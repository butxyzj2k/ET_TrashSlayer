using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet2PoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<RedBullet2PoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}