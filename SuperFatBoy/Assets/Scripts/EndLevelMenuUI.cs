using UnityEngine.UI;
using UnityEngine;
using System;

public class EndLevelMenuUI : MonoBehaviour {

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

    void Start()
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
    }
    public void NextLevel()
    {
        //анимация смены уровня.
        LevelManager.instance.NextLevel();
    }
    public void BackToMainMenu()
    {
        //анимация смены сцены.
        LevelManager.instance.BackToMainMenu();
    }
    public void RestartLevel()
    {
        //анимация рестарта уровня
        LevelManager.instance.RestartLevel();
    }
}
