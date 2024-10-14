using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Slider slider;
    [SerializeField] protected TextMeshProUGUI textInBar;  

    public void SetMaxBar(int _maxValue){
        slider.maxValue = _maxValue;
    }

    public void SetCurrentBar(float _value)
    {
        slider.value = _value;
    }

    public void SetTextInBar(string _text){
        textInBar.text = _text;
    }

}
