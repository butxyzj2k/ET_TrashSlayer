using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FlyController : MonoBehaviour
{
    [SerializeField] Vector3[] path;
    [SerializeField] float duration;
    [SerializeField] PathType pathType;
    [SerializeField] PathMode pathMode;

    void Start()
    {
        FlyMovingVFX();
        InvokeRepeating(nameof(FlyRotateVFX), 0f, duration);
    }

    public void FlyRotateVFX(){
        transform.Rotate(0, 0, 180);
    }

    public void FlyMovingVFX()
    {
        transform.DOLocalPath(path, duration, pathType, pathMode, 10, Color.red)
        .SetEase(Ease.Linear)
        .SetLoops(-1, LoopType.Yoyo);
    }
}
