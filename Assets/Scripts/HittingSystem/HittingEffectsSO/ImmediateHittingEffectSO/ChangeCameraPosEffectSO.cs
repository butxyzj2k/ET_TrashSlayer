using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HittingEffectsSO/ChangeCameraPosEffectSO")]
public class ChangeCameraPosEffectSO : ImmediateHittingEffectSO
{
    [SerializeField] Vector3 targetPos;
    public override void HittingEffectsPerform(GameObject sender, GameObject _receiver)
    {
        Obsever.PostEvent(EventID.Tutorial_ColliderMoveCamera_OnTrigger, new KeyValuePair<string, object>("position", targetPos));
        Obsever.PostEvent(EventID.Tutorial_ColliderMoveCamera_OnTrigger, new KeyValuePair<string, object>(null, TutorialCanvasController.TutorialNumber.Tuto3));
    }
}