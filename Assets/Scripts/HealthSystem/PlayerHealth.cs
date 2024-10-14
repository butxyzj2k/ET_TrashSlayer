using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : Health, IHitttable
{
    Animator anim;

    [SerializeField] private HittingEffectsSO hittingEffectsSO;

    private void Awake() {
        anim = GetComponent<Animator>();
    }
    
    public void TakeHit(float damage, GameObject receiver)
    {
        if(!receiver.GetComponent<Collider2D>().IsTouching(gameObject.GetComponent<CapsuleCollider2D>())) return;
        if(receiver.GetComponent<IHitttable>() == null) return;
        if(receiver.GetComponent<WeaponHealth>() != null && string.Compare(gameObject.name, receiver.GetComponent<WeaponHealth>().ObjectOwner) != 0){
            if(receiver.GetComponent<ShieldHealth>() != null) return;
            HittingEffectsPerform(gameObject, receiver, hittingEffectsSO);
            receiver.GetComponent<Health>().CurentHealth -= damage;
            receiver.GetComponent<Health>().HealthManager();
        }
    }

    public void HittingEffectsPerform(GameObject sender, GameObject _receiver, HittingEffectsSO hittingEffectsSO)
    {
        // GameObject stateVFXObject = hittingEffectsSO.HittingEffects(sender, _receiver);
        // InvokeExtensionCode.Invoke(SceneGameManager.sceneGameManager, () => hittingEffectsSO.ResetHittingEffects(sender, _receiver, stateVFXObject), hittingEffectsSO.TimeEffect);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        TakeHit(1, other.gameObject);
    }

    public void PlayerHurtAnim(){
        if(!isHurt){
            // healthBar.SetCurrentBar(curentHealth, curentHealth.ToString());
            // canCheckHurt = false;
            isHurt = true;
            anim.SetBool("isHurt", true);
            StartCoroutine(ResetHurt()); 
        }
    }

    IEnumerator ResetHurt(){
        yield return new WaitForSeconds(0.1f);
        isHurt = false;
        anim.SetBool("isHurt", false);
        // yield return new WaitForSeconds(0.2f);
        // canCheckHurt = true;
    }

    public void PlayerDeathAnim(){
        // healthBar.SetCurrentBar(curentHealth, curentHealth.ToString());
        isDeath = true;
        anim.SetBool("isDeath", true);
    }
}
