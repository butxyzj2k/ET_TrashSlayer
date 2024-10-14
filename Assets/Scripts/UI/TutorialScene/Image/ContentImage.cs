using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentImage : MonoBehaviour
{
    Image contentImage;
    [SerializeField] List<Sprite> tutoContentSprites;
    int currentIndex = 0;
    Action<KeyValuePair<string, object>> ChangeSpriteInContentImageDelegate;

    private void Start() {
        contentImage = GetComponent<Image>();
        ChangeSpriteInContentImageDelegate = (param) => ChangeSpriteInContentImage((bool)param.Value);

        Obsever.AddListener(EventID.UI_ChangeLeftContentUIButton_OnClick, ChangeSpriteInContentImageDelegate);
        Obsever.AddListener(EventID.UI_ChangeRightContentUIButton_OnClick, ChangeSpriteInContentImageDelegate);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.UI_ChangeLeftContentUIButton_OnClick, ChangeSpriteInContentImageDelegate);
        Obsever.RemoveListener(EventID.UI_ChangeRightContentUIButton_OnClick, ChangeSpriteInContentImageDelegate);
    }

    public void ChangeSpriteInContentImage(bool isMinus){
        if(isMinus){
            if(currentIndex - 1 >= 0){
                currentIndex--;
            }
            else{
                currentIndex = tutoContentSprites.Count - 1;
            }
        }
        else{
            if(currentIndex + 1 < tutoContentSprites.Count){
                currentIndex++;
            }
            else{
                currentIndex = 0;
            }
        }
        contentImage.sprite = tutoContentSprites[currentIndex];
    }
}