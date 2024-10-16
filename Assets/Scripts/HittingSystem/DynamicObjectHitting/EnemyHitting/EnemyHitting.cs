using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EnemyHitting : DynamicObjectHitting
{

    Action<KeyValuePair<string, object>> EnemyPlayHurtSFXDelegate;
    Action<KeyValuePair<string, object>> EnemyPlayDeathSFXDelegate;
    Action<KeyValuePair<string, object>> EnemyHurtAnimDelegate;
    Action<KeyValuePair<string, object>> EnemyDeathDelayDelegate;
    Action<KeyValuePair<string, object>> EnemySpawnDelegate;

    private void Start() {
        EnemyPlayHurtSFXDelegate = (pram) => {
            if(!pram.Key.Equals("GameObject")) return;
            if((GameObject)pram.Value == gameObject) ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.HURT);
        };

        EnemyPlayDeathSFXDelegate = (pram) => {
            if(!pram.Key.Equals("GameObject")) return;
            if((GameObject)pram.Value == gameObject) ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.DEATH);
        };

        EnemyHurtAnimDelegate = (pram) => {
            if(!pram.Key.Equals("GameObject")) return;
            if((GameObject)pram.Value == gameObject) DynamicObjectHurtAnim();
        };

        EnemyDeathDelayDelegate = (pram) => {
            if(!pram.Key.Equals("GameObject")) return;
            if((GameObject)pram.Value == gameObject) DynamicObjectDeathDelay();
        };

        EnemySpawnDelegate = (pram) => {
            if(!pram.Key.Equals("GameObject")) return;
            if((GameObject)pram.Value != gameObject) return;
            if(gameObject.GetComponent<Collider2D>()){
                gameObject.GetComponent<Collider2D>().enabled = true;
            }
            else if(gameObject.GetComponentInChildren<Collider2D>()){
                gameObject.GetComponentInChildren<Collider2D>().enabled = true;
            }

            ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.SPAWN);

            isHurt = false;
            isDeath = false;

            anim.SetBool("isDeath", false);
            ((IHaveHealth)this).InitalizeHealth(maxHealth);
        };

         // add listener
        Obsever.AddListener(EventID.Enemy_HURT, EnemyPlayHurtSFXDelegate);
        Obsever.AddListener(EventID.Enemy_DEATH,EnemyPlayDeathSFXDelegate);
        Obsever.AddListener(EventID.Enemy_HURT, EnemyHurtAnimDelegate);
        Obsever.AddListener(EventID.Enemy_DEATH, EnemyDeathDelayDelegate);
        Obsever.AddListener(EventID.Enemy_SPAWN, EnemySpawnDelegate);
    }

    private void OnEnable() {
        //post event enmy spawn
        Obsever.PostEvent(EventID.Enemy_SPAWN, new KeyValuePair<string, object>("GameObject", gameObject));
    }

    private void OnDestroy() {
        // remove listener
        Obsever.RemoveListener(EventID.Enemy_HURT, EnemyPlayHurtSFXDelegate);
        Obsever.RemoveListener(EventID.Enemy_DEATH,  EnemyPlayDeathSFXDelegate);
        Obsever.RemoveListener(EventID.Enemy_HURT, EnemyHurtAnimDelegate);
        Obsever.RemoveListener(EventID.Enemy_DEATH, EnemyDeathDelayDelegate);
        Obsever.RemoveListener(EventID.Enemy_SPAWN, EnemySpawnDelegate);
    }

    public override void HealthManager(){
        if(CurrentHealth <= 0){
            Obsever.PostEvent(EventID.Enemy_DEATH, new KeyValuePair<string, object>("GameObject", gameObject));  
        }        
        else{
            Obsever.PostEvent(EventID.Enemy_HURT, new KeyValuePair<string, object>("GameObject", gameObject));
        }
    }
}