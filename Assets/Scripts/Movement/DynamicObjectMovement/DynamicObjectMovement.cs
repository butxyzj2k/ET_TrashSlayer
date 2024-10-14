using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjectMovement : ObjectMovement
{
    protected Animator anim;
    [SerializeField] protected GameObject movingSFXPrefab;

    public override void PerformMovement()
    {
        Debug.Log("Not override Perform Movement");
    }

    public override void ObjectMovementAnim(){
        if(anim == null) return;
        anim.SetFloat("Horizontal", rb2d.velocity.x);
        anim.SetFloat("Vertical", rb2d.velocity.y);
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
}