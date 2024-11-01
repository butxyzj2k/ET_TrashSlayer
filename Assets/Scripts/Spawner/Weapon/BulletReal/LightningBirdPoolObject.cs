using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBirdPoolObject : PoolObject
{

    private void Start() {
        SetOnePool();
        InitalizePoolObject();
    }

    public override void InitalizePoolObject()
    {
        SceneGameManager.instance.StartCoroutine(InitalizeEnemyPoolObjectCourotine());
    }

    IEnumerator InitalizeEnemyPoolObjectCourotine(){
        while(FindObjectOfType<PlayerController>() == null){
            yield return null;
        }
        base.InitalizePoolObject();
    }
}
