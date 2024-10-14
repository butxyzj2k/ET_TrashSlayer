using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHitting : DynamicObjectHitting
{

    Action<KeyValuePair<string, object>> PlayerPlayHurtSFXDelegate;
    Action<KeyValuePair<string, object>> PlayerHurtAnimDelegate;
    Action<KeyValuePair<string, object>> PlayerPlayDeathSFXDelegate;
    Action<KeyValuePair<string, object>> PlayerDeathDelayDelegate;
    Action<KeyValuePair<string, object>> PlayerInitalizeHealthDelegate;

   private void Start() {
        PlayerPlayHurtSFXDelegate = (pram) => ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.HURT);
        PlayerHurtAnimDelegate = (pram) => DynamicObjectHurtAnim();
        PlayerPlayDeathSFXDelegate = (pram) => ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.DEATH);
        PlayerDeathDelayDelegate = (pram) => DynamicObjectDeathDelay();
        PlayerInitalizeHealthDelegate = (pram) => ((IHaveHealth)this).InitalizeHealth(maxHealth);
        

        // add listener
        Obsever.AddListener(EventID.Player_HURT, PlayerPlayHurtSFXDelegate);
        Obsever.AddListener(EventID.Player_DEATH, PlayerPlayDeathSFXDelegate);
        Obsever.AddListener(EventID.Player_HURT, PlayerHurtAnimDelegate);
        Obsever.AddListener(EventID.Player_DEATH, PlayerDeathDelayDelegate);
        Obsever.AddListener(EventID.Player_SPAWN, PlayerInitalizeHealthDelegate);

        InvokeExtensionCode.Invoke(SceneGameManager.instance, () => Obsever.PostEvent(EventID.Player_SPAWN, new KeyValuePair<string, object>("maxHealth", maxHealth)), Time.unscaledDeltaTime);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.Player_HURT, PlayerPlayHurtSFXDelegate);
        Obsever.RemoveListener(EventID.Player_DEATH,  PlayerPlayDeathSFXDelegate);
        Obsever.RemoveListener(EventID.Player_HURT,  PlayerHurtAnimDelegate);
        Obsever.RemoveListener(EventID.Player_DEATH,  PlayerDeathDelayDelegate);
        Obsever.RemoveListener(EventID.Player_SPAWN,  PlayerInitalizeHealthDelegate);
    }

    public override void DynamicObjectTakeHit(float damage, GameObject receiver)
    {
        if(receiver.GetComponent<Collider2D>().IsTouching(gameObject.GetComponent<Collider2D>())) 
        {
            base.DynamicObjectTakeHit(damage, receiver);
        }
    }

    public override void HealthManager(){
        if(CurrentHealth <= 0){
            Obsever.PostEvent(EventID.Player_DEATH, new KeyValuePair<string, object>("health", currentHealth));  
            Obsever.PostEvent(EventID.SCENE_EndGame, new KeyValuePair<string, object>(null, null));  
        }        
        else{
            Obsever.PostEvent(EventID.Player_HURT, new KeyValuePair<string, object>("health", currentHealth));
        }
    }

    public override void DynamicObjectDeathDelay(){
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        base.DynamicObjectDeathDelay();
    }
}