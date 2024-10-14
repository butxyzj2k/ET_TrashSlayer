using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRobotDeathSFXPoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<BossRobotDeathSFXPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}