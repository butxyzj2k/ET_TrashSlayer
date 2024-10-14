using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOGHitting : MonoBehaviour, IHitablee
{    
    //Hitting
    [SerializeField] GameObject hittingVFXPrefabs;
    [SerializeField] private GameObject spawnSFXPrefab;
    [SerializeField] private GameObject hurtSFXPrefab;
    [SerializeField] private GameObject deathSFXPrefab;
    [SerializeField] protected List<HittingEffectsSO> hittingObjectEffectSOs = new();
    [SerializeField] private List<HittingEffectsSO> hittingOtherEffectSOs = new();
    protected bool firstTimeSpawn = true;
    public bool FirstTimeSpawn { get => firstTimeSpawn; set => firstTimeSpawn = value; }
    public GameObject HittingVFXPrefab { get=> hittingVFXPrefabs;}
    public GameObject SpawnSFXPrefab { get => spawnSFXPrefab;}
    public GameObject HurtSFXPrefab { get => hurtSFXPrefab;}
    public GameObject DeathSFXPrefab { get => deathSFXPrefab;}
    public List<HittingEffectsSO> HittingObjectEffectSOs { get => hittingObjectEffectSOs;}
    public List<HittingEffectsSO> HittingOtherEffectSOs { get => hittingOtherEffectSOs;}

    [SerializeField] string RangeTakeObjectName;

    private void OnTriggerEnter2D(Collider2D other) {
        if(string.Compare(other.gameObject.name, RangeTakeObjectName) == 0){
            gameObject.GetComponent<ObjectOGMovement>().CanMove = true;
        }
        else if(other.gameObject.CompareTag("Player")){
            ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.HURT);
            ((IHitablee)this).HittingEffectsPerform(gameObject, other.gameObject, hittingObjectEffectSOs);
            gameObject.GetComponent<ObjectOGMovement>().CanMove = false;
            gameObject.SetActive(false);
        }
    }
}