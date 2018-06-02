using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Texture2D cursor;
    public Text scoreText;
    public Text messageText;

    public DataController dataController;
    public GameObject messageBox;
    public GameObject continueBtn;
    public Animator upgradeAnim;
    public AudioSource aSourse;
    public AudioClip[] clicks;
    public AudioClip[] soundtracks;

    public GameObject[] toggles;
    [Header("Grades")]
    public BonusUpgrade[] bonuses;

    private List<int> scoretoupList= new List<int>();
    private MessageType messageType;

	void Start () {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
        scoreText.text = "Max. Score: "+PlayerGameParameters.maxScore.ToString();
        aSourse.clip = soundtracks[Random.Range(0,soundtracks.Length)];
        aSourse.Play();

        for (int i = 0; i < bonuses.Length; i++)
        {
            switch (bonuses[i].bonusType)
            {
                case UpgradeTyte.Shield:
                    bonuses[i].SetScoreToUp(PlayerGameParameters.gradePower_Shield);
                    break;
                case UpgradeTyte.Star:
                    bonuses[i].SetScoreToUp(PlayerGameParameters.gradePower_Star);
                    break;
                case UpgradeTyte.Pill:
                    bonuses[i].SetScoreToUp(PlayerGameParameters.gradePower_Pill);
                    break;
                case UpgradeTyte.Life:
                    bonuses[i].SetScoreToUp(PlayerGameParameters.gradePower_Life);
                    break;
                case UpgradeTyte.NextShip:
                    bonuses[i].SetScoreToUp(PlayerGameParameters.gradePower_NextShip);
                    break;
                case UpgradeTyte.NewColor:
                    bonuses[i].SetScoreToUp(PlayerGameParameters.gradePower_NextColor);
                    break;
            }
        }
        CheckMinScoreToUp();

        if (PlayerGameParameters.isFirstStart)
        {
            continueBtn.SetActive(false);
        }
        for (int i = 1; i < PlayerGameParameters.enableColorCount; i++)
        {
            toggles[i].SetActive(true);
        }
        switch (PlayerGameParameters.UIColor)
        {
            case "Blue":
                toggles[0].GetComponent<Toggle>().isOn = true;
                break;
            case "Green":
                toggles[1].GetComponent<Toggle>().isOn = true;
                break;
            case "Red":
                toggles[2].GetComponent<Toggle>().isOn = true;
                break;
            case "Yellow":
                toggles[3].GetComponent<Toggle>().isOn = true;
                break;
        }
	}
    public void CheckMinScoreToUp()
    {
        scoretoupList.Clear();
        for (int i = 0; i < bonuses.Length; i++)
        {
            //при старте BonusUpgrade устанавливаются и scoreToUp параметры, но так как
            //меню Upgrade не активировалось это не произошло, по этому и используются 
            //стандартные параметры scoreToUp
            scoretoupList.Add(bonuses[i].scoreToUp);
        }

        scoretoupList.Sort();
        PlayerGameParameters.minScoreToUp = scoretoupList[0];
        
        if (PlayerGameParameters.minScoreToUp <= PlayerGameParameters.maxScore)
        {
            upgradeAnim.SetBool("ready", true);
        }
        else upgradeAnim.SetBool("ready", false);
    }
    public void ToggleEnable()
    {
       toggles[PlayerGameParameters.enableColorCount-1].SetActive(true);
    }
    public void ClickSound()
    {
        aSourse.PlayOneShot(clicks[Random.Range(0, clicks.Length)]);
    }
	public void Continue () {
        SceneManager.LoadScene(1);
	}
    public void NewGame()
    {
        //Новая игра обнулит весь ваш прогресс, начать новую игру? да/нет
        //если игрок запустил игру впервый раз, доступна только Навая игра и
        //отсутствует предупреждение.
        messageType = MessageType.NewGame;
        if (!PlayerGameParameters.isFirstStart)
        {
            messageText.text = "Do you want to start New Game?";
            messageBox.SetActive(true);
        }
        else
        {
            Yes();
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Yes()
    {
        switch (messageType)
        {
            case MessageType.NewGame:
                PlayerGameParameters.NewGame();
                dataController.SaveGame();
                SceneManager.LoadScene(1);
                break;
        }
    }
    public enum MessageType {NewGame,Error }
}
