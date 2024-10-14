using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class SceneGameManager : MonoBehaviour
{
    public static SceneGameManager instance;

    [SerializeField] List<GameObject> gameObjectsInScene = new();
    [SerializeField] List<GameObject> obstacleInScene = new();
    [SerializeField] Transform PlayerSpawnPos;

    Action<KeyValuePair<string, object>> ActiveAllObjectInSceneDelegate;
    Action<KeyValuePair<string, object>> UnActiveAllObjectInSceneDelegate;
    Action<KeyValuePair<string, object>> UnActiveAllObstacleObjectInSceneDelegate;
    Action<KeyValuePair<string, object>> TurnOnBackgroundMusicDelegate;

    [SerializeField] bool isGamePlayScene = true;
    
    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }

    private void Start() {
        if(isGamePlayScene)
        {
            foreach(Transform gameSessionChild in GameSession.instance.transform){
                gameObjectsInScene.Add(gameSessionChild.gameObject);
            }

            ActiveAllObjectInSceneDelegate = (param) => {
                ActiveAllObjectInScene();
            };

            UnActiveAllObjectInSceneDelegate = (param) => {
                UnActiveAllObjectInScene();
            };

            UnActiveAllObstacleObjectInSceneDelegate = (param) => {
                if(param.Key != null) return;
                UnActiveAllObstacleObjectInScene();
            };

            TurnOnBackgroundMusicDelegate = (param) => {
                GameObject.FindGameObjectWithTag("BG").GetComponent<AudioSource>().Play();
            };

            Obsever.AddListener(EventID.SCENE_OnStart, UnActiveAllObjectInSceneDelegate);
            Obsever.AddListener(EventID.SCENE_OnCompleteIntro, ActiveAllObjectInSceneDelegate);
            Obsever.AddListener(EventID.SCENE_OnCompleteIntro, TurnOnBackgroundMusicDelegate);
            Obsever.AddListener(EventID.BOSS_SPAWN, UnActiveAllObstacleObjectInSceneDelegate);
            PlayerController.instance.transform.position = PlayerSpawnPos.position;
        }

        InvokeExtensionCode.Invoke(instance, () => {
            if(SceneManager.GetActiveScene().buildIndex == 0) Obsever.PostEvent(EventID.SCENE_HomeScene_OnStart, new KeyValuePair<string, object>(null, null));
            else Obsever.PostEvent(EventID.SCENE_OnStart, new KeyValuePair<string, object>(null, null));    
        }, Time.unscaledDeltaTime);
            
    }

    private void OnDestroy() {
        if(isGamePlayScene)
        {
            Obsever.RemoveListener(EventID.SCENE_OnStart, UnActiveAllObjectInSceneDelegate);
            Obsever.RemoveListener(EventID.SCENE_OnCompleteIntro, ActiveAllObjectInSceneDelegate);
            Obsever.RemoveListener(EventID.SCENE_OnCompleteIntro, TurnOnBackgroundMusicDelegate);
            Obsever.RemoveListener(EventID.BOSS_SPAWN, UnActiveAllObstacleObjectInSceneDelegate);
        }

        instance = null;
    }

    void ActiveAllObjectInScene(){
        foreach(GameObject gameObject in gameObjectsInScene) {
            gameObject.SetActive(true);
        }
    }

    void UnActiveAllObjectInScene(){
        foreach(GameObject gameObject in gameObjectsInScene) {
            gameObject.SetActive(false);
        }
    }

    void UnActiveAllObstacleObjectInScene(){
        foreach(GameObject obstacleObject in obstacleInScene){
                if(obstacleObject.TryGetComponent<TilemapRenderer>(out var tilemapRenderer)){
                    tilemapRenderer.enabled = false;
                }
                if(obstacleObject.TryGetComponent<TilemapCollider2D>(out var tilemapCollider2D)){
                    tilemapCollider2D.enabled = false;
                }
            }
    }
}
