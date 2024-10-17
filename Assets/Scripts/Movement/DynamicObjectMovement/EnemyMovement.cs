using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : ObjectMovement
{
    [SerializeField] protected float radiusToIdel;
    protected ObjectPathfinding objectPathfinding;

    private void Awake() {
        objectPathfinding = GetComponent<ObjectPathfinding>();
        anim = GetComponent<Animator>();
    }

    private void Start() {
        baseSpeed = currentSpeed;
        initialSpawnSpeed = currentSpeed;
        objectPathfinding.SetUpPathFinding();  
    }

    public Vector3 GetDirToTargetTransform()
    {
        objectPathfinding.SetStartPoint(rb2d.position);
        objectPathfinding.SetTargetPoint(targetTransform.position);
        
        if(objectPathfinding.ThePath == null){
            return Vector3.zero;
        }

        if(objectPathfinding.CurrentWaitpoint >= objectPathfinding.ThePath.vectorPath.Count){
            return Vector3.zero;
        }

        Vector2 dir = ((Vector2)objectPathfinding.ThePath.vectorPath[objectPathfinding.CurrentWaitpoint] - rb2d.position + EnemySeparationDirection(FindObjectsOfType<EnemyMovement>()) ).normalized;
        
        return dir;
    }

    public override void PerformMovement()
    {
        if(!canMove || (Vector3.Distance(targetTransform.position, gameObject.transform.position) <= radiusToIdel)) {
            SetObjectIdelding();
        }
        else EnemyMovementPerform();
        ObjectMovementAnim();
        ObjectPlayMovementSFX();
    }

    public virtual void EnemyMovementPerform(){
        Vector3 dir = GetDirToTargetTransform();
    
        if(canMove){
            movementTrajectoryPatternSO.PerformMovement(rb2d, currentSpeed ,dir, ref firstTimeMove);
        }

        if(objectPathfinding.ThePath == null) return;
        if(objectPathfinding.CurrentWaitpoint >= objectPathfinding.ThePath.vectorPath.Count) return;
        
        float distance = Vector2.Distance(rb2d.position, objectPathfinding.ThePath.vectorPath[objectPathfinding.CurrentWaitpoint]);

        if(distance < 0.2){
            objectPathfinding.CurrentWaitpoint++;
        }
    }

    public override void ObjectMovementAnim()
    {
        ObjectMovementAnimationPerform();
    }

    public override void ObjectMovementAnimationPerform(){
        base.ObjectMovementAnimationPerform();

        if(HasParameter(anim, "LastVertical")){
            if(rb2d.velocity != Vector2.zero){
                anim.SetFloat("LastVertical", rb2d.velocity.x/Mathf.Abs(rb2d.velocity.x));
            }
        }
    }

    public bool HasParameter(Animator anim, string parameterName){
        foreach (AnimatorControllerParameter param in anim.parameters)
        {
            if (param.name == parameterName)
            {
                return true;
            }
        }
        return false;
    }

    public Vector2 EnemySeparationDirection(EnemyMovement[] enemyMovements){
        Vector2 dir = Vector3.zero;

        foreach(EnemyMovement enemyMovement in enemyMovements){
            float ratio = 1 - Mathf.Clamp01((enemyMovement.transform.position - transform.position).magnitude / 1f); //Những Enemy xa hơn 1 sẽ không bị tác dụng lực đẩy
            dir -= ratio * (Vector2)(enemyMovement.transform.position - transform.position);
        }

        return dir;
    }
}
