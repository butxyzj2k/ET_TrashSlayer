using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBoxOGPoolObject : PoolObject
{
     public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<PaperBoxOGPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
