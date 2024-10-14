using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyPoolObject : EnemyPoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<BlueEnemyPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
