using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHittingVFX : MonoBehaviour
{
    // private PoolObject hitVFXPoolObject;
    [SerializeField] GameObject hittingVFXPrefabs;

    // private void Start() {
    //     SettingHittingVFX();
    // }

    // public void SettingHittingVFX(){
    //     if(hittingVFXPrefabs == null){
    //         Debug.LogWarning("No hittingVFXPrefabs");
    //         return;
    //     }
    //     PoolObject[] poolObjects = FindObjectsOfType<PoolObject>();
    //     foreach(PoolObject poolObject in poolObjects)
    //     {
    //         if(string.Compare(hittingVFXPrefabs.name, poolObject.PoolObjectName) == 0){
    //             hitVFXPoolObject = poolObject;
    //         }
    //     }
    // }

    public void SpawnHitVFX(Vector3 position){
        if(hittingVFXPrefabs == null){
            Debug.LogWarning("Null");
            return;
        }
        GameObject _hittingVFXObject = PoolObject.GetPoolObject(hittingVFXPrefabs).GetObjectInPool(position, Quaternion.identity, null);
        // _hittingVFXObject.transform.position = position;
        // _hittingVFXObject.SetActive(true);
        // InvokeExtensionCode.Invoke(SceneGameManager.sceneGameManager,() => DestroyHitVFX(_hittingVFXObject), 0.3f);
    }
}
