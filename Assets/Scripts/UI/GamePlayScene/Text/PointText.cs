using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PointText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointText;
    public int currentPoint = 0;
    Action<KeyValuePair<string, object>> EnemyDeathAddPointDelegate;
    Action<KeyValuePair<string, object>> BossDeathAddPointDelegate;

    private void Start() {
        EnemyDeathAddPointDelegate = (param) => {
            AddPoint(10);
        };

        BossDeathAddPointDelegate = (param) => {
            if(param.Key != null) return;
            AddPoint(50);
        };

        Obsever.AddListener(EventID.BOSS_DEATH, BossDeathAddPointDelegate);
        Obsever.AddListener(EventID.Enemy_DEATH, EnemyDeathAddPointDelegate);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.BOSS_DEATH, BossDeathAddPointDelegate);
        Obsever.RemoveListener(EventID.Enemy_DEATH, EnemyDeathAddPointDelegate);
    }

    public void AddPoint(int pointAdded){
        currentPoint += pointAdded;
        pointText.text = " Point: " + currentPoint;
    }
}
