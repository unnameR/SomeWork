using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterMenu : MonoBehaviour {

    public GameParamSO gameParam;
    public LevelButtonController[] levelButtons;
    public GameObject bossLockLevel;
    public Animator bossAnim;
    public Text bossUnlockTxt;
    public Text chapterTxt;
    public Text progressTxt;
    public Image progressBarImg;
    public Image background;

    private ChapterSO chapter;
    private LevelButtonController oldActiveLevel;
    private int activeLevel;

    void Init()
    {
        float progressPercent = (float)chapter.progress / (chapter.levels.Length * 3);

        chapterTxt.text = chapter.chapterName;
        progressTxt.text = (progressPercent * 100).ToString("f0") + "%";
        progressBarImg.fillAmount = (float)chapter.progress / (chapter.levels.Length * 3);
        oldActiveLevel = levelButtons[0];
        
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].LoadStat(chapter.levels[i]);
        }

        GameLevelSO[] levelsComplete = System.Array.FindAll(chapter.levels, a => a.isLevelComplete);
        bossUnlockTxt.text = levelsComplete.Length + "/" + chapter.bossUnlockLevelCount;
        if (levelsComplete.Length >= chapter.bossUnlockLevelCount)
        {
            chapter.levels[8].isLevelLock = false;//гавнецо, сделать нормально.
            bossLockLevel.SetActive(false);
            bossAnim.SetTrigger("unlock");
        }

        ActiveLevel(0);
    }
    public void LoadLevelsParam(ChapterSO _chapter)
    {
        chapter = _chapter;
        background.sprite = chapter.background;
        Init();
    }
    public void ActiveLevel(int number)
    {
        if (chapter.levels[number].isLevelLock)
            return;

        activeLevel = number;
        oldActiveLevel.ActiveLevel(false);
        levelButtons[number].ActiveLevel(true);
        oldActiveLevel = levelButtons[number];
    }
    public void Play()
    {
        gameParam.currentLevel = chapter.levels[activeLevel];
        Debug.Log(chapter.levels[activeLevel].chapterN + " - " + chapter.levels[activeLevel].levelN + " level loading...");
        SceneManager.LoadScene(1);
    }
}

