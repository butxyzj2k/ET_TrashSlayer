using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : ObjectAttack
{
    private bool canChangeBulletInBulletBox = true;
    BulletBox bulletBox;

    private void Start() {
        SetCurrentObjectSkill("PlayerAttack");
        bulletBox = GetComponentInChildren<BulletBox>();
        currentObjectSkill.skillWeaponPrefab = bulletBox.GetObjectInBox();
    }

    private void OnEnable() {
        //Reset lại trạng thái sẵn sàng tấn công của Player mỗi khi spawn
        isAttack = false;
        canAttack = true;
    }
    
    public override void PerformAttack()
    {
        if(currentObjectSkill.attackPatternSO == null) return;
        Vector3 mousePoint = InputManager.instance.GetMousePoint();
        barrel.rotation = currentObjectSkill.attackPatternSO.GetDir(barrel, mousePoint); //Xoay hướng tấn công của Player
        //Thực hiện tấn công khi người chơi Click chuột và canAttack 
        if(InputManager.instance.GetIsPressButtonAttack() && canAttack){
            currentObjectSkill.skillWeaponPrefab = bulletBox.GetObjectInBox();
            if(currentObjectSkill.skillWeaponPrefab == null) return;

            canAttack = false;

            PlayObjectAttackSFX();

            currentObjectSkill.attackPatternSO.PerformAttack(barrel, mousePoint, PoolObject.GetPoolObject(currentObjectSkill.skillWeaponPrefab), () => isAttack = false);
            //Sau khi tấn công, trừ đi 1 đạn trong bulletBox
            bulletBox.MinusObjectInBox();
            DelayAttack();   
        }    
    }

    public void ChangeBullet(){
        if(Input.GetKey(KeyCode.Tab) && canChangeBulletInBulletBox){
            canChangeBulletInBulletBox = false;
            bulletBox.ChangeObjectInBox();
            StartCoroutine(AllowBulletChangeAfterHold());
        }
    }
    //Chuyển đạn khác sau 0,25s người chơi vẫn giữ tab
    IEnumerator AllowBulletChangeAfterHold(){
        yield return new WaitForSeconds(0.25f);
        canChangeBulletInBulletBox = true;
    }

    public override void PlayObjectAttackSFX()
    {
        if(currentObjectSkill.skillSFXPrefab != null){
            PoolObject.GetPoolObject(currentObjectSkill.skillSFXPrefab).GetObjectInPool(new Vector3(0, 0, 0), Quaternion.identity, () => {
                Dictionary<string, object> data = new()
                {
                    { "isLoop", currentObjectSkill.sfxLoop}
                };
                return data;
            });
        }
    }
}
