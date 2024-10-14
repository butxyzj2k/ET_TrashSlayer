using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponHealth 
{
    protected float CurrentTime { get; set; }
    public float TimeExist { get; set; }
    public GameObject CurrentOwner { get; set; }
    public string CurrentTarget { get; set; }

    public void DestroyObjectAfterTimeExit(GameObject gameObject){
        CurrentTime += Time.deltaTime;
        if(CurrentTime > TimeExist){
            CurrentTime = 0;
            gameObject.SetActive(false);
        }
    }

    public void DestroyObjectWhenOwnerDead(GameObject gameObject){
        if(CurrentOwner != null){
            if(!CurrentOwner.activeInHierarchy) gameObject.SetActive(false);
        }
    }
}