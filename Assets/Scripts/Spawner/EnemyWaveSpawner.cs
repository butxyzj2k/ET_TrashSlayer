using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyWaveSpawner : BaseWaveSpawner
{
    private bool haveSpawnedBoss = false;

    private void Update() {
        WaveSpawnerPerform();
        SpawnEnemyBoss();
    }

    protected override IEnumerator LimitNumberObjectPerfom(){
        while (true) {
        int currentNumberObjectSpawnInScene = GameObject.FindGameObjectsWithTag("Enemy").Length;
        
        // Chờ cho đến khi số lượng quái vật trong scene nhỏ hơn limit
        if (currentNumberObjectSpawnInScene < limitNumberSpawnInScene) {
            yield break; // Thoát vòng lặp nếu đủ điều kiện spawn tiếp
        }
        
        yield return new WaitForSeconds(0.5f); // Giảm tần suất kiểm tra để tránh lặp liên tục
    }
    }
    public override void CreateOneObjectInWave(Vector3 spawnPos, PoolObject poolObject)
    {
        poolObject.GetObjectInPool(spawnPos, quaternion.identity, null);
    }

    public void SpawnEnemyBoss(){
        if(!haveSpawnedBoss && currentWave + 1 > numWaves){
            EnemyController[] enemies = FindObjectsOfType<EnemyController>();
            if(enemies.Length > 0){
                return;
            }
            
            haveSpawnedBoss = true;
            EnemyBossController.instance.gameObject.transform.SetPositionAndRotation(GetRandomPos(), Quaternion.identity);
            EnemyBossController.instance.gameObject.SetActive(true);

            Obsever.PostEvent(EventID.BOSS_SPAWN, new KeyValuePair<string, object>("maxHealth", EnemyBossController.instance.enemyHitting.MaxHealth));
            Obsever.PostEvent(EventID.BOSS_SPAWN, new KeyValuePair<string, object>("GameObject", EnemyBossController.instance.gameObject));
            Obsever.PostEvent(EventID.BOSS_SPAWN, new KeyValuePair<string, object>(null, null));
        }
    }
}