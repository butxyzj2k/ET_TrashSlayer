using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class ObjectPathfinding : MonoBehaviour
{
    int currentWaitpoint = 0;

    Vector3 starPoint;
    Vector3 targetPoint;
    Vector3 offsetPoint;

    Path thePath;
    Seeker seeker;

    public int CurrentWaitpoint { get => currentWaitpoint; set => currentWaitpoint = value; }
    public Path ThePath { get => thePath; set => thePath = value; }

    public void SetUpPathFinding(){
        seeker = GetComponent<Seeker>();
        seeker.traversableTags |= 1 << 0;
        seeker.traversableTags &= ~(1 << 1);
        CreateOffsetPoint();
        
        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
    }

    public void SetStartPoint(Vector3 _starPoint){
        starPoint = _starPoint;
    }
    
    public void SetTargetPoint(Vector3 endPoint){        
        targetPoint = endPoint + offsetPoint;
    }

    public void CreateOffsetPoint(){
        float xOffset = UnityEngine.Random.RandomRange(-0.75f, 0.5f);
        float yOffset = UnityEngine.Random.RandomRange(-0.2f, 0.2f);
        
        yOffset = yOffset > 0 ? yOffset + 0.5f : yOffset - 0.5f;
        offsetPoint = new Vector3(xOffset, yOffset, 0);
    }
    

    void UpdatePath(){
        if(seeker.IsDone()){
            seeker.StartPath(starPoint ,targetPoint, OnPathComplete);
            currentWaitpoint = 0;
        }
    }

    void OnPathComplete(Path p){
        if(!p.error){
            thePath = p;
            currentWaitpoint = 0;
        }
    }

    
}
