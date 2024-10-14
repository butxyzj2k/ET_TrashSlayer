using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScene1InvisibleMeeleeWeaponPoolObject :  PoolObject
{
     public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<BossScene1InvisibleMeeleeWeaponPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }
}
