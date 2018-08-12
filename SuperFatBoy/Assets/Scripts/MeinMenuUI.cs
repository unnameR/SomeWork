using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MeinMenuUI : MonoBehaviour {

    public GameObject[] chaptersIcon;
    public GameObject[] lockIcons;
    public Text[] chaptersText;
    public Text chapterNameTxt;
    public Text progressTxt;
    public Image progressBarImg;
    public Color selectColor;

    int oldSelectedChapter = 0;

    public void UnlockIcon(int i,bool isLock)
    {
        lockIcons[i].SetActive(isLock);
    }
    public void SetProgress(int progress, int max)
    {
        float progressPercent = (float)progress / max;

        progressTxt.text = (progressPercent * 100).ToString("f0") + "%";
        progressBarImg.fillAmount = (float)progress / max;
    }
    public void SelectChapter(int chapter, string name)
    {
        chaptersIcon[oldSelectedChapter].transform.localScale = new Vector3(1f, 1f, 1f);
        chaptersIcon[oldSelectedChapter].GetComponent<Image>().color = Color.white;
        chaptersText[oldSelectedChapter].color = Color.black;

        chaptersIcon[chapter].transform.localScale = new Vector3(1.2f, 1.2f, 1f);
        chaptersIcon[chapter].GetComponent<Image>().color = selectColor;
        chaptersText[chapter].color = Color.white;

        chapterNameTxt.text = name;

        oldSelectedChapter = chapter;
    }
}
