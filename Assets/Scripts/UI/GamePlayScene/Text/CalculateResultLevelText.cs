using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CalculateResultLevelText : CalculateResultComponentText
{
    protected override void SetTextResult()
    {
        textResult = (SceneManager.GetActiveScene().buildIndex - 1).ToString();
    }
}