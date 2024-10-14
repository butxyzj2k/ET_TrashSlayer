using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GetSideCollison
{
    public enum Side{
        none,
        left, 
        right,
        up,
        down
    }

    public static Side GetSide(Vector3 centralObjectPos, GameObject gameObjectNeedToCheck){
        Vector3 posToCheck = gameObjectNeedToCheck.transform.position - gameObjectNeedToCheck.transform.up.normalized;

        Vector3 relativeVector = new(centralObjectPos.x - posToCheck.x, centralObjectPos.y - posToCheck.y, 0);

        //size of sender
        float xBoundsOfGameObjectNeedToCheck;
        if(gameObjectNeedToCheck.GetComponent<Collider2D>()) xBoundsOfGameObjectNeedToCheck = gameObjectNeedToCheck.GetComponent<Collider2D>().bounds.extents.x;
        else xBoundsOfGameObjectNeedToCheck = gameObjectNeedToCheck.GetComponentInChildren<Collider2D>().bounds.extents.x;


        if(Mathf.Abs(relativeVector.x) < xBoundsOfGameObjectNeedToCheck){
            if(relativeVector.y > 0){
                return Side.down;
            }
            else{
                return Side.up;
            }
        }
        else{
            if(relativeVector.x > 0){
                return Side.left;
            }
            else{
                return Side.right;
            }
        }
    }
}
