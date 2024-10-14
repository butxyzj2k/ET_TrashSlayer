using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingBagHitting : ObstacleObjectHitting
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<MeeleeBulletHitting>() != null){
            Vector3 scale = gameObject.transform.localScale;
            if(other.gameObject.transform.position.x > gameObject.transform.position.x){
                scale.x = -1;
            }
            else{
                scale.x = 1;
            }
            gameObject.transform.localScale = scale;
            gameObject.GetComponentInParent<Animator>().SetTrigger("Kicked");
        }
    }
}
