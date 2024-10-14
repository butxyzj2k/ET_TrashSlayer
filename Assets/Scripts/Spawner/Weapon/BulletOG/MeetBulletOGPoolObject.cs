using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetBulletOGPoolObject : PoolObject
{
     public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<MeetBulletOGPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
