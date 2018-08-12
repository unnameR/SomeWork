using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class LevelUI : MonoBehaviour {

    public RectTransform[] controllUI;
    public Text timer;
    public MenuSwitcher ms;
    public GameObject levelCompleteMenu;
    public GameObject chapterCompleteMenu;


    public void UpdateTimer(float currentTime)
    {
        timer.text = currentTime.ToString("f2");
    }
    public void SetControllUISize(int size)
    {
        foreach (var ui in controllUI)
        {
            ui.sizeDelta = new Vector2(80 + 10 * size, 80 + 10 * size);
        }
    }
    public void LevelComplete()
    {
        ms.SetCurrentMenu(gameObject);
        ms.SetNewMenu(levelCompleteMenu);
    }
    public void ChapterComplete()
    {
        ms.SetCurrentMenu(gameObject);
        ms.SetNewMenu(chapterCompleteMenu);
    }
    
}
