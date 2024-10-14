using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ObjectAttack : MonoBehaviour
{
    [Serializable] 
    public struct ObjectSkill{
        public string skillName;
        public AttackPatternSO attackPatternSO;
        public GameObject skillWeaponPrefab;
        public string skillAnimationClip;
        public GameObject skillSFXPrefab;
        public bool sfxLoop;
        public float timeToStartAttack;
    }
    [SerializeField] protected List<ObjectSkill> skillsList = new();
    protected ObjectSkill currentObjectSkill;

    [SerializeField] protected float attackSpeedRate = 1;
    protected float defaultAttackSpeedRate = 1;
    [SerializeField] protected float damageRate = 1;
    protected float defaultDamageRate = 1;
    [SerializeField] protected Transform barrel;

    protected bool canAttack = true;
    protected bool isAttack = false;
    protected bool canResetCanAttack = true;


    public bool IsAttack { get => isAttack; set => isAttack = value; }
    public bool CanAttack { get => canAttack; set => canAttack = value; }
    public float DamageRate { get => damageRate; set => damageRate = value; }
    public float DefaultDamageRate { get => defaultDamageRate; set => defaultDamageRate = value; }
    public float AttackSpeedRate { get => attackSpeedRate; set => attackSpeedRate = value; }
    public float DefaultAttackSpeedRate { get => defaultAttackSpeedRate; set => defaultAttackSpeedRate = value; }
    public ObjectSkill CurrentObjectSkill { get => currentObjectSkill; set => currentObjectSkill = value; }
    public Transform Barrel { get => barrel; set => barrel = value; }
    public bool CanResetCanAttack { get => canResetCanAttack; set => canResetCanAttack = value; }

    public abstract void PerformAttack();
    public virtual void DelayAttack(){
        SceneGameManager.instance.StartCoroutine(ResetAttack(currentObjectSkill.attackPatternSO.AttackDelay * attackSpeedRate));
    }

    public virtual IEnumerator ResetAttack(float timeToResetAttack){
        float time = 0;
        while(time < timeToResetAttack){
            if(canResetCanAttack) time += Time.deltaTime;
            yield return null;
        }
        yield return null;
        canAttack = true;
    }

    public virtual void SetCurrentObjectSkill(string skillName){
        foreach(ObjectSkill skill in skillsList){
            if(string.Compare(skill.skillName, skillName) == 0){
                currentObjectSkill = skill;
            }
        }
    }

    public virtual void PlayObjectAttackSFX(){
        Debug.Log("Not override PlayObjectAttackSFX");
    }
}
