using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CalculateResultTimeText : CalculateResultComponentText
{
    protected override void SetTextResult()
    {
        textResult = SecondToMinuteText((int)GameSession.instance.timeGamePlay);
    }

    string SecondToMinuteText(int timePlay){
        int minute = timePlay/60;
        int second = timePlay - minute*60;
        return minute.ToString() +":"+second.ToString();
    }
}