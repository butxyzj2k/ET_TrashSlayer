using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShieldHitting : MonoBehaviour, IHaveHealth, IWeaponHealth, IHitablee
{
    //Health
    [SerializeField] private int maxHealth = 1;
    private float currentHealth;
    [SerializeField] private UnityEvent onHit;
    [SerializeField] private UnityEvent onDeath;

    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    UnityEvent IHaveHealth.OnHit { get => onHit; set => onHit = value; }
    UnityEvent IHaveHealth.OnDeath { get => onDeath; set => onDeath = value; }
    

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


    //WeaponHealth
    private float currentTime = 0;
    [SerializeField] private float timeExist = 5;
    private GameObject currentOwner = null;
    [SerializeField] string currentTarget;
    float IWeaponHealth.CurrentTime { get => currentTime; set => currentTime = value; }
    float IWeaponHealth.TimeExist { get => timeExist; set => timeExist = value; }
    public GameObject CurrentOwner { get => currentOwner; set => currentOwner = value; }
    public string CurrentTarget { get => currentTarget; set => currentTarget = value; } 

    //Shield
    
    private void Start() {
        onHit.AddListener(() => ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.HURT));
        onDeath.AddListener(() => ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.DEATH));
    }

    private void OnEnable() {
        currentTime = 0;
        ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.SPAWN);
        ((IHaveHealth)this).InitalizeHealth(maxHealth);
    }

    // private void Update() {
    //     ((IWeaponHealth)this).DestroyObjectAfterTimeExit(gameObject);
    //     DestroyShieldWhenOwnerDead();
    // }

    public void DestroyObject()
    {
        gameObject.SetActive(false);
    }
}