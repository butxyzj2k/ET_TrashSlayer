using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected float curentHealth;
    protected bool isHurt = false;
    protected bool isDeath = false;
    // protected bool canCheckHurt = true;
    // [SerializeField] protected HittingEffectsSO hittingEffectsSO;

    [SerializeField] protected UnityEvent OnHit;
    [SerializeField] protected UnityEvent OnDeath;

    [SerializeField] protected Bar healthBar;

    [SerializeField] protected GameObject spawnSFXPrefab;
    [SerializeField] protected GameObject crashSFXPrefab;
    [SerializeField] protected GameObject deathSFXPrefab;
    protected PoolObject spawnSFXPoolObject;
    protected PoolObject crashSFXPoolObject;    
    protected PoolObject deathSFXPoolObject;

    public float CurentHealth { get => curentHealth; set => curentHealth = value; }
    public bool IsHurt { get => isHurt; set => isHurt = value; }
    public bool IsDeath { get => isDeath; set => isDeath = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public Bar HealthBar { get => healthBar; set => healthBar = value; }
    

    private void Start() {
        SetSFXPoolObject();
        InitalizeHealth(maxHealth);
        PlaySpawnSFX();
        // if(healthBar != null){
        //     healthBar.gameObject.SetActive(true);
        //     healthBar.SetMaxBar(maxHealth);
        //     healthBar.SetCurrentBar(maxHealth, maxHealth.ToString());
        // }
    }

    public void InitalizeHealth(int initialHealth){
        if(initialHealth <= 0){
            curentHealth = 1;
        }
        curentHealth  = initialHealth;
    }

    public void HealthManager(){
        // if(curentHealth <= 0){
        //     OnDeath?.Invoke();
        //     // if(healthBar != null){
        //     //     healthBar.SetCurrentBar(curentHealth, 0.ToString());
        //     // }
        //     return;
        // }
        // if(healthBar != null){
        //     healthBar.SetCurrentBar(curentHealth, curentHealth.ToString());
        // }
        OnHit?.Invoke();
    }

    
    public virtual void DestroyObject(){
        gameObject.SetActive(false);
    }

    public void SetSFXPoolObject(){
        SetSpawnSFXPoolObject();
        SetCrashSFXPoolObject();
        SetDeathSFXPoolObject();
    }

    public virtual void SetSpawnSFXPoolObject(){
        if(spawnSFXPrefab == null) return;
        PoolObject[] poolObjects = FindObjectsOfType<PoolObject>();
        foreach(PoolObject poolObject in poolObjects){
            if(string.Compare(poolObject.PoolObjectName, spawnSFXPrefab.name) == 0){
                spawnSFXPoolObject = poolObject;
            }
        }
    }

    public virtual void PlaySpawnSFX(){
        if(spawnSFXPoolObject == null ){
            SetSpawnSFXPoolObject();
            return;
        } 
        
    }

    public virtual void StopSpawnSFX(GameObject spawnSFXObject){
        spawnSFXObject.GetComponent<AudioSource>().Stop();
        spawnSFXObject.SetActive(false);
    }

    public virtual void SetCrashSFXPoolObject(){
        if(crashSFXPrefab == null) return;
        PoolObject[] poolObjects = FindObjectsOfType<PoolObject>();
        foreach(PoolObject poolObject in poolObjects){
            if(string.Compare(poolObject.PoolObjectName, crashSFXPrefab.name) == 0){
                crashSFXPoolObject = poolObject;
            }
        }
    }

    public virtual void PlayCrashSFX(){
        if(crashSFXPoolObject == null ){
            SetCrashSFXPoolObject();
            return;
        } 
        
    }

    public virtual void StopCrashSFX(GameObject crashSFXObject){
        crashSFXObject.GetComponent<AudioSource>().Stop();
        crashSFXObject.SetActive(false);
    }

    public virtual void SetDeathSFXPoolObject(){
        if(deathSFXPrefab == null) return;
        PoolObject[] poolObjects = FindObjectsOfType<PoolObject>();
        foreach(PoolObject poolObject in poolObjects){
            if(string.Compare(poolObject.PoolObjectName, deathSFXPrefab.name) == 0){
                deathSFXPoolObject = poolObject;
            }
        }
    }

    public virtual void PlayDeathSFX(){
        if(deathSFXPoolObject == null ){
            SetDeathSFXPoolObject();
            return;
        } 
        
    }

    public virtual void StopDeathSFX(GameObject deathSFXObject){
        deathSFXObject.GetComponent<AudioSource>().Stop();
        deathSFXObject.SetActive(false);
    }
}
