using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetBulletPoolObject : PoolObject
{
     public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<MeetBulletPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
