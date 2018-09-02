using UnityEngine.UI;
using UnityEngine;
using System;

public class EndLevelMenuUI : MonoBehaviour {

    public AwardSO chapter1Award;
    public AwardSO chapter2Award;
    public AwardSO chapter3Award;
    public GameParamSO gameParam;
    public GameObject medalGo;
    public GameObject secretGo;
    public GameObject bestTimeNew;
    public Text currentLevelTimeTxt;
    public Text bestTimeTxt;
    public Text toMedalTimeTxt;
    public Text attemptTxt;
    public Text levelNameTxt;

    
    //int attempts;

    void OnEnable()
    {
        SetChapterProgress();
        UpdateUI();

        ReplayManager.instance.StartReplay();
    }

    
    /*public void Setattempts(int _attempts)
    {
        attempts = _attempts;
    }*/
    void UpdateUI()
    {
        medalGo.SetActive(gameParam.currentLevel.medalM);
        secretGo.SetActive(gameParam.currentLevel.medalS);

        if (gameParam.currentLevel.currentTime < gameParam.currentLevel.bestTime)
        {
            bestTimeNew.SetActive(true);
            bestTimeTxt.gameObject.SetActive(false);
            gameParam.currentLevel.bestTime = gameParam.currentLevel.currentTime;//не хорошо.
        }
        else
        {
            bestTimeNew.SetActive(false);
            bestTimeTxt.gameObject.SetActive(true);
            bestTimeTxt.text = gameParam.currentLevel.bestTime.ToString("f2");
        }

        currentLevelTimeTxt.text = gameParam.currentLevel.currentTime.ToString("f2");
        toMedalTimeTxt.text = gameParam.currentLevel.forMedalTime.ToString("f2");
        attemptTxt.text = gameParam.currentLevel.attempts.ToString();
        levelNameTxt.text = gameParam.currentLevel.chapterN + "-" + gameParam.currentLevel.levelN + ": " + gameParam.currentLevel.levelName;
    }
    void SetChapterProgress()
    {
        int medals = Array.FindAll<GameLevelSO>(gameParam.currentChapter.levels, l => l.medalM).Length;
        int secrets = Array.FindAll<GameLevelSO>(gameParam.currentChapter.levels, l => l.medalS).Length;
        int levelscomplete = Array.FindAll<GameLevelSO>(gameParam.currentChapter.levels, l => l.isLevelComplete).Length;
        
        gameParam.currentChapter.progress = medals + secrets + levelscomplete;

        switch (gameParam.currentChapter.chapterName)//гавнецо
        {
            case "Forest":
                if (chapter1Award.condition == gameParam.currentChapter.progress)
                    chapter1Award.isComplete = true;
                break;
            case "Desert":
                if (chapter2Award.condition == gameParam.currentChapter.progress)
                    chapter2Award.isComplete = true;
                break;
            case "Factory":
                if (chapter3Award.condition == gameParam.currentChapter.progress)
                    chapter3Award.isComplete = true;
                break;
        }

        DataSaver.SaveData(gameParam.currentChapter, gameParam.currentChapter.chapterName);
        DataSaver.SaveData(chapter1Award, chapter1Award.awardName);
        DataSaver.SaveData(chapter2Award, chapter2Award.awardName);
        DataSaver.SaveData(chapter3Award, chapter3Award.awardName);
    }
    public void NextLevel()
    {
        //анимация смены уровня.
        LevelManager.instance.NextLevel();
    }
    
    public void RestartLevel()
    {
        //анимация рестарта уровня
        LevelManager.instance.RestartLevel();
    }
}
