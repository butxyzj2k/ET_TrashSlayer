using System;
using System.Collections;
using UnityEngine;

public class BlueEnemyAttack : EnemyAttack
{
    [SerializeField] private float timeResetShield;
    private bool canCreateShield = false;

    private void OnEnable() {
        isAttack = false;
        canAttack = true;
        canCreateShield = false;


        float x = UnityEngine.Random.Range(1, 10);
        if(x < 3.5) canCreateShield = true;
        else StartCoroutine(ResetCreateShield());
    }

    private void Update() {
        CreateShield();
    }   

    public void CreateShield() {
        if(canCreateShield){
            canCreateShield = false;
            SetCurrentObjectSkill("Shield");
            PerformAttack();
            StartCoroutine(ResetCreateShield());
        }
    }

    IEnumerator ResetCreateShield()
    {
        yield return new WaitForSeconds(0.2f);
        SetCurrentObjectSkill("Meelee");
        yield return new WaitForSeconds(timeResetShield);
        canCreateShield = true;
    }
}