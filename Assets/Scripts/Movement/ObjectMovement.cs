using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectMovement : MonoBehaviour
{
    [Header("Speed--")]
    [SerializeField] protected float currentSpeed = 5; //Tốc độ hiện tại được sử dụng để di chuyển, thực hiện các tính toán hoặc thay đổi tạm thời
    protected float baseSpeed; //Giá trị tốc độ gốc tại một thời điểm cụ thể
    protected float initialSpawnSpeed; //Tốc độ khi đối tượng mới được spawn ra

    [Header("Cpn--")]
    [SerializeField] protected Rigidbody2D rb2d;

    [Header("Other--")]
    [SerializeField] protected MovementPatternSO movementPatternSO;
    [SerializeField] protected bool canMove = true;
    // [SerializeField] protected bool canChangeScale = false;
    protected bool firstTimeMove = true;
    protected Vector2 lastVelo = Vector2.zero;

    public float CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }
    public float BaseSpeed { get => baseSpeed; set => baseSpeed = value; }
    public float InitialSpawnSpeed { get => initialSpawnSpeed; set => initialSpawnSpeed = value; }
    public bool CanMove { get => canMove; set => canMove = value; }

    public abstract void PerformMovement();
    
    public abstract void ObjectMovementAnim();

    public virtual void SetObjectIdelding(){
        rb2d.velocity = Vector2.zero;
    }

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
