using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DynamicObjectHitting : MonoBehaviour, ICanTakeDamage, IHaveHealth, IHitablee
{
    //TakeDamage

    [SerializeField] protected float damage = 1;
    public float Damage { get => damage; set => damage = value; }

    //Health
    [SerializeField] protected int maxHealth = 1;
    protected float currentHealth;
    [SerializeField] protected UnityEvent onHit;
    [SerializeField] protected UnityEvent onDeath;

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

    //DynamicObjectHitting
    protected bool isHurt = false;
    protected bool isDeath = false;

    public bool IsHurt { get => isHurt; set => isHurt = value; }
    public bool IsDeath { get => isDeath; set => isDeath = value; }

    protected Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        TakeHit(damage, other.gameObject);
    }

    public virtual void HealthManager(){
        Debug.Log("Not override HealthManager");
    }

    public void TakeHit(float damage, GameObject receiver)
    {
        DynamicObjectTakeHit(damage, receiver);
    }

    public virtual void DynamicObjectTakeHit(float damage, GameObject receiver){
        if(receiver.GetComponent<MeeleeBulletHitting>() && !gameObject.CompareTag(receiver.GetComponent<MeeleeBulletHitting>().CurrentOwner.tag)){
            ((IHitablee)this).HittingEffectsPerform(gameObject, receiver, hittingObjectEffectSOs);
            receiver.GetComponent<IHaveHealth>().GetHit(damage);
        }
    }

    public void DynamicObjectHurtAnim(){
        if(!isHurt){
            isHurt = true;
            anim.SetBool("isHurt", true);
            StartCoroutine(ResetHurt()); 
        }
    }

    IEnumerator ResetHurt(){
        yield return new WaitForSeconds(GetComponent<ObjectGetClipIn4>().hurtTime);
        isHurt = false;
        anim.SetBool("isHurt", false);
    }

    public virtual void DynamicObjectDeathDelay(){
        if(gameObject.GetComponent<Collider2D>()) gameObject.GetComponent<Collider2D>().enabled = false;
        else if(gameObject.GetComponentInChildren<Collider2D>()) gameObject.GetComponentInChildren<Collider2D>().enabled = false;
        
        isDeath = true;
        anim.SetBool("isDeath", true);
        Invoke(nameof(DestroyObject), GetComponent<ObjectGetClipIn4>().deathTime);
    }


    public void DestroyObject()
    {
        gameObject.SetActive(false);
    }
}