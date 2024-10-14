using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff/Speed")]
public class BoostMovementSkillPatternSO : SkillPatternSO
{
    public float BoostSpeedRate;
    public override void PerformSkill()
    {
        // playerMovement.Speed = playerMovement.Speed + playerMovement.Speed * 0.1f;
        PlayerController.instance.playerMovement.CurrentSpeed = PlayerController.instance.playerMovement.InitialSpawnSpeed + PlayerController.instance.playerMovement.InitialSpawnSpeed * BoostSpeedRate;
        PlayerController.instance.playerMovement.BaseSpeed = PlayerController.instance.playerMovement.CurrentSpeed;
        PlayerController.instance.playerMovement.GetComponent<Animator>().SetFloat("speed", BoostSpeedRate + 1);
        Debug.Log("Boost " + BoostSpeedRate*100 + "Movement");
    }
}
