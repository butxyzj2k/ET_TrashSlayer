using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalAreaEffectHitting : MonoBehaviour, ICanTakeDamage, IWeaponHealth, IHitablee
{
    //TakeDamage
    [SerializeField] protected float damage = 1;
    
    public float Damage { get => damage; set => damage = value; }

    //WeaponHealth
    private float currentTime = 0;
    [SerializeField] private float timeExist = 5;
    private GameObject currentOwner = null;
    [SerializeField] string currentTarget;
    float IWeaponHealth.CurrentTime { get => currentTime; set => currentTime = value; }
    public float TimeExist { get => timeExist; set => timeExist = value; }
    public GameObject CurrentOwner { get => currentOwner; set => currentOwner = value; }
    public string CurrentTarget { get => currentTarget; set => currentTarget = value; } 

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
    public List<HittingEffectsSO> HittingObjectEffectSOs { get => hittingObjectEffectSOs;}
    public List<HittingEffectsSO> HittingOtherEffectSOs { get => hittingOtherEffectSOs;}
    public GameObject DeathSFXPrefab { get => deathSFXPrefab;}

    //Partical Hitting
    ParticleSystem particleDamageSystem;
    bool canTakeDamage = true;

    private void Awake() {
        particleDamageSystem = GetComponent<ParticleSystem>();
        particleDamageSystem.trigger.AddCollider(PlayerController.instance.transform);
    }

    private void Start() {
        currentOwner = transform.parent.gameObject;
    }

    private void OnParticleTrigger() {
        if(particleDamageSystem == null) return;
        TakeHit(damage, particleDamageSystem.trigger.GetCollider(0).gameObject);
    }

    public void TakeHit(float damage, GameObject receiver)
    {
        if(!receiver.CompareTag(currentOwner.tag)){
            StartCoroutine(ParticalDamgeTakeHit(damage, receiver));
        }
    }

    IEnumerator ParticalDamgeTakeHit(float damage, GameObject receiver){
        if(canTakeDamage){
            canTakeDamage = false;

            DamagePopUpVisualEffect.CreateDamagePopUp(receiver.transform.position + new Vector3(-1 * 0.7f * receiver.transform.localScale.x, 0.4f), Quaternion.identity, () => {
            Dictionary<string, object> data = new(){
                    { "isCritical", false },
                    { "value", damage }
                };
                return data;
            });
                    
            receiver.GetComponent<IHaveHealth>().GetHit(damage); 
            ((IHitablee)this).HittingEffectsPerform(gameObject, receiver, hittingObjectEffectSOs);
            StartCoroutine(ResetCanTakeDamage());
            yield return null;
        }
    }

    IEnumerator ResetCanTakeDamage(){
        yield return new WaitForSeconds(1);
        canTakeDamage = true;
    }
}
