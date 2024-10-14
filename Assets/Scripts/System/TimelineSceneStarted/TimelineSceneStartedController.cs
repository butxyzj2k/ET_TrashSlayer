using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineSceneStartedController : MonoBehaviour
{
    PlayableDirector sceneIntroTimeline;
    Action<KeyValuePair<string, object>> PlaySceneIntroTimelineDelegate;
    Action<KeyValuePair<string, object>> UnActiveSceneIntroTimelineDelegate;

    private void Awake() {
        sceneIntroTimeline = GetComponentInChildren<PlayableDirector>();
    }

    private void Start() {
        PlaySceneIntroTimelineDelegate = (param) => {
            sceneIntroTimeline.Play();
            StartCoroutine(SceneIntroTimelineControl());
        };

        UnActiveSceneIntroTimelineDelegate = (param) => {
            gameObject.SetActive(false);
        };

        Obsever.AddListener(EventID.SCENE_OnStart, PlaySceneIntroTimelineDelegate);
        Obsever.AddListener(EventID.SCENE_OnCompleteIntro, UnActiveSceneIntroTimelineDelegate);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.SCENE_OnStart, PlaySceneIntroTimelineDelegate);
        Obsever.RemoveListener(EventID.SCENE_OnCompleteIntro, UnActiveSceneIntroTimelineDelegate);
    }

    IEnumerator SceneIntroTimelineControl(){
        float time = 0;
        while(time < (float)sceneIntroTimeline.duration){
            time += Time.deltaTime;
            if(InputManager.instance.GetIsSkipTimeline()){
                break;
            }
            yield return null;
        }
        Obsever.PostEvent(EventID.SCENE_OnCompleteIntro, new KeyValuePair<string, object>(null, null));
        gameObject.SetActive(false);
    }
}
