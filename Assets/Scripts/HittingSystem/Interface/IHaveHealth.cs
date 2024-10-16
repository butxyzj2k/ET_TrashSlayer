using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IHaveHealth 
{
    public int MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    protected UnityEvent OnHit { get; set; }
    protected UnityEvent OnDeath { get; set; }

    public void InitalizeHealth(int initialHealth){
        if(initialHealth <= 0){
            CurrentHealth = 1;
        }
        CurrentHealth  = initialHealth;
    }

    public void DestroyObject();

    public void GetHit(float damage){
        if(CurrentHealth < 0) return;
        CurrentHealth -= damage;
        if(damage > 0){
            HealthManager();
        } 
    }

    public void HealthManager(){
        if(CurrentHealth <= 0) OnDeath?.Invoke();        
        else OnHit?.Invoke();
    }
}