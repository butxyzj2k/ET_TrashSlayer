using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmediateHittingEffectSO : HittingEffectsSO
{
    public override void HittingEffectsPerform(GameObject sender, GameObject _receiver)
    {
        Debug.Log("Not override Immediate HittingEffects");
    }
}