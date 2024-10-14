using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public static GameSession instance;
    public float timeGamePlay = 0;

    Action<KeyValuePair<string, object>> DeleteGameSessionDelegate;
    void Awake(){
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if(numGameSessions > 1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
        }

        if(instance == null){
            instance = this;
        }

        timeGamePlay = 0;
    }

    public void Start(){
        DeleteGameSessionDelegate = (param) => {
            Destroy(gameObject);
        };

        Obsever.AddListener(EventID.SCENE_HomeScene_OnStart, DeleteGameSessionDelegate);
    }

    private void OnDestroy() {
        instance = null;
        Obsever.RemoveListener(EventID.SCENE_HomeScene_OnStart, DeleteGameSessionDelegate);
    }

    private void Update() {
        timeGamePlay += Time.deltaTime;
    }

}
