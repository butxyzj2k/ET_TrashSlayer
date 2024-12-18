using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamMovement : StaticObjectMovement
{
    [SerializeField] float maxLaserSize = 20f;

    [SerializeField] GameObject laserStart;
    [SerializeField] GameObject laserMiddle;
    [SerializeField] GameObject laserEnd;

    [SerializeField] float starLaserSize;
    [SerializeField] float middleLaserSize;
    [SerializeField] float endLaserSize;

    private float currentLaserSize;

    private void OnEnable() {
        canMove = false;
    }

    public void ChangeSizeOfLaserBeam(){
        int layerCheck = LayerMask.GetMask("Player", "Obstacle");
        Vector2 laserDir = transform.up;
        Debug.DrawRay(laserStart.transform.position, laserDir * maxLaserSize, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(laserStart.transform.position, laserDir, maxLaserSize, layerCheck);
        Debug.DrawRay(laserStart.transform.position, laserDir * maxLaserSize, Color.red);
        if(hit.collider != null){
            currentLaserSize = Vector2.Distance(hit.point, transform.position) - starLaserSize - endLaserSize;
        }
        else{
            currentLaserSize = maxLaserSize - starLaserSize - endLaserSize;
        }

        Vector3 laserScale = laserMiddle.transform.localScale;
        laserScale.x =  (currentLaserSize)/middleLaserSize;
        laserMiddle.transform.localScale = laserScale;

        Vector3 laserEndPos = laserEnd.transform.localPosition;
        laserEndPos.y = starLaserSize + currentLaserSize;
        laserEnd.transform.localPosition = laserEndPos;
    }

    public override void ObjectMovementAnim()
    {
        ChangeSizeOfLaserBeam();
    }
}
