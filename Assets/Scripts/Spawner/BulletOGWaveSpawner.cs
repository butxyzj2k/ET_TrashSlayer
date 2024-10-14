using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BulletOGWaveSpawner : BaseWaveSpawner
{
    protected override IEnumerator LimitNumberObjectPerfom(){
        while (true) {
        int currentNumberObjectSpawnInScene = GameObject.FindGameObjectsWithTag("BulletOG").Length;
        
        if (currentNumberObjectSpawnInScene < limitNumberSpawnInScene) {
            yield break; // Thoát vòng lặp nếu đủ điều kiện spawn tiếp
        }
        
        yield return new WaitForSeconds(0.5f); 
    }
    }

    public override void CreateOneObjectInWave(Vector3 spawnPos, PoolObject poolObject)
    {
        int randomZRoatation = UnityEngine.Random.Range(0, 360);
        Quaternion spawnQuaternion = Quaternion.Euler(0, 0, randomZRoatation);
        poolObject.GetObjectInPool(spawnPos, spawnQuaternion, null);
    }
}