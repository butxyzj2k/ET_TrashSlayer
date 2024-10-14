using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : Health, IHitttable
{
    private Animator anim;
    private EnemyCreateOrb enemyCreateOrb;
    [SerializeField] int pointAddToPlayer = 10;

    [SerializeField] private HittingEffectsSO hittingEffectsSO;

    private void Awake() {
        anim = GetComponent<Animator>();
        enemyCreateOrb = GetComponent<EnemyCreateOrb>();
    }

    private void Start() {
        SetSFXPoolObject();
        InitalizeHealth(maxHealth);
        // if(healthBar != null){
        //     healthBar.transform.parent.gameObject.SetActive(true);
        //     healthBar.SetMaxBar(maxHealth);
        //     healthBar.SetCurrentBar(maxHealth, maxHealth.ToString());
        // }
    }
    
    private void OnEnable() {
        // isHurt = false;
        // isDeath = false;
        // anim.SetBool("isDeath", false);
        // PlaySpawnSFX();
        // InitalizeHealth(maxHealth);
        // if(gameObject.GetComponent<Collider2D>()){
        //     gameObject.GetComponent<Collider2D>().enabled = true;
        // }
        // else if(gameObject.GetComponentInChildren<Collider2D>()){
        //     gameObject.GetComponentInChildren<Collider2D>().enabled = true;
        // }
        // if(healthBar != null){
        //     healthBar.transform.parent.gameObject.SetActive(true);
        //     healthBar.SetMaxBar(maxHealth);
        //     // healthBar.SetCurrentBar(maxHealth, maxHealth.ToString());
        // }
    }

    private void OnDisable() {
        if(isDeath) FindObjectOfType<PointText>().AddPoint(pointAddToPlayer);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<IHitttable>() == null) return;
        if(other.gameObject.GetComponent<WeaponHealth>() != null && string.Compare(tag, other.gameObject.GetComponent<WeaponHealth>().ObjectOwner) != 0){
            if(other.gameObject.GetComponent<ShieldHealth>() != null) return;
            TakeHit(1, other.gameObject);
            // other.gameObject.SetActive(false);
        }
    }


    public void TakeHit(float damage, GameObject receiver)
    {
        HittingEffectsPerform(gameObject, receiver, hittingEffectsSO);
        receiver.GetComponent<Health>().CurentHealth -= damage;
        receiver.GetComponent<Health>().HealthManager();
    }

    public void HittingEffectsPerform(GameObject sender, GameObject _receiver, HittingEffectsSO hittingEffectsSO)
    {
        // GameObject stateVFXObject = hittingEffectsSO.HittingEffects(sender, _receiver);
        // InvokeExtensionCode.Invoke(SceneGameManager.sceneGameManager, () => hittingEffectsSO.ResetHittingEffects(sender, _receiver, stateVFXObject), hittingEffectsSO.TimeEffect);
    }

    public void EnemyHurtAnim(){
        isHurt = true;
        anim.SetBool("isHurt", true);
        StartCoroutine(ResetHurt());
    }

    IEnumerator ResetHurt(){
        yield return new WaitForSeconds(GetComponent<ObjectGetClipIn4>().hurtTime);
        isHurt = false;
        anim.SetBool("isHurt", false);
        // yield return new WaitForSeconds(0.2f);
        // canCheckHurt = true;
    }

    public void EnemyDeathAnim(){
        isDeath = true;
        anim.SetBool("isDeath", true);
    }

    public void EnemyDeathDelay(){
        if(gameObject.GetComponent<Collider2D>()){
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        else if(gameObject.GetComponentInChildren<Collider2D>()){
            gameObject.GetComponentInChildren<Collider2D>().enabled = false;
        }
        Invoke(nameof(DestroyObject), GetComponent<ObjectGetClipIn4>().deathTime);
        enemyCreateOrb.CreteOrb();
        if(healthBar != null){
            healthBar.transform.parent.gameObject.SetActive(false);
        }
    }   
}
              