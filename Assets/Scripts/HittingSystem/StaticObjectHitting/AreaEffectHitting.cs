using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaEffectHitting : MonoBehaviour, ICanTakeDamage, IWeaponHealth, IHitablee
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

    //AreaEffect
    [SerializeField] private Collider2D areaCollider2D;
    [SerializeField] private float timeBetweenTakeEffect = 2;

    private void OnEnable() {
        currentTime = 0;
        ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.SPAWN);
    }

    private void OnDisable() {
        ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.DEATH);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<DynamicObjectHitting>() != null) TakeHit(damage, other.gameObject);
        else if(other.gameObject.transform.parent.GetComponent<DynamicObjectHitting>() != null) TakeHit(damage, other.transform.parent.gameObject); 
    }

    public void TakeHit(float damage, GameObject receiver)
    {
        if(!receiver.CompareTag(currentOwner.tag)){
            StartCoroutine(AreaDamageTakeHit(damage, receiver));
        }
    }

    protected virtual IEnumerator AreaDamageTakeHit(float damage, GameObject receiver){
        while(true){
            Collider2D receiverCollider = null;
            if(receiver.GetComponent<Collider2D>()) receiverCollider = receiver.GetComponent<Collider2D>();
            else receiverCollider = receiver.GetComponentInChildren<Collider2D>();
            
            if(areaCollider2D.IsTouching(receiverCollider)){

                if(damage > 0){DamagePopUpVisualEffect.CreateDamagePopUp(receiver.transform.position + new Vector3(-1 * 0.7f * receiver.transform.localScale.x, 0.4f), Quaternion.identity, () => {
                    Dictionary<string, object> data = new()
                        {
                            { "isCritical", false },
                            { "value", damage }
                        };
                    return data;
                });}
                
                receiver.GetComponent<IHaveHealth>().GetHit(damage); 
                ((IHitablee)this).HittingEffectsPerform(gameObject, receiver, hittingObjectEffectSOs);
            }
            else{
                break;
            }
            yield return new WaitForSeconds(timeBetweenTakeEffect);
        }
    }
}