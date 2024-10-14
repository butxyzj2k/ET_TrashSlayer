using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff/BoostTimeEffectInHittingEffect")]
public class BoostTimeEffectInHittingEffectSkillPatternSO : SkillPatternSO
{
    Action<KeyValuePair<string, object>> ResetHittingEffectsDelegate;
    [SerializeField] float BoostTimeEffectRate;
    public override void PerformSkill()
    {
        var timedHittingEffectSOs = Resources.FindObjectsOfTypeAll(typeof(TimedHittingEffectSO));

        if(timedHittingEffectSOs.Length > 0){
            foreach(var timedHittingEffectSO in timedHittingEffectSOs){
                ((TimedHittingEffectSO)timedHittingEffectSO).TimeEffect = ((TimedHittingEffectSO)timedHittingEffectSO).TimeEffect + ((TimedHittingEffectSO)timedHittingEffectSO).TimeEffect * BoostTimeEffectRate;
            }
        }

        ResetHittingEffectsDelegate = (param) => {
            ResetHittingEffects();
        };

        Obsever.AddListener(EventID.UI_HomeButton_OnClick, ResetHittingEffectsDelegate);
    }

    public void ResetHittingEffects(){
        var timedHittingEffectSOs = Resources.FindObjectsOfTypeAll(typeof(TimedHittingEffectSO));
        if(timedHittingEffectSOs.Length > 0){
            foreach(var timedHittingEffectSO in timedHittingEffectSOs){
                ((TimedHittingEffectSO)timedHittingEffectSO).TimeEffect = ((TimedHittingEffectSO)timedHittingEffectSO).DefaultTimeEffect;
            }
        }
    }
}
