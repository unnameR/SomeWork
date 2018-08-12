using UnityEngine;

public class MainMenu : MonoBehaviour {

    public ChapterSO[] chaptersSO;
    public ChapterMenu chapterMenu;
    public MenuSwitcher menuSwitcher;
    public MeinMenuUI meinMenuUI;

    int chapterSelected;
    int chapterCount;
    int progressAll;
    int levelsAll;

    void OnEnable()
    {
        progressAll = 0;
        for (int i = 0; i < chaptersSO.Length; i++)
        {
            meinMenuUI.UnlockIcon(i, chaptersSO[i].isLock);
            levelsAll = +chaptersSO[i].levels.Length * 3;
            progressAll +=chaptersSO[i].progress;
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
