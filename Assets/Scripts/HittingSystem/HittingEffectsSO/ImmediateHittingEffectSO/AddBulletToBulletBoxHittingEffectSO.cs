using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HittingEffectsSO/AddBulletToBulletBoxHittingEffectSO")]
public class AddBulletToBulletBoxHittingEffectSO : ImmediateHittingEffectSO
{
    [SerializeField] BulletSO bulletSO;
    public override void HittingEffectsPerform(GameObject sender, GameObject _receiver)
    {
        BulletBox bulletBox = _receiver.GetComponentInChildren<BulletBox>();

       bulletBox.AddObjectToBox(bulletSO);
       if(bulletBox.CurrentIndex == 0) bulletBox.ChangeObjectInBox();
    }
}