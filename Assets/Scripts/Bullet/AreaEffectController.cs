using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffectController : WeaponController
{
    public void ReleaseAreaEffectShakeCamera(float _shakeAmplitude){
        CameraController.instance.ShakeCameraPerform(_shakeAmplitude);
    }
}