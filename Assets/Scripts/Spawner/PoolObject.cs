using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{

    protected List<GameObject> poolObjects = new List<GameObject>();
    [SerializeField] protected int amountToPool;

    [SerializeField] protected GameObject poolPrefab;
    [SerializeField] protected string poolObjectName;

    public string PoolObjectName { get => poolObjectName; private set => poolObjectName = value; }


    private void Start() {
        InitalizePoolObject();
    }

    public abstract void SetOnePool();

    public virtual void InitalizePoolObject(){
        for(int i = 0; i < amountToPool; i++){
            GameObject poolObject = Instantiate(poolPrefab, transform.position, Quaternion.identity);
            poolObject.transform.SetParent(gameObject.transform);
            poolObjects.Add(poolObject);
            poolObject.SetActive(false);
        }
    }

    public GameObject GetObjectInPool(Vector3 position, Quaternion rotation, Func<Dictionary<string, object>> data){
        foreach(GameObject _objectInPoolObject in poolObjects){
            if(!_objectInPoolObject.activeInHierarchy){
                _objectInPoolObject.GetComponent<IObjectInPool>().ReleaseObject(position, rotation, data);
                return _objectInPoolObject;
            }
        }
        return null;
    }

    public static PoolObject GetPoolObject(GameObject gameObjectPrefab){
        if(gameObjectPrefab == null) return null;
        PoolObject[] poolObjects = FindObjectsOfType<PoolObject>();
        PoolObject gameObjectPoolObject = null;
        foreach(PoolObject poolObject in poolObjects){
            if(string.Compare(poolObject.PoolObjectName, gameObjectPrefab.name) == 0){
                gameObjectPoolObject = poolObject;
            }
        }
        return gameObjectPoolObject;
    }
}