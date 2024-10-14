using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUpVisualEffect : VisualEffect
{
    private TextMeshPro damagePopUpText;
    private Color textColor;
    
    [SerializeField] private float moveSpeed;

    public TextMeshPro DamagePopUpText { get => damagePopUpText; set => damagePopUpText = value; }
    public Color TextColor { get => textColor; set => textColor = value; }

    private void Awake() {
        
        damagePopUpText = GetComponent<TextMeshPro>();
        textColor = damagePopUpText.color;

    }

    public static GameObject CreateDamagePopUp(Vector3 position, Quaternion rotation, Func<Dictionary<string, object>> data){
        return FindObjectOfType<DamagePopUpPoolObject>().GetObjectInPool(position, rotation, data);
    }

    protected override IEnumerator PlayVisualEffect(Vector3 position, Quaternion rotation, Func<Dictionary<string, object>> data)
    {
        gameObject.transform.SetPositionAndRotation(position, rotation);

        damagePopUpText.text = data()["value"].ToString();

        gameObject.transform.localScale = Vector3.one;
        textColor.a = 1;

        if((bool)data()["isCritical"]){
            textColor = Color.red;
            damagePopUpText.color = textColor;
        }
        else{
            textColor = Color.yellow;
            damagePopUpText.color = textColor;
        }
        
        gameObject.SetActive(true);
        float time = 0;
        
        while(time <= timeToPlayVFX){
            time += Time.deltaTime;

            if(time < timeToPlayVFX * 0.5f){
                moveSpeed +=  Time.deltaTime;
                gameObject.transform.localScale += 0.5f * Time.deltaTime * Vector3.one;
            }
            else{
                moveSpeed -=  Time.deltaTime;
                gameObject.transform.localScale -= 0.5f * Time.deltaTime * Vector3.one;
                textColor.a -= 0.05f * Time.deltaTime;
                damagePopUpText.color = textColor;
            }
            transform.position = transform.position + moveSpeed * Time.deltaTime * Vector3.up;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}