using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Preloader : MonoBehaviour {

    //public SceneChanger sChanger;
    public AwardSO[] awards;
    public ChapterSO[] chapters;
    public GameParamSO gameParam;
    public Image loadingBar;

    void Start()
    {
        if (!DataSaver.LoadData(gameParam, "Game Param")) CreateDataFiles();//возможно стоит сделать подругому.
        else LoadData();
        StartCoroutine(LoadAsync());
    }
    void CreateDataFiles()//при первом запуске неоткуда загружать сохранения, по этому нужно или проверять на нул или сделать первый сейв до загрузки
    {
        //gameParam.filesCreated = true;
        for (int i = 0; i < chapters.Length; i++)//chapters and levels
        {
            for (int a = 0; a < chapters[i].levels.Length; a++)
            {
                DataSaver.SaveData(chapters[i].levels[a], chapters[i].levels[a].levelName);//"Chapter" + i + " Levels");
            }
            DataSaver.SaveData(chapters[i], chapters[i].chapterName); //"Chapter" + i);
        }

        DataSaver.SaveData(gameParam, "Game Param");//game param

        for (int i = 0; i < awards.Length; i++)
        {
            DataSaver.SaveData(awards[i], awards[i].awardName);//"Award" + i);//awards
        }
    }
    void LoadData()
    {
        for (int i = 0; i < chapters.Length; i++)//chapters and levels
        {
            for (int a = 0; a < chapters[i].levels.Length; a++)
            {
                DataSaver.LoadData(chapters[i].levels[a], chapters[i].levels[a].levelName);//"Chapter" + i + " Levels");
            }
            DataSaver.LoadData(chapters[i], chapters[i].chapterName); //"Chapter" + i);
        }

        DataSaver.LoadData(gameParam, "Game Param");//game param

        for (int i = 0; i < awards.Length; i++)
        {
            DataSaver.LoadData(awards[i], awards[i].awardName);//"Award" + i);//awards
        }
    }
    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("MenuScene");
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.fillAmount = progress;
            yield return null;
        }
    }
}
