using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSkill : MonoBehaviour
{
    private int currentExp = 0;
    private int currentLevel = 0;

    [SerializeField] private List<int> expsEachLevel = new();

    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }

    private void Start() {
        InvokeExtensionCode.Invoke(SceneGameManager.instance, () => Obsever.PostEvent(EventID.Player_SPAWN, new KeyValuePair<string, object>("maxExp", expsEachLevel[currentLevel + 1])), Time.unscaledDeltaTime);
    }

    public void AddExp(int expAdded){
        currentExp += expAdded;
        PlayerLevelUp();
        Obsever.PostEvent(EventID.Player_TAKEEXP, new KeyValuePair<string, object>("exp", currentExp));
    }

    public void PlayerLevelUp(){
        if(currentLevel + 1 < expsEachLevel.Count){
            if(currentExp < expsEachLevel[currentLevel + 1]) return;
            currentLevel++;
            Obsever.PostEvent(EventID.Player_LEVELUP, new KeyValuePair<string, object>("currentLevel", currentLevel));
            if(currentLevel + 1 < expsEachLevel.Count) Obsever.PostEvent(EventID.Player_LEVELUP, new KeyValuePair<string, object>("maxExp", expsEachLevel[currentLevel + 1]));
        }
    }
}
