using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyShieldPoolObject : PoolObject
{
     public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<BlueEnemyShieldPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
