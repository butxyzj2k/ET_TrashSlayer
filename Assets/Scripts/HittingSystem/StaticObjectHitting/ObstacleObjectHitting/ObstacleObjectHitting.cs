using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectHitting : MonoBehaviour, IHitablee
{
    //Hitting
    [SerializeField] GameObject hittingVFXPrefabs;
    [SerializeField] protected GameObject spawnSFXPrefab;
    [SerializeField] protected GameObject hurtSFXPrefab;
    [SerializeField] protected GameObject deathSFXPrefab;
    [SerializeField] protected List<HittingEffectsSO> hittingObjectEffectSOs = new();
    [SerializeField] protected List<HittingEffectsSO> hittingOtherEffectSOs = new();
    protected bool firstTimeSpawn = true;
    public bool FirstTimeSpawn { get => firstTimeSpawn; set => firstTimeSpawn = value; }
    public GameObject HittingVFXPrefab { get=> hittingVFXPrefabs;}
    public GameObject SpawnSFXPrefab { get => spawnSFXPrefab;}
    public GameObject HurtSFXPrefab { get => hurtSFXPrefab;}
    public GameObject DeathSFXPrefab { get => deathSFXPrefab;}
    public List<HittingEffectsSO> HittingObjectEffectSOs { get => hittingObjectEffectSOs;}
    public List<HittingEffectsSO> HittingOtherEffectSOs { get => hittingOtherEffectSOs;}

    private void OnEnable() {
        ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.SPAWN);
    }

    private void OnDisable() {
        ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.DEATH);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<MeeleeBulletHitting>()){
            ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.HURT);
            ((IHitablee)this).HittingEffectsPerform(gameObject, other.gameObject, hittingObjectEffectSOs);
        }
    }
}