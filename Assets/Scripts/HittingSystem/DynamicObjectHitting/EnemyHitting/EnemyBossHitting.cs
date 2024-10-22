using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBossHitting : EnemyHitting
{
    Action<KeyValuePair<string, object>> EnemyBossPlayDeathSFXDelegate;
    Action<KeyValuePair<string, object>> EnemyBossDeathDelayDelegate;
    Action<KeyValuePair<string, object>> EnemyBossSpawnDelegate;

    private void Start() {
        EnemyBossPlayDeathSFXDelegate = (pram) => {
            if(pram.Key == null) return;
            if(!pram.Key.Equals("GameObject")) return;
            if((GameObject)pram.Value == gameObject) ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.DEATH);
        };

        EnemyBossDeathDelayDelegate = (pram) => {
            if(pram.Key == null) return;
            if(!pram.Key.Equals("GameObject")) return;
            if((GameObject)pram.Value == gameObject) DynamicObjectDeathDelay();
        };

        EnemyBossSpawnDelegate = (pram) => {
            if(pram.Key == null) return;
            
            if(!pram.Key.Equals("GameObject")) return;
            if((GameObject)pram.Value != gameObject) return;

            if(gameObject.GetComponent<Collider2D>()) gameObject.GetComponent<Collider2D>().enabled = true;
            
            else if(gameObject.GetComponentInChildren<Collider2D>()) gameObject.GetComponentInChildren<Collider2D>().enabled = true;
            

            ((IHitablee)this).PlayHittingSFX(IHitablee.SFXPrefab.SPAWN);

            isHurt = false;
            isDeath = false;

            anim.SetBool("isDeath", false);
            ((IHaveHealth)this).InitalizeHealth(maxHealth);
        };


        // add listener
        Obsever.AddListener(EventID.BOSS_DEATH, EnemyBossPlayDeathSFXDelegate);
        
        Obsever.AddListener(EventID.BOSS_DEATH, EnemyBossDeathDelayDelegate);        
        
        Obsever.AddListener(EventID.BOSS_SPAWN, EnemyBossSpawnDelegate);
        
        
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        // remove listener
        Obsever.RemoveListener(EventID.BOSS_DEATH, EnemyBossPlayDeathSFXDelegate);
        
        Obsever.RemoveListener(EventID.BOSS_DEATH, EnemyBossDeathDelayDelegate);        
        
        Obsever.RemoveListener(EventID.BOSS_SPAWN, EnemyBossSpawnDelegate);
    }

    public override void HealthManager(){
        if(CurrentHealth <= 0){
            Obsever.PostEvent(EventID.BOSS_DEATH, new KeyValuePair<string, object>("GameObject", gameObject));  
            Obsever.PostEvent(EventID.BOSS_DEATH, new KeyValuePair<string, object>("health", currentHealth));
            Obsever.PostEvent(EventID.BOSS_DEATH, new KeyValuePair<string, object>(null, null));
        }        
        else{
            Obsever.PostEvent(EventID.BOSS_HURT, new KeyValuePair<string, object>("GameObject", gameObject));
            Obsever.PostEvent(EventID.BOSS_HURT, new KeyValuePair<string, object>("health", currentHealth));
        }
    }
}