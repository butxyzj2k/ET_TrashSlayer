using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbHitting : MonoBehaviour
{
    // private PoolObject playerTakingOrbSFXPoolObject;
    // [SerializeField] private HittingEffectsSO hittingEffectsSO;

    // private void Awake() {
    //     playerTakingOrbSFXPoolObject = FindObjectOfType<PlayerTakingOrbSFXPoolObject>();
    // }

    // public void HittingEffectsPerform(GameObject sender, GameObject _receiver, HittingEffectsSO hittingEffectsSO)
    // {
        
    // }

    // public void TakeHit(float damage, GameObject receiver)
    // {
    //     hittingEffectsSO.HittingEffects(gameObject, receiver);
    // }

    // public void CreatePlayerTakingOrbSFXObject(){
    //     if(playerTakingOrbSFXPoolObject == null) return;
    //     GameObject takingOrbSFX = playerTakingOrbSFXPoolObject.GetObjectInPool();
    //     if(takingOrbSFX == null) return;
    //     takingOrbSFX.GetComponent<AudioSource>().gameObject.SetActive(true);
    //     takingOrbSFX.GetComponent<AudioSource>().Play();
    //     InvokeExtensionCode.Invoke(SceneGameManager.sceneGameManager, () => DestroyPlayerTakingOrbSFXObject(takingOrbSFX), takingOrbSFX.GetComponent<AudioSource>().clip.length);
    // }

    // public void DestroyPlayerTakingOrbSFXObject(GameObject takingOrbSFX){
    //     takingOrbSFX.GetComponent<AudioSource>().Stop();
    //     takingOrbSFX.GetComponent<AudioSource>().gameObject.SetActive(false);
    // }
}
