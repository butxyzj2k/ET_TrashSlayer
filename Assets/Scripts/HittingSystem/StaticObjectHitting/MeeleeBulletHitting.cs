using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeeleeBulletHitting : MonoBehaviour, ICanTakeDamage, IHaveHealth, IWeaponHealth, IHitablee
{
    //TakeDamage

    [SerializeField] private float damage = 1;

    public float Damage { get => damage; set => damage = value; }

    //Health
    [SerializeField] private int maxHealth = 1;
    private float currentHealth;
    [SerializeField] private UnityEvent onHit;
    [SerializeField] private UnityEvent onDeath;

    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    UnityEvent IHaveHealth.OnHit { get => onHit; set => onHit = value; }
    UnityEvent IHaveHealth.OnDeath { get => onDeath; set => onDeath = value; }
    
    //WeaponHealth
    private float currentTime = 0;
    [SerializeField] private float timeExist = 5;
    private GameObject currentOwner = null;
    [SerializeField] string currentTarget;
    float IWeaponHealth.CurrentTime { get => currentTime; set => currentTime = value; }
    float IWeaponHealth.TimeExist { get => timeExist; set => timeExist = value; }
    public GameObject CurrentOwner { get => currentOwner; set => currentOwner = value; }
    public string CurrentTarget { get => currentTarget; set => currentTarget = value; } 

    //Hitting
    [SerializeField] GameObject hittingVFXPrefabs;
    [SerializeField] private GameObject spawnSFXPrefab;
    [SerializeField] private GameObject hurtSFXPrefab;
    [SerializeField] private GameObject deathSFXPrefab;
    [SerializeField] private List<HittingEffectsSO> hittingObjectEffectSOs = new();
    [SerializeField] private List<HittingEffectsSO> hittingOtherEffectSOs = new();
    protected bool firstTimeSpawn = true;
    public bool FirstTimeSpawn { get => firstTimeSpawn; set => firstTimeSpawn = value; }
    public GameObject HittingVFXPrefab { get=> hittingVFXPrefabs;}
    public GameObject SpawnSFXPrefab { get => spawnSFXPrefab;}
    public GameObject HurtSFXPrefab { get => hurtSFXPrefab;}
    public GameObject DeathSFXPrefab { get => deathSFXPrefab;}
    public List<HittingEffectsSO> HittingObjectEffectSOs { get => hittingObjectEffectSOs;}
    public List<HittingEffectsSO> HittingOtherEffectSOs { get => hittingOtherEffectSOs;}

    //MeeleeBulletHitting
    [SerializeField] float normalDamageRate = 1;
    [SerializeField] float extraDamageRate = 2;

    private void Start() {
        onHit.AddListener(() => ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.HURT));
        onDeath.AddListener(() => ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.DEATH));
        ((IHaveHealth)this).InitalizeHealth(maxHealth);
    }

    private void OnEnable() {
        currentTime = 0;
        ((IHaveHealth)this).InitalizeHealth(maxHealth);
        ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.SPAWN);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IHaveHealth>() != null || other.gameObject.GetComponent<ObstacleObjectHitting>() != null ) {
            TakeHit(damage, other.gameObject);
        }
        else if (other.transform.parent.GetComponent<IHitablee>() != null) {
            TakeHit(damage, other.transform.parent.gameObject);
        }    
    }

    public void TakeHit(float damage, GameObject receiver)
    {
        if(receiver.GetComponent<DynamicObjectHitting>() && !receiver.CompareTag(currentOwner.tag)){
            float damageRate = string.Compare(currentTarget, receiver.name) != 0 ? normalDamageRate : extraDamageRate;
            damage = damage * damageRate * currentOwner.GetComponent<ObjectAttack>().DamageRate;
    
            DamagePopUpVisualEffect.CreateDamagePopUp(receiver.transform.position + new Vector3(-1 * 0.7f * receiver.transform.localScale.x, 0.4f), Quaternion.identity, () => {
                Dictionary<string, object> data = new()
                    {
                        { "isCritical", damageRate > normalDamageRate },
                        { "value", damage }
                    };
                return data;
            });

            receiver.GetComponent<IHaveHealth>().GetHit(damage);
            ((IHitablee)this).HittingEffectsPerform(gameObject, receiver, hittingObjectEffectSOs);
            ((IHitablee)this).PlayHittingVFX(transform.position);
        }
        else if((receiver.GetComponent<IWeaponHealth>() != null && !receiver.GetComponent<IWeaponHealth>().CurrentOwner.CompareTag(currentOwner.tag)) || receiver.GetComponent<ObstacleObjectHitting>()){
            if(receiver.GetComponent<ICanTakeDamage>() == null) ((IHaveHealth)this).GetHit(1);
            if(receiver.TryGetComponent<IHaveHealth>(out IHaveHealth health)){ 
                health.GetHit(damage);
            }
            ((IHitablee)this).HittingEffectsPerform(gameObject, receiver, hittingOtherEffectSOs);
            ((IHitablee)this).PlayHittingVFX(transform.position); 
        }
    }

    public void DestroyObject()
    {
        gameObject.SetActive(false);
    }
}