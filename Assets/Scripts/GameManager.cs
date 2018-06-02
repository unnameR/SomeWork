using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Text scoreText;
    public GameObject endGameUI;
    public GameObject helpGameUI;
    public Text helpGameUIText;

    public GameObject[] Ships;
    public AudioClip playerDieSound;
    public AudioSource aSourse;
    public AudioClip[] Soundtracks;

    public static int score=0000;

    public static int pillCount;
    public static int shieldCount;
    public static int starCount;
    public static int playerLifeCount;
    public static float fireRate;
    public static float moveSpeed;
    public static bool isStarRecived;
    public static bool isPillRecived;

    public float timeBeforeRespawnPlayer = 3f;
    public float slowmoDuration = 5f;

    void Awake()
    {
        Cursor.lockState= CursorLockMode.Locked;
        pillCount = PlayerGameParameters.pillCount;
        shieldCount = PlayerGameParameters.shieldCount;
        starCount = PlayerGameParameters.starCount;
        playerLifeCount = PlayerGameParameters.playerLifeCount;
        fireRate = PlayerGameParameters.fireRate;
        moveSpeed = PlayerGameParameters.moveSpeed;
        score = 0;

        while (starCount >= 10)
        {
            playerLifeCount++;
            starCount -= 10;
        }
    }
	void Start () {
        aSourse.clip = Soundtracks[Random.Range(0, Soundtracks.Length)];
        aSourse.Play();
        scoreText.text = score.ToString();
        Instantiate(Ships[PlayerGameParameters.playerShip]);
        if (PlayerGameParameters.isFirstStart)
        {
            ShowHelp("Press Left Mouse Button to fire!");
        }
	}
	
	void Update () {
        scoreText.text = score.ToString();
        if (Input.GetButtonDown("Cancel"))
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            endGameUI.SetActive(true);
        }
	}
    public void ShowHelp(string text)
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        helpGameUI.SetActive(true);
        helpGameUIText.text = text;
    }
    public void OK()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        helpGameUI.SetActive(false);
    }
    public void PlayerDie()
    {
        Time.timeScale = 1;//убираем слоумо
        aSourse.PlayOneShot(playerDieSound);
        playerLifeCount--;

        if (playerLifeCount > 0)
            StartCoroutine(Resp());
        else
        {
            if (score > PlayerGameParameters.maxScore)
                PlayerGameParameters.maxScore = score;
            Cursor.lockState = CursorLockMode.None;
            endGameUI.SetActive(true);
        }
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        PlayerGameParameters.isFirstStart = false;
        GameObject.Find("DataController").GetComponent<DataController>().SaveGame();
        SceneManager.LoadScene(0);
    }
    public void ActiveSlowMo()
    {
        pillCount--;
        StartCoroutine(SlowMo());
    }
    IEnumerator Resp()
    {
        yield return new WaitForSeconds(timeBeforeRespawnPlayer);

        GameObject resp = Instantiate(Ships[PlayerGameParameters.playerShip]);
        resp.GetComponent<PlayerController>().IsRespawned = true;
    }
    IEnumerator SlowMo()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(slowmoDuration);
        Time.timeScale = 1;
    }
}
