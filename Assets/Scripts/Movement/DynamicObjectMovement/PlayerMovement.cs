using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : ObjectMovement
{

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start() {
        GetComponent<Animator>().SetFloat("speed", 1);
        baseSpeed = currentSpeed;
        initialSpawnSpeed = currentSpeed;
    }
    
    public override void PerformMovement()
    {
        if(!canMove){
            SetObjectIdelding();
        } 
        else{
            Vector2 direction = InputManager.instance.GetInputMovement();
            rb2d.velocity = currentSpeed * Time.fixedDeltaTime * direction;
        }
    }

    public override void ObjectMovementAnim()
    {
        ObjectMovementAnimationPerform();
    }

    public override void ObjectMovementAnimationPerform(){
        base.ObjectMovementAnimationPerform();

        if(rb2d.velocity != Vector2.zero){
            anim.SetFloat("LastHorizontal", rb2d.velocity.x);
            anim.SetFloat("LastVertical", rb2d.velocity.y);
        }
    }
}
