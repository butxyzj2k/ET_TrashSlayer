using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour
{
    public static int numScenePlayable = 4;
    public static LoadSceneController loadSceneController;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] float timeDelayLoadScene = 7;
    [SerializeField] GameObject LoadScreenUIObject;
    [SerializeField] BoostTimeEffectInHittingEffectSkillPatternSO boostTimeEffectInHittingEffectSkillPatternSO;
    bool isLoadScene = false;

    private void Awake() {
        if(SceneManager.GetActiveScene().buildIndex == 0){
            if(FindObjectOfType<GameSession>() != null){
                Destroy(FindObjectOfType<GameSession>().gameObject);
            }
            Time.timeScale = 1;
            // boostTimeEffectInHittingEffectSkillPatternSO.ResetHittingEffects();
            // SceneGameManager.OnLoadNewScene.RemoveAllListeners();
        }
        if(loadSceneController == null){
            loadSceneController = this;
        }
    }

    private void OnEnable() {
        isLoadScene = false;
    }

    private void Update() {
        if(LoadScreenUIObject.activeInHierarchy && !isLoadScene){
            LoadScreenUIObject.SetActive(false);
        }
    }

    public void LoadNextScene(int sceneIndex){
        StartCoroutine(LoadNextSceneCourotine(sceneIndex));
    }

    public void ReturnHomeScene(){
        StartCoroutine(LoadSceneAsync(0));
    }

    IEnumerator LoadNextSceneCourotine(int sceneIndex){
        float time = 0;
        textMeshProUGUI.gameObject.SetActive(true);
        while(time <= timeDelayLoadScene){
            time += Time.deltaTime;
            textMeshProUGUI.text = ((int)(7 - time)).ToString();
            yield return null;
        }
        // yield return null;
        textMeshProUGUI.gameObject.SetActive(false);
        if(sceneIndex + 1 > numScenePlayable){
            StartCoroutine(LoadSceneAsync(0));
        }
        else{
            StartCoroutine(LoadSceneAsync(sceneIndex + 1));
        }
    }

    IEnumerator LoadSceneAsync(int sceneIdToLoad){
        isLoadScene = true;
        LoadScreenUIObject.SetActive(true);

        yield return new WaitForSecondsRealtime(3);
        AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneIdToLoad);
        while(!loadSceneOperation.isDone){
            yield return null;
        }
    }

    public void QuitGame(){
        Application.Quit();
    }
}
