using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWaveSpawner : MonoBehaviour
{
    [SerializeField] protected int limitNumberSpawnInScene = 12;
    [SerializeField] protected int minObjectCountNormalWave;
    [SerializeField] protected int maxObjectCountNormalWave;
    [SerializeField] protected float spawnRate = 1f;
    [SerializeField] protected float timeBetweenBigWave = 60;
    [SerializeField] protected float timeBetweenNormalWave = 0;

    [SerializeField] protected private GameObject spawnerVFXPrefab;
    [SerializeField] protected List<PoolObject> objectPools = new();
    [SerializeField] protected List<int> numSpawnRateInWave = new();
    [SerializeField] protected List<int> objectCountInBigWave = new();

    [SerializeField] protected float radiusSpawn = 3;

    protected int currentWave = 0;   
    protected int numWaves; 
    protected bool waveIsDone = false;
    protected bool isBigWave = false;
    protected bool bigWavePerform = false;

    [SerializeField] protected PolygonCollider2D spawnerRange;
    protected Action<KeyValuePair<string, object>> StartSpawmDelegate;

    public int CurrentWave { get => currentWave; set => currentWave = value; }
    public int NumWaves { get => numWaves; set => numWaves = value; }

    private void Start() {
        numWaves = objectCountInBigWave.Count;

        StartSpawmDelegate = (param) => {
            waveIsDone = true; 
            isBigWave = true;
        };

        Obsever.AddListener(EventID.SCENE_OnStart, StartSpawmDelegate);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.SCENE_OnStart, StartSpawmDelegate);
    }

    private void Update() {
        WaveSpawnerPerform();
    }

    public void CheckBigWave(){
        if(isBigWave && bigWavePerform){
            isBigWave = false;
            StartCoroutine(ResetBigWave());
        }
    }

    public void WaveSpawnerPerform(){
        if(currentWave + 1 > numWaves) return;
        if(waveIsDone && isBigWave){
            StartCoroutine(BigWaveSpwaner());
        }
        else if(waveIsDone && !isBigWave){
            StartCoroutine(NormalWaveSpawner());
        }
        CheckBigWave();
    }

    protected virtual IEnumerator LimitNumberObjectPerfom(){
        yield return null;
    }

    IEnumerator NormalWaveSpawner(){
        waveIsDone = false;

        // Kiểm tra giới hạn số lượng object trong scene
        yield return StartCoroutine(LimitNumberObjectPerfom());

        int randomObjectSpawn = UnityEngine.Random.Range(minObjectCountNormalWave * numSpawnRateInWave[currentWave], maxObjectCountNormalWave * numSpawnRateInWave[currentWave]);
        Vector2[] pos = new Vector2[randomObjectSpawn];

        for(int i = 0; i < randomObjectSpawn; i++){
            pos[i] = GetRandomPos();
            PoolObject.GetPoolObject(spawnerVFXPrefab).GetObjectInPool(pos[i], Quaternion.identity, null);
        }

        yield return new WaitForSeconds(1);

        for(int i = 0; i < randomObjectSpawn; i++){
            int randomObject = UnityEngine.Random.Range(0,objectPools.Count);
            CreateOneObjectInWave(pos[i], objectPools[randomObject]);
        }

        yield return new WaitForSeconds(timeBetweenNormalWave);

        waveIsDone = true;
    }

    IEnumerator BigWaveSpwaner(){
        waveIsDone = false;
        bigWavePerform = true;
        
        for(int i = 0; i < objectCountInBigWave[currentWave] * numSpawnRateInWave[currentWave]; i++){
            yield return StartCoroutine(LimitNumberObjectPerfom()); // Kiểm tra giới hạn quái

            Vector3 spawnPosition = GetRandomPos();

            PoolObject.GetPoolObject(spawnerVFXPrefab).GetObjectInPool(spawnPosition,  Quaternion.identity, null);
            yield return new WaitForSeconds(1f);
            
            int randomObject = UnityEngine.Random.Range(0, objectPools.Count);
            CreateOneObjectInWave(spawnPosition, objectPools[randomObject]);

            yield return new WaitForSeconds(spawnRate);
        } 
        
        yield return null;

        waveIsDone = true;
        bigWavePerform = false;
        currentWave++; 
    }

    public virtual void CreateOneObjectInWave(Vector3 spawnPos, PoolObject poolObject){
        Debug.Log("Not override");
    }

    IEnumerator ResetBigWave(){
        float time = 0;
        while(time < timeBetweenBigWave){
            if(!bigWavePerform) time += Time.deltaTime;
            yield return null;
        }
        isBigWave = true;
    }

    public Vector2 GetRandomPos(){
        //randomPosNearPlayer
        float xOffset = UnityEngine.Random.RandomRange(-radiusSpawn, radiusSpawn);
        float yOffset = UnityEngine.Random.RandomRange(-radiusSpawn,radiusSpawn);

        var playerTransform = (Vector2)PlayerController.instance.transform.position;

        var randomPoint = playerTransform + new Vector2(xOffset, yOffset);

        var closedPoint = spawnerRange.ClosestPoint(randomPoint);
        
        if(closedPoint == randomPoint){
            return randomPoint;
        }
        else{
            return closedPoint;
        }
    }

}
