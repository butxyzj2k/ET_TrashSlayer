using System;
using System.Collections;
using System.Collections.Generic;
// using UnityEditor.SearchService;
using UnityEngine;

public class SoundEffect : BaseEffect
{
    public override void ReleaseObject(Vector3 position, Quaternion rotation, Func<Dictionary<string, object>> data){
        gameObject.SetActive(true);

        AudioSource audioSource = gameObject.GetComponent<AudioSource>();

        audioSource.loop = (bool)data()["isLoop"];
        audioSource.Play();

        if (!(bool)data()["isLoop"]) {
            InvokeExtensionCode.Invoke(SceneGameManager.instance, () => StopSFX(gameObject), audioSource.clip.length);
        } else {
            SceneGameManager.instance.StartCoroutine(ResetSFXLoop(gameObject, data));
        }
    }

    void StopSFX(GameObject sfxObject){
        if (!sfxObject.TryGetComponent<AudioSource>(out var audioSource)) return;

        audioSource.loop = false;
        audioSource.Stop();
        sfxObject.SetActive(false);
    }

    IEnumerator ResetSFXLoop(GameObject sfxObject, Func<Dictionary<string, object>> data) {
        while (true) {
            if (data != null && (bool)data()["isStopSFXLoop"]) {
                    StopSFX(sfxObject);
                    yield break;
            }
            yield return null;
        }
    }
}