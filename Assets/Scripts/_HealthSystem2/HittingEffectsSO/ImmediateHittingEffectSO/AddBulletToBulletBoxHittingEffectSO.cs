using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HittingEffectsSO/AddBulletToBulletBoxHittingEffectSO")]
public class AddBulletToBulletBoxHittingEffectSO : ImmediateHittingEffectSO
{
    [SerializeField] BulletSO bulletSO;
    public override void HittingEffectsPerform(GameObject sender, GameObject _receiver)
    {
       _receiver.GetComponentInChildren<IBox>().AddObjectToBox(bulletSO);
    }
}