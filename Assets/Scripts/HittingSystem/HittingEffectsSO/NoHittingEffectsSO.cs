using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HittingEffectsSO/NoHittingEffectsSO")]
public class NoHittingEffectsSO : HittingEffectsSO
{
    public override void HittingEffectsPerform(GameObject sender, GameObject _receiver)
    {
        
    }
}
