using UnityEngine.UI;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour {

    public GameParamSO gameParam;
    public GameObject medalGo;
    public GameObject secretGo;
    public Text levelNameTxt;
    public Text bestTimeTxt;
    public Text forMedalTimeTxt;

    void OnEnable()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        medalGo.SetActive(gameParam.currentLevel.medalM);
        secretGo.SetActive(gameParam.currentLevel.medalS);
        levelNameTxt.text = gameParam.currentLevel.chapterN + "-" + gameParam.currentLevel.levelN + ": " + gameParam.currentLevel.levelName;
        bestTimeTxt.text = gameParam.currentLevel.bestTime.ToString("f2");
        forMedalTimeTxt.text = gameParam.currentLevel.forMedalTime.ToString("f2");
    }
}
