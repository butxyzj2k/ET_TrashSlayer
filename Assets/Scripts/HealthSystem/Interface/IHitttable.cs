using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitttable 
{
    void TakeHit(float damage, GameObject receiver);

    void HittingEffectsPerform(GameObject sender, GameObject _receiver, HittingEffectsSO hittingEffectsSO);
}
