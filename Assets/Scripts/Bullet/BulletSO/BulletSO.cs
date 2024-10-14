using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BulletSO")]
public class BulletSO : ScriptableObject
{
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private Sprite bulletSprite;
    [SerializeField] private string bulletName;
    public GameObject BulletObject { get => bulletObject; set => bulletObject = value; }
    public Sprite BulletSprite { get => bulletSprite; set => bulletSprite = value; }
    public string BulletName { get => bulletName; set => bulletName = value; }
}
