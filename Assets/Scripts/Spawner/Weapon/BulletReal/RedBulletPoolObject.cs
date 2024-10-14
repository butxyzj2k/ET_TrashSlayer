using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBulletPoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<RedBulletPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
