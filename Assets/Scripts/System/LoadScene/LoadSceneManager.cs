using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager instance;

    Action<KeyValuePair<string, object>> LoadGameSceneDelegate;
    Action<KeyValuePair<string, object>> LoadHomeSceneDelegate;
    Action<KeyValuePair<string, object>> LoadTutorialSceneDelegate;
    Action<KeyValuePair<string, object>> LoadFirstGameSceneDelegate;
    Action<KeyValuePair<string, object>> QuitGameDelegate;

    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }

    private void Start() {
        LoadGameSceneDelegate = (param) => {
            if(param.Key != null) return;
            StartCoroutine(LoadGameScene());
        };
        
        LoadHomeSceneDelegate = (param) => {
            StartCoroutine(LoadHomeScene());
        };

        LoadTutorialSceneDelegate = (param) => {
            StartCoroutine(LoadTutorialScene());
        };

        LoadFirstGameSceneDelegate = (param) => {
            StartCoroutine(LoadFirstGameScene());
        };

        QuitGameDelegate = (param) => {
            Application.Quit();
        };

        Obsever.AddListener(EventID.BOSS_DEATH, LoadGameSceneDelegate);
        Obsever.AddListener(EventID.UI_HomeButton_OnClick, LoadHomeSceneDelegate);
        Obsever.AddListener(EventID.UI_TutorialButton_OnClick, LoadTutorialSceneDelegate);
        Obsever.AddListener(EventID.UI_PlayButton_OnClick, LoadFirstGameSceneDelegate);
        Obsever.AddListener(EventID.UI_QuitButton_OnClick, QuitGameDelegate);
    }

    private void OnDestroy() {
        Obsever.RemoveListener(EventID.BOSS_DEATH, LoadGameSceneDelegate);
        Obsever.RemoveListener(EventID.UI_HomeButton_OnClick, LoadHomeSceneDelegate);
        Obsever.RemoveListener(EventID.UI_TutorialButton_OnClick, LoadTutorialSceneDelegate);
        Obsever.RemoveListener(EventID.UI_PlayButton_OnClick, LoadFirstGameSceneDelegate);
        Obsever.RemoveListener(EventID.UI_QuitButton_OnClick, QuitGameDelegate);

        instance = null;
    }

    IEnumerator LoadGameScene(){
        if(SceneManager.GetActiveScene().buildIndex <= 1) yield break;
        Obsever.PostEvent(EventID.SCENE_EndScene, new KeyValuePair<string, object>("timeWaitingToLoadNextScene", 7));
        yield return new WaitForSeconds(7);
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadFirstGameScene(){
        yield return null;
        StartCoroutine(LoadSceneAsync(2));
    }

    IEnumerator LoadHomeScene(){
        yield return null;
        StartCoroutine(LoadSceneAsync(0));
    }

    IEnumerator LoadTutorialScene(){
        yield return null;
        StartCoroutine(LoadSceneAsync(1));
    }

    IEnumerator LoadSceneAsync(int sceneIdToLoad){
        if(sceneIdToLoad + 1 > SceneManager.sceneCountInBuildSettings){
            Obsever.PostEvent(EventID.SCENE_EndGame, new KeyValuePair<string, object>(null, null));
        }
        else{
            Obsever.PostEvent(EventID.SCENE_WaitingLoadingScreen_OnComplete, new KeyValuePair<string, object>(null, null));
            yield return new WaitForSecondsRealtime(3);
            AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneIdToLoad);
            while(!loadSceneOperation.isDone){
                yield return null;
            }
        }
    }
}