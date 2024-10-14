using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBanner : MonoBehaviour
{

    Action<KeyValuePair<string, object>> EnemyBossActiveBannerDelegate;
    Action<KeyValuePair<string, object>> PlayBackBackGroundMusicDeletgate;

    void Start()
    {
        EnemyBossActiveBannerDelegate = (param) => {
            if(param.Key != null) return;
            gameObject.SetActive(true);
            StartCoroutine(PlayBannerAnimation());
        };

        PlayBackBackGroundMusicDeletgate = (param) => {
            if(param.Key != null) return;
            GameObject.FindGameObjectWithTag("BG").GetComponent<AudioSource>().Play();
        };

        Obsever.AddListener(EventID.BOSS_SPAWN, EnemyBossActiveBannerDelegate);
        Obsever.AddListener(EventID.BOSS_SPAWN, PlayBackBackGroundMusicDeletgate);

        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.BOSS_SPAWN, EnemyBossActiveBannerDelegate);
        Obsever.RemoveListener(EventID.BOSS_SPAWN, PlayBackBackGroundMusicDeletgate);
    }

    IEnumerator PlayBannerAnimation(){
        yield return new WaitForSecondsRealtime(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length * 2);
        Obsever.PostEvent(EventID.UI_BossBanner_OnComplete, new KeyValuePair<string, object>(null, null));
        gameObject.SetActive(false);
    }

}
