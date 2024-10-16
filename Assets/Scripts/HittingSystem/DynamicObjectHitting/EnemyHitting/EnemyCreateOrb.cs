using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyCreateOrb : MonoBehaviour
{
    [SerializeField] private int numOrb;
    [SerializeField] private List<GameObject> orbPrefabs = new();

    Action<KeyValuePair<string, object>> BossDeath1;
    Action<KeyValuePair<string, object>> EnemyDeath1;

    private void Start() {
        BossDeath1 = (pram) => {
            if(pram.Key == null) return;
            if(!pram.Key.Equals("GameObject")) return;
            if((GameObject)pram.Value == gameObject) CreteOrb();
        };

        EnemyDeath1 = (pram) => {
            if(pram.Key == null) return;
            if(!pram.Key.Equals("GameObject")) return;
            if((GameObject)pram.Value == gameObject) CreteOrb();
        };

        Obsever.AddListener(EventID.Enemy_DEATH, EnemyDeath1);

        Obsever.AddListener(EventID.BOSS_DEATH, BossDeath1);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.Enemy_DEATH, EnemyDeath1);

        Obsever.RemoveListener(EventID.BOSS_DEATH, BossDeath1);
    }

    public void CreteOrb(){
        for(int i = 0; i < numOrb; i++){
            int randomOrb = UnityEngine.Random.Range(0, orbPrefabs.Count);
            float randomPosX = UnityEngine.Random.RandomRange(-1f, 1f);
            float randomPosY = UnityEngine.Random.RandomRange(-1f, 1f);
            PoolObject.GetPoolObject(orbPrefabs[randomOrb]).GetObjectInPool(transform.position + new Vector3(randomPosX, randomPosY, 0), quaternion.identity, null);
        }
    }
}
