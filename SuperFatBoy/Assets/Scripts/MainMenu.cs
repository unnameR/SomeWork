using UnityEngine;

public class MainMenu : MonoBehaviour {

    public AwardSO secretAll;
    public AwardSO medalAll;
    public ChapterSO[] chaptersSO;
    public ChapterMenu chapterMenu;
    public MenuSwitcher menuSwitcher;
    public MeinMenuUI meinMenuUI;

    int chapterSelected;
    int chapterCount;
    int progressAll;
    int levelsAll;
    int medalsAll;
    int secretsAll;

    void OnEnable()//долго грузится. Нужно сделать нормально
    {
        progressAll = 0;
        for (int i = 0; i < chaptersSO.Length; i++)
        {
            meinMenuUI.UnlockIcon(i, chaptersSO[i].isLock);
            if (!medalAll.isComplete) medalsAll += System.Array.FindAll<GameLevelSO>(chaptersSO[i].levels, l => l.medalM).Length;
            if (!secretAll.isComplete) secretsAll += System.Array.FindAll<GameLevelSO>(chaptersSO[i].levels, l => l.medalS).Length;
            levelsAll += chaptersSO[i].levels.Length * 3;//на каждом уровне по 3 ачивки
            progressAll +=chaptersSO[i].progress;
        }

        if (!secretAll.isComplete)
        {
            secretAll.isComplete = secretsAll == secretAll.condition;
            DataSaver.SaveData(secretAll, secretAll.awardName);
        }
        if (!medalAll.isComplete)
        {
            medalAll.isComplete = medalsAll == medalAll.condition;
            DataSaver.SaveData(medalAll, medalAll.awardName);
        }

        meinMenuUI.SetProgress(progressAll, levelsAll * chaptersSO.Length);

        ChapterSO[] notLock = System.Array.FindAll<ChapterSO>(chaptersSO, ch => !ch.isLock);
        chapterCount = notLock.Length - 1;

        ChangeChapter(chapterSelected);
    }
    public void ChangeLeft()
    {
        chapterSelected = (chapterSelected <= 0) ? chapterCount : --chapterSelected;
        ChangeChapter(chapterSelected);
    }
    public void ChangeRight()
    {
        chapterSelected = (chapterSelected >= chapterCount) ? 0 : ++chapterSelected;
        ChangeChapter(chapterSelected);
    }
    void ChangeChapter(int chapter)
    {
        meinMenuUI.SelectChapter(chapter, chaptersSO[chapter].chapterName);
    }
    public void StartGame()
    {
        menuSwitcher.SetCurrentMenu(this.gameObject);
        menuSwitcher.SetNewMenu(chapterMenu.gameObject);
        chapterMenu.LoadLevelsParam(chaptersSO[chapterSelected]);
    }
}
