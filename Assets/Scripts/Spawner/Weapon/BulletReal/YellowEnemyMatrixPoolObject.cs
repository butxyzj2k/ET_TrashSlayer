using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEnemyMatrixPoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<YellowEnemyMatrixPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
