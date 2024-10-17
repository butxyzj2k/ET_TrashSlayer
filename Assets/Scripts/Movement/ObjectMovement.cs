using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectMovement : MonoBehaviour
{
    [Header("Speed--")]
    [SerializeField] protected float rotateSpeed = 0.75f;
    [SerializeField] protected float currentSpeed = 5; //Tốc độ hiện tại được sử dụng để di chuyển, thực hiện các tính toán hoặc thay đổi tạm thời
    protected float baseSpeed; //Giá trị tốc độ gốc tại một thời điểm cụ thể
    protected float initialSpawnSpeed; //Tốc độ khi đối tượng mới được spawn ra

    [Header("Cpn--")]
    [SerializeField] protected Rigidbody2D rb2d;
    [SerializeField] protected Transform objectSprite;
    protected Animator anim;

    [Header("MoveToTarget")]
    protected Transform targetTransform;

    [Header("Other--")]
    [SerializeField] protected MovementTrajectoryPatternSO movementTrajectoryPatternSO;
    [SerializeField] protected bool canMove = true;
    [SerializeField] protected GameObject movingSFXPrefab;
    
    protected bool firstTimeMove = true;
    protected Vector2 lastVelo = Vector2.zero;

    public float CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }
    public float BaseSpeed { get => baseSpeed; set => baseSpeed = value; }
    public float InitialSpawnSpeed { get => initialSpawnSpeed; set => initialSpawnSpeed = value; }
    public bool CanMove { get => canMove; set => canMove = value; }
    public Transform TargetTransform { get => targetTransform; set => targetTransform = value; }

    public abstract void PerformMovement();

    public virtual void SetObjectIdelding(){
        rb2d.velocity = Vector2.zero;
    }

    public void ObjectPlayMovementSFX(){
        if(movingSFXPrefab == null) return;
        //Nếu trước đó Object không di chuyển và bây giờ mới di chuyển thì thực hiện SFX
        if(lastVelo == Vector2.zero && rb2d.velocity != Vector2.zero){
            PoolObject.GetPoolObject(movingSFXPrefab).GetObjectInPool(new Vector3(0, 0, 0), Quaternion.identity, () => {
                Dictionary<string, object> data = new()
                {
                    { "isStopSFXLoop", rb2d.velocity == Vector2.zero || Time.timeScale == 0},
                    { "isLoop", true}
                };
                return data;
            });
        }
        lastVelo = rb2d.velocity;
    }
    
    #region MovementAnim

    public abstract void ObjectMovementAnim();

    public void RotateObjectInMovement(){
        objectSprite.Rotate(0, 0, rotateSpeed);
    }

    public virtual void ObjectMovementAnimationPerform(){
        if(anim == null) return;
        anim.SetFloat("Horizontal", rb2d.velocity.x);
        anim.SetFloat("Vertical", rb2d.velocity.y);
    }

    public void ObjectMovementChangeScale(){

        Vector3 objectScale = gameObject.transform.localScale;

        if(gameObject.GetComponent<Rigidbody2D>().velocity.x > 0){
            objectScale.x = 1;
        }
        else if(gameObject.GetComponent<Rigidbody2D>().velocity.x < 0){
            objectScale.x = -1;
        }

        gameObject.transform.localScale = objectScale;
    }

    #endregion

    private void Start() {
        baseSpeed = currentSpeed;
        initialSpawnSpeed = currentSpeed;
    }

    private void OnEnable() {
        canMove = true;
        firstTimeMove = true;
        lastVelo = Vector2.zero;
    }
}
