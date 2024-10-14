using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : Bar
{
    Action<KeyValuePair<string, object>> PlayerSetMaxBarDelegate;
    Action<KeyValuePair<string, object>> PlayerSetCurrentBarSpawnDelegate;
    Action<KeyValuePair<string, object>> PlayerSetCurrentBarDelegate;

    private void Start() {

        PlayerSetMaxBarDelegate = (pram) => {
            if(pram.Key == null) return;
            if(!pram.Key.Equals("maxHealth")) return;
            SetMaxBar((int)pram.Value);
        };

        PlayerSetCurrentBarSpawnDelegate = (pram) => {
            if(pram.Key == null) return;
            if(!pram.Key.Equals("maxHealth")) return;
            SetCurrentBar((int)pram.Value);
            SetTextInBar(pram.Value.ToString());
        };
        
        PlayerSetCurrentBarDelegate = (pram) => {
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

        Obsever.AddListener(EventID.Player_SPAWN, PlayerSetMaxBarDelegate);
        Obsever.AddListener(EventID.Player_SPAWN, PlayerSetCurrentBarSpawnDelegate);
        Obsever.AddListener(EventID.Player_HURT, PlayerSetCurrentBarDelegate);
        Obsever.AddListener(EventID.Player_DEATH, PlayerSetCurrentBarDelegate);

        Obsever.AddListener(EventID.PlayerHpBar_OnChangeHp, PlayerSetCurrentBarDelegate);
        Obsever.AddListener(EventID.PlayerHpBar_OnChangeHp, PlayerSetMaxBarDelegate);
        Obsever.AddListener(EventID.PlayerHpBar_OnChangeHp,  PlayerSetCurrentBarDelegate);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.Player_SPAWN, PlayerSetMaxBarDelegate);
        Obsever.RemoveListener(EventID.Player_SPAWN, PlayerSetCurrentBarSpawnDelegate);
        Obsever.RemoveListener(EventID.Player_HURT, PlayerSetCurrentBarDelegate);
        Obsever.RemoveListener(EventID.Player_DEATH, PlayerSetCurrentBarDelegate);

        Obsever.RemoveListener(EventID.PlayerHpBar_OnChangeHp, PlayerSetCurrentBarDelegate);
        Obsever.RemoveListener(EventID.PlayerHpBar_OnChangeHp, PlayerSetMaxBarDelegate);
        Obsever.RemoveListener(EventID.PlayerHpBar_OnChangeHp, PlayerSetCurrentBarDelegate);
    }
}
