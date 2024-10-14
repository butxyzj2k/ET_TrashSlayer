using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateVisualEffect : VisualEffect
{
    private Transform targetTransform; 
    public Transform TargetTransform { get => targetTransform; set => targetTransform = value; }
    protected override IEnumerator PlayVisualEffect(Vector3 position, Quaternion rotation, Func<Dictionary<string, object>> data)
    {
        timeToPlayVFX = (float)data()["timeToPlayVFX"];
        targetTransform = (Transform)data()["targetTransform"];
        gameObject.transform.SetPositionAndRotation(position, rotation);
        gameObject.SetActive(true);

        float time = 0;
        while(time <= timeToPlayVFX){
            time += Time.deltaTime;

            if(!targetTransform.gameObject.activeInHierarchy) break;
            
            transform.localScale = targetTransform.transform.localScale;
            transform.position = targetTransform.transform.position;
            
            yield return null;
        }

        gameObject.SetActive(false);
    }
}