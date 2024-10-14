using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffect : BaseEffect
{
    [SerializeField] protected float timeToPlayVFX = 0.3f;
    public float TimeToPlayVFX { get => timeToPlayVFX; set => timeToPlayVFX = value; }
    public override void ReleaseObject(Vector3 position, Quaternion rotation, Func<Dictionary<string, object>> data){
        SceneGameManager.instance.StartCoroutine(PlayVisualEffect(position, rotation, data));
    }

    protected virtual IEnumerator PlayVisualEffect(Vector3 position, Quaternion rotation, Func<Dictionary<string, object>> data){
        gameObject.transform.SetPositionAndRotation(position, rotation);
        gameObject.SetActive(true);
        yield return new WaitForSeconds(timeToPlayVFX);
        gameObject.SetActive(false);
    }
}
