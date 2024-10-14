using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitablee 
{
    public bool FirstTimeSpawn { get; set;}
    public GameObject HittingVFXPrefab { get;}
    public GameObject SpawnSFXPrefab { get;}
    public GameObject HurtSFXPrefab { get;}
    public GameObject DeathSFXPrefab { get;}

    public List<HittingEffectsSO> HittingObjectEffectSOs  { get;}
    public List<HittingEffectsSO> HittingOtherEffectSOs  { get;}


    public enum SFXPrefab{
        SPAWN,
        HURT,
        DEATH,
    }
    

    public void PlayHittingSFX(SFXPrefab sfxPrefab){
        if(sfxPrefab == SFXPrefab.SPAWN){
            if(SpawnSFXPrefab == null) return;
            if(FirstTimeSpawn){
                FirstTimeSpawn = false;
                return;
            }
            PoolObject.GetPoolObject(SpawnSFXPrefab).GetObjectInPool(new Vector3(0, 0, 0), Quaternion.identity, () => {
                Dictionary<string, object> data = new()
                {
                    { "isLoop", false}
                };
                return data;
            });
        }
        else if(sfxPrefab == SFXPrefab.HURT){
            if(HurtSFXPrefab == null) return;
            PoolObject.GetPoolObject(HurtSFXPrefab).GetObjectInPool(new Vector3(0, 0, 0), Quaternion.identity, () => {
                Dictionary<string, object> data = new()
                {
                    { "isLoop", false}
                };
                return data;
            });
        }
        else{
            if(DeathSFXPrefab == null) return;
            PoolObject.GetPoolObject(DeathSFXPrefab).GetObjectInPool(new Vector3(0, 0, 0), Quaternion.identity, () => {
                Dictionary<string, object> data = new()
                {
                    { "isLoop", false}
                };
                return data;
            });
        }
    }

    public void PlayHittingVFX(Vector3 position){
        if(HittingVFXPrefab == null) return;
        PoolObject.GetPoolObject(HittingVFXPrefab).GetObjectInPool(position, Quaternion.identity, null);
    }

    public void HittingEffectsPerform(GameObject sender, GameObject _receiver, List<HittingEffectsSO> hittingObjectEffectSOs){
        if(hittingObjectEffectSOs.Count <= 0) return;
        foreach(var hittingEffectsSO in hittingObjectEffectSOs){
            hittingEffectsSO.HittingEffectsPerform(sender, _receiver);
        }
    }
}