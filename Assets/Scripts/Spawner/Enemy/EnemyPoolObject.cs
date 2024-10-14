using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolObject : PoolObject
{
    [SerializeField] protected float moveSpeedRate = 1;
    [SerializeField] protected float damageRate = 1;
    [SerializeField] protected int healRate = 1;

    public override void SetOnePool()
    {
        throw new System.NotImplementedException();
    }

    private void Start() {
        InitalizePoolObject();
    }

    public override void InitalizePoolObject()
    {
        SceneGameManager.instance.StartCoroutine(InitalizeEnemyPoolObjectCourotine());
    }

    public void ChangeEnemyAtribute(List<GameObject> poolObjects){
        foreach(GameObject obj in poolObjects){
            obj.GetComponent<EnemyMovement>().CurrentSpeed *= moveSpeedRate;
            obj.GetComponent<EnemyMovement>().InitialSpawnSpeed = obj.GetComponent<EnemyMovement>().CurrentSpeed;
            obj.GetComponent<EnemyMovement>().BaseSpeed = obj.GetComponent<EnemyMovement>().CurrentSpeed;
            obj.GetComponent<EnemyAttack>().DamageRate *= damageRate;
            obj.GetComponent<EnemyAttack>().DefaultDamageRate = obj.GetComponent<EnemyAttack>().DamageRate;
            obj.GetComponent<EnemyHitting>().MaxHealth += healRate;
            obj.GetComponent<IHaveHealth>().InitalizeHealth(obj.GetComponent<EnemyHitting>().MaxHealth);
        }
    }

    IEnumerator InitalizeEnemyPoolObjectCourotine(){
        while(PlayerController.instance == null){
            yield return null;
        }
        base.InitalizePoolObject();
        ChangeEnemyAtribute(poolObjects);
    }

}