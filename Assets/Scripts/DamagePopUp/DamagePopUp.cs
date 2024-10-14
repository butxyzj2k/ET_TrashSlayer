using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    // private static PoolObject damagePopUpPoolObject;
    // private TextMeshPro damagePopUpText;
    // private Color textColor;

    // [SerializeField] private float moveSpeed;

    // [SerializeField] private float aliveTime;
    // private float aliveTimer;

    // public TextMeshPro DamagePopUpText { get => damagePopUpText; set => damagePopUpText = value; }
    // public Color TextColor { get => textColor; set => textColor = value; }

    // private void Awake() {
    //     damagePopUpPoolObject = FindObjectOfType<DamagePopUpPoolObject>();
    //     damagePopUpText = GetComponent<TextMeshPro>();
    //     textColor = damagePopUpText.color;

    //     aliveTimer = aliveTime;
    // }

    // private void OnEnable() {
    //     aliveTimer = aliveTime;
    //     gameObject.transform.localScale = Vector3.one;
    //     textColor.a = 1;
    // }

    // private void Update() {
    //     aliveTimer -= Time.deltaTime;
    //     if(aliveTimer < 0){
    //         gameObject.SetActive(false);
    //     }
    //     DamgePopUpMoveUp();
    //     DamagePopUpEffect();
    // }

    // public static void CreateDamagePopUp(float value, Vector3 position, bool isCritical){
    //     GameObject damagePopUpObject = damagePopUpPoolObject.GetObjectInPool();
        
    //     damagePopUpObject.transform.position = position;
    //     damagePopUpObject.GetComponent<DamagePopUp>().DamagePopUpText.text = value.ToString();

    //     if(isCritical){
    //         damagePopUpObject.GetComponent<DamagePopUp>().TextColor = Color.red;
    //         damagePopUpObject.GetComponent<DamagePopUp>().DamagePopUpText.color = damagePopUpObject.GetComponent<DamagePopUp>().TextColor;
    //     }
    //     else{
    //         damagePopUpObject.GetComponent<DamagePopUp>().TextColor = Color.yellow;
    //         damagePopUpObject.GetComponent<DamagePopUp>().DamagePopUpText.color = damagePopUpObject.GetComponent<DamagePopUp>().TextColor;
    //     }

    //     damagePopUpObject.SetActive(true);
    // }

    // public void DamgePopUpMoveUp(){
    //     if(aliveTimer > aliveTime * 0.5f){
    //         moveSpeed +=  Time.deltaTime;
    //     }
    //     else{
    //         moveSpeed -=  Time.deltaTime;
    //     }
    //     transform.position = transform.position + moveSpeed * Time.deltaTime * Vector3.up;
    // }

    // public void DamagePopUpEffect(){
    //     if(aliveTimer > aliveTime * 0.5f){
    //         gameObject.transform.localScale += 0.5f * Time.deltaTime * new Vector3(1, 1, 1);
    //     }
    //     else{
    //         gameObject.transform.localScale -= 0.5f * Time.deltaTime * new Vector3(1, 1, 1);
    //         textColor.a -= 2.5f * Time.deltaTime;
    //         damagePopUpText.color = textColor;
    //     }
    // }


}
