using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBulletHitVFXPoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<RedBulletHitVFXPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
