using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HittingEffectsSO/GiveHealthEffectSO")]
public class GiveHealthEffectSO : ImmediateHittingEffectSO
{
    [SerializeField] float HealthRate = 0.02f;

    public override void HittingEffectsPerform(GameObject sender, GameObject _receiver)
    {
        PlayerHitting playerHitting = _receiver.GetComponent<PlayerHitting>();

        if(playerHitting.CurrentHealth + HealthRate > playerHitting.MaxHealth) return;
        ((IHaveHealth)playerHitting).GetHit(-HealthRate);
        Obsever.PostEvent(EventID.PlayerHpBar_OnChangeHp, new KeyValuePair<string, object>("health", playerHitting.CurrentHealth));
    }
}
