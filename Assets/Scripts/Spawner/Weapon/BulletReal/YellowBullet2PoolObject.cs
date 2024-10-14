using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBullet2PoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<YellowBullet2PoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}