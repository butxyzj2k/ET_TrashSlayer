using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "HittingEffectsSO/HypnosisEnemyHittingEffectSO")]
public class HypnosisEnemyHittingEffectSO : TimedHittingEffectSO
{
    public float detectionRadius = 5f;

    public override void HittingEffectsPerform(GameObject sender, GameObject _receiver)
    {
        if(_receiver.GetComponent<IHaveHealth>().CurrentHealth <= 0 || !_receiver.GetComponent<EnemyHitting>()) return;
        
        base.HittingEffectsPerform(sender, _receiver);
    }

    protected override IEnumerator ControlHittedObjectInHittingEffectCourotine(GameObject hittedObject){
        EnemyController enemyController = hittedObject.GetComponent<EnemyController>();
        EnemyMovement enemyMovement = hittedObject.GetComponent<EnemyMovement>();
        EnemyAttack enemyAttack = hittedObject.GetComponent<EnemyAttack>();
        Rigidbody2D enemyRb2d = hittedObject.GetComponent<Rigidbody2D>();

        CreateStateVFX(hittedObject);
        GameObject newTarget = null;

        float time = 0;
        while(time <= timeEffect){
            if(!hittedObject.activeInHierarchy) break;
            time += Time.deltaTime;
            
            //Tìm target mới gần nhất sau mỗi 1s hoặc newTarget đã bị giết
            if(time%1 <= 0.02f)
            {
                GameObject[] enemyActives = GameObject.FindGameObjectsWithTag("Enemy");
                newTarget = FindClosestEnemyWithinRadius(hittedObject, enemyActives);
                if(newTarget != null){
                    enemyController.TargetTransform = newTarget;
                    enemyController.SettingEnemyTargetTransform();
                }
            }
                    
            if(newTarget == null) enemyRb2d.velocity = new Vector2(-enemyRb2d.velocity.x, enemyRb2d.velocity.y); 
            
                
            //Attack
            if(enemyAttack.IsAttack){

                List<GameObject> bullets = new();
                bullets.AddRange(FindObjectsOfType<WeaponController>().Select(bullet => bullet.gameObject));

                foreach (var bullet in bullets)
                {
                    if (bullet.GetComponent<IWeaponHealth>().CurrentOwner == hittedObject)
                    {
                        bullet.GetComponent<IWeaponHealth>().CurrentOwner = PlayerController.instance.gameObject;
                        InvokeExtensionCode.Invoke(SceneGameManager.instance, () => bullet.GetComponent<IWeaponHealth>().CurrentOwner = hittedObject , timeEffect - time);
                    }
                } 
            }    
            yield return null;
        }
        SceneGameManager.instance.StartCoroutine(ResetHittingEffects(hittedObject));
    }

    protected override IEnumerator ResetHittingEffects(GameObject _receiver)
    {
        EnemyController enemyController = _receiver.GetComponent<EnemyController>();
        enemyController.TargetTransform = PlayerController.instance.transform.gameObject;
        enemyController.SettingEnemyTargetTransform();
        // _receiver.GetComponent<EnemyAttack>().Target = FindObjectOfType<PlayerManager>().gameObject.transform;
        // _receiver.GetComponent<EnemyMovement>().TargetTransform = FindObjectOfType<PlayerManager>().gameObject.transform;
        yield return null;
    }

    private GameObject FindClosestEnemyWithinRadius(GameObject centralEnemy, GameObject[] enemies)
    {
        GameObject closestEnemy = null;

        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = centralEnemy.transform.position;

        foreach (GameObject potentialTarget in enemies)
        {
            if (potentialTarget == centralEnemy || !potentialTarget.activeInHierarchy)
            {
                continue;
            }

            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            
            if (dSqrToTarget < closestDistanceSqr && dSqrToTarget <= detectionRadius * detectionRadius)
            {
                closestDistanceSqr = dSqrToTarget;
                closestEnemy = potentialTarget;
            }
        }

        return closestEnemy;
    }
}
