using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExpBar : Bar
{
    Action<KeyValuePair<string, object>> PlayerSetSpawnBarDelegate;
    Action<KeyValuePair<string, object>> PlayerSetCurrentBarDelegate;
    Action<KeyValuePair<string, object>> PlayerSetMaxBarDelegate;
    Action<KeyValuePair<string, object>> PlayerSetTextInBarDelegate;

    private void Start() {

        PlayerSetSpawnBarDelegate = (pram) => {
            if(pram.Key == null) return;
            if(!pram.Key.Equals("maxExp")) return;
            SetMaxBar((int)pram.Value);
            SetCurrentBar(0);
            SetTextInBar("LV.0");
        };
        
        PlayerSetCurrentBarDelegate = (pram) => {
            if(pram.Key == null) return;
            if(!pram.Key.Equals("exp")) return;
            SetCurrentBar((int)pram.Value);
        };

        PlayerSetMaxBarDelegate = (pram) => {
            if(pram.Key == null) return;
            if(!pram.Key.Equals("maxExp")) return;
            SetMaxBar((int)pram.Value);
        };

        PlayerSetTextInBarDelegate = (pram) => {
            if(pram.Key == null) return;
            if(!pram.Key.Equals("currentLevel")) return;
            SetTextInBar("LV." + pram.Value.ToString());
        };

        Obsever.AddListener(EventID.Player_SPAWN,  PlayerSetSpawnBarDelegate);
        Obsever.AddListener(EventID.Player_TAKEEXP,  PlayerSetCurrentBarDelegate);
        Obsever.AddListener(EventID.Player_LEVELUP,   PlayerSetMaxBarDelegate);
        Obsever.AddListener(EventID.Player_LEVELUP,   PlayerSetTextInBarDelegate);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.Player_SPAWN, PlayerSetSpawnBarDelegate);
        Obsever.RemoveListener(EventID.Player_TAKEEXP,  PlayerSetCurrentBarDelegate);
        Obsever.RemoveListener(EventID.Player_LEVELUP,   PlayerSetMaxBarDelegate);
        Obsever.RemoveListener(EventID.Player_LEVELUP,   PlayerSetTextInBarDelegate);
    }
}