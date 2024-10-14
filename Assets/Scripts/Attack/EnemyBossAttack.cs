using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBossAttack : EnemyAttack
{
    [SerializeField] float timeBetweemAttackSkill;
    [SerializeField] List<ObjectSkill> skillsAvailable = new();
    [SerializeField] List<ObjectSkill> skillsWaiting = new();
    [SerializeField] float timeDelayAttackAfterSpawn = 0.5f;
    private void Awake() {
        anim = GetComponent<Animator>();
        //SetUpArray
        for(int i = 0; i < skillsList.Count; i++){
            skillsAvailable.Add(skillsList[i]) ;
        }
    }

    private void OnEnable() {
        canAttack = false;
        StartCoroutine(DelayAttackAfterSpawn());
    }

    //Boss sẽ thực hiện tấn công sau 1 khoảng delay sau spawn
    IEnumerator DelayAttackAfterSpawn(){
        yield return new WaitForSeconds(timeDelayAttackAfterSpawn);
        canAttack = true;
    }


    public override IEnumerator EnemyAttackPerform()
    {
        TargetAttackPosition = targetTransform.position;

        //Lấy 1 Skill bất kỳ trong danh sách để bắt đầu tấn công
        int randomAttack = (int)UnityEngine.Random.Range(0, skillsAvailable.Count);
        SetCurrentObjectSkill(skillsAvailable[randomAttack].skillName);
        skillsWaiting.Add(skillsAvailable[randomAttack]);
        skillsAvailable.RemoveAt(randomAttack);
        
        yield return base.EnemyAttackPerform();
    }

    public override IEnumerator ResetAttack(float timeToResetAttack)
    {
        yield return null;
        timeToResetAttack = timeBetweemAttackSkill;
        SceneGameManager.instance.StartCoroutine(base.ResetAttack(timeToResetAttack));
        ResetListObjectSkills();
    }

    public void ResetListObjectSkills(){
        if(skillsAvailable.Count == 0){
            for(int i = 0; i < skillsWaiting.Count; i++){
                skillsAvailable.Add(skillsWaiting[i]);
            }
            skillsWaiting.Clear();
        }
    }
}
