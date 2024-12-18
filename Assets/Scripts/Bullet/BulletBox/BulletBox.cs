using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBox : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bulletSprite;
    private int currentIndex = 0;
    [SerializeField] List<BulletSO> bulletsInBulletBox = new List<BulletSO>();
    [SerializeField] List<int> numEachBulletInBulletBox = new List<int>();


    public List<BulletSO> BulletsInBulletBox { get => bulletsInBulletBox; private set => bulletsInBulletBox = value; }
    public int CurrentIndex { get => currentIndex; set => currentIndex = value; }

    private void Awake() {
        bulletSprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start() {
        bulletSprite.sprite = bulletsInBulletBox[currentIndex].BulletSprite;
    }

    public void ChangeObjectInBox(){
        currentIndex++;
        if(currentIndex >= bulletsInBulletBox.Count) currentIndex = 0;
        ChangeSripteObjectInBox();
        Obsever.PostEvent(EventID.BulletBox_OnChangeBullet, new KeyValuePair<string, object>("sprite", bulletsInBulletBox[currentIndex].BulletSprite));
        Obsever.PostEvent(EventID.BulletBox_OnChangeBullet, new KeyValuePair<string, object>("text", numEachBulletInBulletBox[currentIndex]));
    }

    public GameObject GetObjectInBox(){
        return bulletsInBulletBox[currentIndex].BulletObject;
    }

    public void AddObjectToBox(object objectAdded){
        bool haved = false;
        BulletSO bulletSO = (BulletSO)objectAdded;
        for(int i = 0; i < bulletsInBulletBox.Count; i++){
            if(string.Compare(bulletSO.BulletName, bulletsInBulletBox[i].BulletName) == 0){
                numEachBulletInBulletBox[i]++;
                haved = true;
            }
        }
        if(!haved){
            bulletsInBulletBox.Add((BulletSO)bulletSO);
            numEachBulletInBulletBox.Add(1);
        }

        if(bulletsInBulletBox[currentIndex] == (BulletSO)objectAdded){
            Obsever.PostEvent(EventID.BulletBox_OnAddOrMinusBullet, new KeyValuePair<string, object>("text", numEachBulletInBulletBox[currentIndex]));
        }
    }

    public void MinusObjectInBox(){
        numEachBulletInBulletBox[currentIndex]--;
        // kiểm tra thử nó đã hết chưa, nếu hết rồi thì Reset về lại Bullet đầu tiên;
        if(numEachBulletInBulletBox[currentIndex] == 0){
            numEachBulletInBulletBox.RemoveAt(currentIndex);
            bulletsInBulletBox.RemoveAt(currentIndex);
            
            ChangeObjectInBox();
        }
        else{
            Obsever.PostEvent(EventID.BulletBox_OnAddOrMinusBullet, new KeyValuePair<string, object>("text", numEachBulletInBulletBox[currentIndex]));
        }
    }

    public void ChangeSripteObjectInBox(){
        bulletSprite.sprite = bulletsInBulletBox[currentIndex].BulletSprite;
    }
}
