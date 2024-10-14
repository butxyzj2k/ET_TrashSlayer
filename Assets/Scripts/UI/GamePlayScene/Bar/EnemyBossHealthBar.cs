using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBossHealthBar : Bar
{
    Action<KeyValuePair<string, object>> EnemyBossActiveBarDelegate;
    Action<KeyValuePair<string, object>> EnemyBossSetMaxBarDelegate;
    Action<KeyValuePair<string, object>> EnemyBossSetCurrentBarSpawnDelegate;
    Action<KeyValuePair<string, object>> EnemyBossSetCurrentBarDelegate;

    private void Start() {
        EnemyBossActiveBarDelegate = (pram) => {
            if(gameObject.transform.gameObject.activeInHierarchy) return;
            gameObject.transform.gameObject.SetActive(true);
        };

        EnemyBossSetMaxBarDelegate = (pram) => {
            if(pram.Key == null) return;
            if(!pram.Key.Equals("maxHealth")) return;
            SetMaxBar((int)pram.Value);
        };

        EnemyBossSetCurrentBarSpawnDelegate = (pram) => {
            if(pram.Key == null) return;
            if(!pram.Key.Equals("maxHealth")) return;
            SetCurrentBar((int)pram.Value);
            SetTextInBar(pram.Value.ToString());
        };
        
        EnemyBossSetCurrentBarDelegate = (pram) => {
            if(pram.Key == null) return;
            if(!pram.Key.Equals("health")) return;
            if((float)pram.Value <= 0) {
                SetCurrentBar(0);
                SetTextInBar(0.ToString());
            }
            else {
                SetCurrentBar((float)pram.Value);
                SetTextInBar(Math.Round((float)pram.Value, 2).ToString());
            }
        };

        Obsever.AddListener(EventID.BOSS_SPAWN, EnemyBossActiveBarDelegate);

        Obsever.AddListener(EventID.BOSS_SPAWN, EnemyBossSetMaxBarDelegate);

        Obsever.AddListener(EventID.BOSS_SPAWN, EnemyBossSetCurrentBarSpawnDelegate);

        Obsever.AddListener(EventID.BOSS_HURT, EnemyBossSetCurrentBarDelegate);
        
        Obsever.AddListener(EventID.BOSS_DEATH, EnemyBossSetCurrentBarDelegate);

        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.BOSS_SPAWN, EnemyBossActiveBarDelegate);

        Obsever.RemoveListener(EventID.BOSS_SPAWN, EnemyBossSetMaxBarDelegate);

        Obsever.RemoveListener(EventID.BOSS_SPAWN, EnemyBossSetCurrentBarSpawnDelegate);

        Obsever.RemoveListener(EventID.BOSS_HURT, EnemyBossSetCurrentBarDelegate);
        
        Obsever.RemoveListener(EventID.BOSS_DEATH, EnemyBossSetCurrentBarDelegate);
    }
}