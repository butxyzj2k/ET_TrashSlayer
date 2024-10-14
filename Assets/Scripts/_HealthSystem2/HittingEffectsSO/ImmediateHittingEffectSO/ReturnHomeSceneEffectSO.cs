using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HittingEffectsSO/ReturnHomeSceneEffectSO")]
public class ReturnHomeSceneEffectSO : ImmediateHittingEffectSO
{
    public override void HittingEffectsPerform(GameObject sender, GameObject _receiver)
    {
        Debug.Log("Post");
        Obsever.PostEvent(EventID.UI_HomeButton_OnClick, new KeyValuePair<string, object>(null, null));
    }
}