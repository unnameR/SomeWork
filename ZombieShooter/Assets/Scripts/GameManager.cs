using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static int currentDay=1;
    public static int score=0;

    public static bool isNight;

    public Animator backgroundAnim;
    public Animator UIAnim;
    public Animator houseAnim;
    public Animator notificationAnim;

    public Text scoreUI;
    public Text curDayUI;
    //сколько секунд длятся сутки
    public float dayTimer = 30f;
    public static float nightTimer = 60f;

    private float currentTime;
	// Use this for initialization
	void Start () {
        currentTime = Time.time + 10;//dayTimer;
	}
	
	void Update () {
        scoreUI.text = "Score: " + score;
        if (isNight)
        {
            if (Time.time >= currentTime)//если была ночь - наступает день, после того как проходит 60 сек ночного времени
            {
                currentTime=Time.time+dayTimer;
                currentDay ++;
                isNight = false;
                DayTime();
            }
        }
        else
        {
            if (Time.time >= currentTime)//если был день - наступает ночь, после того как проходит 30 сек дневного времени
            {
                currentTime = Time.time + nightTimer;
                isNight = true;
                NightTime();
            }
        }
	}
    void DayTime()
    {
        backgroundAnim.SetBool("isNight", false);
        UIAnim.SetBool("isNight", false);
        houseAnim.SetBool("isNight", false);
    }
    void NightTime()
    {
        backgroundAnim.SetBool("isNight",true);
        UIAnim.SetBool("isNight", true);
        houseAnim.SetBool("isNight", true);

        notificationAnim.Play("DayChanging");
        StartCoroutine(ChangeDay());
    }
    IEnumerator ChangeDay()
    {
        yield return new WaitForSeconds(1f);
        curDayUI.text = "Night: "+currentDay;
    }
}
