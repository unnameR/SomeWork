using UnityEngine;

[CreateAssetMenu()]
public class GameParamSO : ScriptableObject {

    public GameLevelSO currentLevel;
    public ChapterSO currentChapter;
    public int playerMoney;
    public float soundVolume;
    public float musicVolume;
    public int controllSize;
    //public bool filesCreated;
}
