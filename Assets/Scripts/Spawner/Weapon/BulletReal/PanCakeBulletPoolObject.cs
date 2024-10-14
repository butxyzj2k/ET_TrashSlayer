using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCakeBulletPoolObject : PoolObject
{
     public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<PanCakeBulletPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
