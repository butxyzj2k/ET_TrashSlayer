using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletNumberText : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textMeshProUGUI;

    protected Action<KeyValuePair<string, object>> ChangeTextNumberDelegate;

    private void Start() {
        ChangeTextNumberDelegate = (param) => {
            if (param.Key == null) return;
            if(!param.Key.Equals("text")) return;
            ChangeBulletNumberText(param.Value.ToString());
        };

        Obsever.AddListener(EventID.BulletBox_OnChangeBullet, ChangeTextNumberDelegate);
        Obsever.AddListener(EventID.BulletBox_OnAddOrMinusBullet, ChangeTextNumberDelegate);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.BulletBox_OnChangeBullet, ChangeTextNumberDelegate);
        Obsever.RemoveListener(EventID.BulletBox_OnAddOrMinusBullet, ChangeTextNumberDelegate);
    }

    public void ChangeBulletNumberText(string numberText){
        textMeshProUGUI.text = numberText;
    }
}