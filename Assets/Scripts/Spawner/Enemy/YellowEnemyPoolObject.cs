using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEnemyPoolObject : EnemyPoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<YellowEnemyPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
