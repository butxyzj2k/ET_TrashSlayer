using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticlePoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<BloodParticlePoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
