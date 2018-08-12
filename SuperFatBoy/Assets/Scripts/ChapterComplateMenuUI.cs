using UnityEngine.UI;
using UnityEngine;

public class ChapterComplateMenuUI : MonoBehaviour {

    public GameParamSO gameParam;
    public Text chapterNameTxt;
    public Text levelsCompleteTxt;
    public Text medalsGetCountTxt;
    public Text secretsGetCountTxt;
    public Image background;

    void OnEnable()
    {
        chapterNameTxt.text = gameParam.currentChapter.chapterName;
        levelsCompleteTxt.text = LevelsComplete() + "/" + gameParam.currentChapter.levels.Length;
        medalsGetCountTxt.text = "x " + MedalsGet();
        secretsGetCountTxt.text = "x " + SecretsGet();
    }
    public void NextChapter()
    {
        //load main menu scene and Active next chapter
        LevelManager.instance.BackToMainMenu();//не так
    }
    int LevelsComplete()
    {
        return System.Array.FindAll(gameParam.currentChapter.levels, l => l.isLevelComplete).Length;
    }
    int MedalsGet()
    {
        return System.Array.FindAll(gameParam.currentChapter.levels, l => l.medalM).Length;
    }
    int SecretsGet()
    {
        return System.Array.FindAll(gameParam.currentChapter.levels, l => l.medalS).Length;
    }
}
