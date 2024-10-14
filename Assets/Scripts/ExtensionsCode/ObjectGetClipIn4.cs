using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGetClipIn4 : MonoBehaviour
{
    [Header("Name")]
    public string attackName;
    public string meeleeAttackName;
    public string hurtName;
    public string deathName;
    public string idleName;
    [Header("Time")]
    public float attackTime;
    public float meeleeAttackTime;
    public float hurtTime;
    public float deathTime;
    public float idleTime;
 
    private Animator anim;
 
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        if(anim == null)
        {
            Debug.Log("Error: Did not find anim!");
        } else
        {
            //Debug.Log("Got anim");
        }
        UpdateAnimClipTimes();
    }
    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            if(string.Compare(clip.name, attackName) == 0){
                attackTime = clip.length;
            }
            else if(string.Compare(clip.name, meeleeAttackName) == 0){
                meeleeAttackTime = clip.length;
            }
            else if(string.Compare(clip.name, hurtName) == 0){
                hurtTime = clip.length;
            }
            else if(string.Compare(clip.name, deathName) == 0){
                deathTime = clip.length;
            }
            else if(string.Compare(clip.name, idleName) == 0){
                idleTime = clip.length;
            }
        }
    }
}
