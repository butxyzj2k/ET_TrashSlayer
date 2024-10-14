using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemyPoolObject : EnemyPoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<GreenEnemyPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
