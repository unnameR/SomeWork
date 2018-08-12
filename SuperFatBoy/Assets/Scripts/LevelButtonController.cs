using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelButtonController : MonoBehaviour {

    public Text timeTxt;
    public Text numberTxt;
    public GameObject medalM;
    public GameObject medalS;
    public GameObject lockIcon;
    public Image levelMedalM;
    public Image levelMedalS;
    public Text levelMedalMTimeTxt;
    public Text levelNameTxt;
    public Text statTxt;
    public Color levelCompleteColor;//B22222FF
    public Color buttonsActiveColor;//B22222FF
    public Color aMedalActiveColor;//FFAA00FF
    public Color sMedalActiveColor;//00AAFFFF

    //private Animator anim;
    private Image currentImage;
    private GameLevelSO levelStat;

    void Awake()
    {
        //anim = GetComponent<Animator>();
    }
    void OnEnable()
    {
        currentImage = GetComponent<Image>();
    }
    
    public void ActiveLevel(bool active)
    {
        timeTxt.text = levelStat.bestTime.ToString("f2");
        levelMedalMTimeTxt.text = levelStat.forMedalTime.ToString("f2");

        levelNameTxt.text = levelStat.chapterN + "-" + levelStat.levelN + ": " + levelStat.levelName;
        statTxt.text = levelStat.chapterN + "-" + levelStat.levelN;

        timeTxt.gameObject.SetActive(active && levelStat.bestTime!=999);
        levelNameTxt.gameObject.SetActive(active);

        levelMedalM.color = levelStat.medalM ? aMedalActiveColor : Color.grey;
        levelMedalS.color = levelStat.medalS ? sMedalActiveColor : Color.grey;

        if (levelStat.isLevelComplete)
        {
            currentImage.color = active ? buttonsActiveColor : levelCompleteColor;
            //numberTxt.color = active ? Color.black : Color.white;
        }
        else
        {
            currentImage.color = active ? Color.white : Color.grey;
            //numberTxt.color = active ? Color.white : Color.black;
        }
        //if (active)
            //anim.SetTrigger("active");
        //else
        transform.localScale = active ? new Vector3(1.2f, 1.2f,1f) : Vector3.one;//not work
        //что то не даёт менять размер кнопки когда игра.
    }
    public void LoadStat(GameLevelSO gl)
    {
        currentImage = GetComponent<Image>();
        levelStat = gl;
        medalM.SetActive(gl.medalM);
        medalS.SetActive(gl.medalS);
        lockIcon.SetActive(gl.isLevelLock);
        timeTxt.gameObject.SetActive(false);
        currentImage.color = gl.isLevelComplete ? levelCompleteColor : Color.grey;

        levelNameTxt.text = gl.levelName;
    }
}
