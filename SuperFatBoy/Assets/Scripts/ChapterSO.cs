using UnityEngine;

[CreateAssetMenu()]
public class ChapterSO : ScriptableObject {

    public string chapterName;
    public int bossUnlockLevelCount;
    public int progress;//1 очка за выполнение уровня, 1 за каждую медальку. =9уровней*3
    public bool isLock;
    public Sprite background;
    public GameLevelSO[] levels;
    public SoundSO chapterMainSound;
}
