using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBirdPoolObject : PoolObject
{
    public override void SetOnePool()
    {
        int numPool = FindObjectsOfType<LightningBirdPoolObject>().Length;
        if(numPool > 1){
            Destroy(gameObject);
        }
    }

    private void Awake() {
        SetOnePool();
    }

    private void Start() {
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
