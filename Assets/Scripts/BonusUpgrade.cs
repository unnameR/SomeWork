using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BonusUpgrade : MonoBehaviour {

    public Button bonusButton;
    public Image progress;
    public Text scoreToUpText;
    public int scoreToUp = 200;
    public UpgradeTyte bonusType;
    private bool maxUpdate;

    private int currentScore;
    //private int gradePower;
	// Use this for initialization

	void Start () {
        currentScore = PlayerGameParameters.maxScore;
        SetBonus();
	}
    public void SetScoreToUp(int gradePower)
    {
        for (int i = 0; i < gradePower; i++)
        {
            scoreToUp *= 2;
        }
    }
    void SetBonus()
    {
        scoreToUpText.text = currentScore.ToString() + "/" + scoreToUp.ToString();

        if (!maxUpdate)
        {
            if (currentScore < scoreToUp)
            {
                bonusButton.interactable = false;
                progress.fillAmount = (float)currentScore / (float)scoreToUp;
            }
            else
            {
                bonusButton.interactable = true;
                progress.fillAmount = 1;
            }
        }
        else
        {
            bonusButton.interactable = false;
            scoreToUpText.text = scoreToUp.ToString() + "/" + scoreToUp.ToString();
            progress.fillAmount = 1;
        }
    }
    public void ActivateBonus()
    {
        switch (bonusType)
        {
            case UpgradeTyte.Shield:
                PlayerGameParameters.shieldCount++;
                PlayerGameParameters.gradePower_Shield++;
                scoreToUp *= 2;
                break;
            case UpgradeTyte.Star:
                PlayerGameParameters.starCount++;
                PlayerGameParameters.gradePower_Star++;
                scoreToUp *= 2;
                break;
            case UpgradeTyte.Pill:
                PlayerGameParameters.pillCount++;
                PlayerGameParameters.gradePower_Pill++;
                scoreToUp *= 2;
                break;
            case UpgradeTyte.Life:
                PlayerGameParameters.playerLifeCount++;
                PlayerGameParameters.gradePower_Life++;
                scoreToUp *= 2;
                break;
            case UpgradeTyte.NextShip:
                if (PlayerGameParameters.playerShip < 3)
                {
                    PlayerGameParameters.playerShip++;
                    PlayerGameParameters.gradePower_NextShip++;
                    scoreToUp *= 2;
                }
                else maxUpdate = true;
                break;
            case UpgradeTyte.NewColor:
                if (PlayerGameParameters.enableColorCount < 4)
                {
                    PlayerGameParameters.enableColorCount++;
                    PlayerGameParameters.gradePower_NextColor++;
                    scoreToUp *= 2;
                }
                else maxUpdate = true;
                break;
        }

        GameObject.Find("MainMenu").GetComponent<MainMenu>().CheckMinScoreToUp();
        GameObject.Find("DataController").GetComponent<DataController>().SaveGame();
        SetBonus();
    }
}
public enum UpgradeTyte { Shield, Star, Pill, Life, NextShip, NewColor }
