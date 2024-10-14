using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    // public static SFXManager instance;

    // private void Awake() {
    //     if(instance == null){
    //         instance = this;
    //     }
    //     if(FindObjectsOfType<SFXManager>().Length > 1){
    //         Destroy(this);
    //     }
    // }
    //  public void PlaySFX(GameObject sfxPrefab, bool isLoop, Func<bool> func) {
    //     if (sfxPrefab == null) return;
        
    //     PoolObject sfxPoolObject = PoolObject.GetPoolObject(sfxPrefab);
    //     if (sfxPoolObject == null) return;

    //     GameObject sfxObject = sfxPoolObject.GetObjectInPool();
    //     if (sfxObject == null) return;

    //     AudioSource audioSource = sfxObject.GetComponent<AudioSource>();
    //     if (audioSource == null) return;

    //     sfxObject.SetActive(true);
    //     audioSource.loop = isLoop;
    //     audioSource.Play();

    //     if (!isLoop) {
    //         InvokeExtensionCode.Invoke(SceneGameManager.sceneGameManager, () => StopSFX(sfxObject), audioSource.clip.length);
    //     } else {
    //         SceneGameManager.sceneGameManager.StartCoroutine(ResetSFXLoop(sfxObject, func));
    //     }
    // }

    // IEnumerator ResetSFXLoop(GameObject sfxObject, Func<bool> func) {
    //     while (true) {
    //         if (func != null && func()) {
    //                 StopSFX(sfxObject);
    //                 yield break;
    //         }
    //         yield return null;
    //     }
    // }

    // public void StopSFX(GameObject sfxObject) {
    //     AudioSource audioSource = sfxObject.GetComponent<AudioSource>();
    //     if (audioSource == null) return;

    //     audioSource.loop = false;
    //     audioSource.Stop();
    //     sfxObject.SetActive(false);
    // }
}
