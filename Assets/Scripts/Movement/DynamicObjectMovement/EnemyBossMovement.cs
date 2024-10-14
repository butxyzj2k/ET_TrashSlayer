using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossMovement : EnemyMovement
{
    private bool isMovingToPlayer = false;

    [SerializeField] private float radiusMoveArround = 1.5f;
    GameObject tempTransform;

    public bool IsMovingToPlayer { get => isMovingToPlayer; set => isMovingToPlayer = value; }


    private void Start() {
        tempTransform = new()
        {
            name = "BossTempTransform"
        };
        baseSpeed = currentSpeed;
        initialSpawnSpeed = currentSpeed;

        objectPathfinding.SetUpPathFinding();  
    }

    public void SetTargetTransform(){
        if(isMovingToPlayer){
            targetTransform = PlayerController.instance.transform;
        }
        else{
            tempTransform.transform.SetPositionAndRotation(GetRandomPointArroundPlayer(), Quaternion.identity);
            targetTransform = tempTransform.transform;
        }
    }

    public override void EnemyMovementPerform()
    {  
        float distance = Vector2.Distance(targetTransform.position, rb2d.position);
        if(distance < 0.75f){
            StartCoroutine(ResetMoveArround());
        }
        SetTargetTransform();
        base.EnemyMovementPerform();
    }
    
    Vector2 GetRandomPointArroundPlayer(){
        Vector2 random = new Vector2(UnityEngine.Random.Range(-radiusMoveArround, radiusMoveArround), UnityEngine.Random.Range(-radiusMoveArround, radiusMoveArround));
        return random + (Vector2)PlayerController.instance.transform.position;
    }

    IEnumerator ResetMoveArround(){
        canMove = false;
        yield return new WaitForSeconds(1.2f);
        canMove = true;
    }
}
