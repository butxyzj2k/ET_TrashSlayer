using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHealth : Health, IHitttable
{
    protected float currentTime;
    [SerializeField] protected float timeExist;

    protected WeaponHittingVFX weaponHittingVFX;

    [SerializeField] protected string objectOwner;

    // [SerializeField] protected HittingEffectsSO hittingEffectsSO;
    [SerializeField] protected List<HittingEffectsSO> hittingObjectEffectSOs;
    [SerializeField] protected List<HittingEffectsSO> hittingOtherEffectSOs;
    protected GameObject currentOwner;

    public string ObjectOwner { get => objectOwner; set => objectOwner = value; }
    public GameObject CurrentOwner { get => currentOwner; set => currentOwner = value; }
    public float TimeExist { get => timeExist; set => timeExist = value; }

    private void Start() {
        SetSFXPoolObject();
        weaponHittingVFX = GetComponent<WeaponHittingVFX>();
        InitalizeHealth(maxHealth);
    }

    private void OnEnable() {
        PlaySpawnSFX();
        currentTime = 0;
        InitalizeHealth(maxHealth);
    }

    private void Update() {
        DestroyBulletAfterTimeExist();
    }

    public void TakeHit(float damage, GameObject receiver)
    {
        if(receiver.GetComponent<AreaEffectHealth>() || receiver.GetComponent<OrbHitting>()) return;
        WeaponTakeHit(damage, receiver);
    }

    public virtual void WeaponTakeHit(float damage, GameObject receiver){
        Debug.Log("Haven't changed");
    }

    public void HittingEffectsPerform(GameObject sender, GameObject _receiver, HittingEffectsSO hittingEffectsSO)
    {
        // GameObject stateVFXObject = hittingEffectsSO.HittingEffects(sender, _receiver);
        // InvokeExtensionCode.Invoke(SceneGameManager.sceneGameManager, () => hittingEffectsSO.ResetHittingEffects(sender, _receiver, stateVFXObject), hittingEffectsSO.TimeEffect);
    }

    public void DestroyBulletAfterTimeExist(){
        currentTime += Time.deltaTime;
        if(currentTime > timeExist){
            currentTime = 0;
            gameObject.SetActive(false);
        }
    }
}
