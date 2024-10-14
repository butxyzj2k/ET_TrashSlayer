using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackgroundLoop : MonoBehaviour
{
    [SerializeField] Vector3 spawnPoint;
    [SerializeField] float distanceToReset;
    [SerializeField] float speed;

    private void Update() {
        MenuBackgroundCycleLoop();
    }

    public void MenuBackgroundCycleLoop(){
        transform.position = transform.position + Vector3.left * Time.deltaTime * speed; 

        if(Vector2.Distance(transform.position, spawnPoint) > 4 * distanceToReset){
            transform.position = spawnPoint;
        }
    }
}
