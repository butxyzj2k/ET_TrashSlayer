using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletNumberImage : MonoBehaviour
{
    [SerializeField] protected Image image;

    protected Action<KeyValuePair<string, object>> ChangeBulletImageDelegate;

    private void Start() {
        ChangeBulletImageDelegate = (param) => {
            if (param.Key == null) return;
            if(!param.Key.Equals("sprite")) return;
            ChangeBulletImage((Sprite)param.Value);
        };

        Obsever.AddListener(EventID.BulletBox_OnChangeBullet, ChangeBulletImageDelegate);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.BulletBox_OnChangeBullet, ChangeBulletImageDelegate);
    }

    public void ChangeBulletImage(Sprite bulletSprite){
        image.sprite = bulletSprite;
    }
}
