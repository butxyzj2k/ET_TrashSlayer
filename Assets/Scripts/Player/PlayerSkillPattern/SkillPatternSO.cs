using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillPatternSO : ScriptableObject
{
    [SerializeField] protected Sprite skillImage;
    [SerializeField] protected string skillDescription;
    // protected PlayerMovement playerMovement;
    // protected PlayerAttack playerAttack;
    // protected PlayerHealth playerHealth;

    public Sprite SkillImage { get => skillImage; set => skillImage = value; }
    public string SkillDescription { get => skillDescription; set => skillDescription = value; }

    // public void Setting(GameObject player){
    //     if(playerAttack != null && playerHealth != null && playerMovement != null) return;
    //     playerMovement = player.GetComponent<PlayerMovement>();
    //     playerAttack = player.GetComponent<PlayerAttack>();
    //     playerHealth = player.GetComponent<PlayerHealth>();
    // }
    public abstract void PerformSkill();
}
