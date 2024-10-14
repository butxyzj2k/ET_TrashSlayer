using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class HittingEffectsSO : ScriptableObject
{   
    public abstract void HittingEffectsPerform(GameObject sender ,GameObject _receiver);
}
