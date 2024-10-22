using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyAttack : ObjectAttack
{
    protected Transform targetTransform;
    protected Animator anim;
    [SerializeField] protected float sightAttackDist;
    public Transform TargetTransform { get => targetTransform; set => targetTransform = value; }
    public Vector3 TargetAttackPosition;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void OnEnable() {
        //Reset lại trạng thái sẵn sàng tấn công của Enemy mỗi khi spawn
        isAttack = false;
        canAttack = true;
    }

    private void Start() {
        //Thiết lập currentskill ban đầu
        currentObjectSkill = skillsList[0];
    }
    //Kiểm tra xem Player có nằm trên tầm nhìn mà Enemy có thể tấn công không, nếu có return true
    public virtual bool CheckSightAttack(){
        int layerMask = LayerMask.GetMask(LayerMask.LayerToName(targetTransform.gameObject.layer));
        RaycastHit2D rayAttack = Physics2D.Raycast(barrel.position, 
                                                    targetTransform.position - barrel.position,
                                                    sightAttackDist, layerMask);
        Debug.DrawRay(barrel.position, 
                      (targetTransform.position - barrel.position).normalized*sightAttackDist,
                      Color.red);
        if(rayAttack.collider != null){
            TargetAttackPosition = rayAttack.collider.transform.position;
            return true;
        }
        return false;
    }

    public override void PerformAttack()
    {
        //Nếu có thể tấn công, thực hiện tấn công
        if(canAttack == true){
            SceneGameManager.instance.StartCoroutine(EnemyAttackPerform());
        }
    }

    public virtual IEnumerator EnemyAttackPerform(){
            canAttack = false;
            isAttack = true;

            PlayObjectAttackSFX();
            
            anim.Play(currentObjectSkill.skillAnimationClip);

            if(currentObjectSkill.skillWeaponPrefab == null){
                isAttack = false;
                canAttack = true;
                yield break;
            }
            yield return new WaitForSeconds(currentObjectSkill.timeToStartAttack);
            currentObjectSkill.attackPatternSO.PerformAttack(barrel, TargetAttackPosition, PoolObject.GetPoolObject(currentObjectSkill.skillWeaponPrefab), () => isAttack = false); 
            DelayAttack();
    }

    public override IEnumerator ResetAttack(float timeToResetAttack){
        //Kiểm tra nếu quái vật đã ngừng trạng thái tấn công được trả về từ attackPatternSO thì mới bắt đầu ResetAttack
        while(true){
            if(!isAttack){
                anim.Play("Move");
                break;
            }
            yield return null;
        }
        SceneGameManager.instance.StartCoroutine(base.ResetAttack(timeToResetAttack));
    }   

    public override void PlayObjectAttackSFX()
    {
        if(currentObjectSkill.skillSFXPrefab != null){
            PoolObject.GetPoolObject(currentObjectSkill.skillSFXPrefab).GetObjectInPool(new Vector3(0, 0, 0), quaternion.identity, () => {
                Dictionary<string, object> data = new()
                {
                    { "isStopSFXLoop", !isAttack || !gameObject.activeInHierarchy || !PlayerController.instance.playerHitting.IsDeath},
                    { "isLoop", currentObjectSkill.sfxLoop}
                };
                return data;
            });
        }
    } 
}
