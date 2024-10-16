using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HittingEffectsSO/GiveExpEffectSO")]
public class GiveExpEffectSO : ImmediateHittingEffectSO
{
    [SerializeField] int ExpAdded = 2;
    public override void HittingEffectsPerform(GameObject sender, GameObject _receiver)
    {
        _receiver.GetComponent<PlayerSkill>().AddExp(ExpAdded);
    }
}
