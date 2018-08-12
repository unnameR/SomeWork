using UnityEngine;

[CreateAssetMenu()]
public class GameLevelSO : ScriptableObject {

    public int chapterN;
    public int levelN;
    public int attempts;//возможно не так.
    public bool medalM;
    public bool medalS;
    public bool isLevelComplete;
    public bool isLevelLock;
    public float currentTime;
    public float bestTime = 999;
    public float forMedalTime;
    public string levelName;

    public GameObject levelPrefab;

    public void ResetLevel()
    {
        bestTime = 0;
        medalM = medalS = false;
        isLevelComplete = false;
        currentTime = 0.0f; 
        bestTime = 999;
    }
}
