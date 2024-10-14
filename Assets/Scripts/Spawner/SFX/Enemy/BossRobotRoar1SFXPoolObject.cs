using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRobotRoar1SFXPoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<BossRobotRoar1SFXPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}