using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUISettings : MonoBehaviour {

    public Text pillText;
    public Text shieldText;
    public Text starText;
    public Text playerLifeText;

    public Image pillImage;
    public Image shieldImage;
    public Image starImage;
    public Image playerLifeImage;

    public Image endGameUI;
    public Image endGameUIButton;

    public ColorConfigurations[] colorConfig;

    public SpriteRenderer[] playerShips;

    private string color;
    private int currentShip;

    void Awake()
    {
        color = PlayerGameParameters.UIColor;
        currentShip = PlayerGameParameters.playerShip;
    }
	void Start () {
        UpdatePlayerParam();
        SetColorConfig(color);
	}
	
	// Update is called once per frame
	void Update () {
        UpdatePlayerParam();
	}
    void UpdatePlayerParam()
    {
        pillText.text = GameManager.pillCount.ToString();
        shieldText.text = GameManager.shieldCount.ToString();
        starText.text = GameManager.starCount.ToString();
        playerLifeText.text = GameManager.playerLifeCount.ToString();
    }
    void SetColorConfig(string colorName)
    {
        foreach (ColorConfigurations config in colorConfig)
        {
            if (colorName == config.colorName)
            {
                pillImage.sprite = config.pillColor;
                shieldImage.sprite = config.powerUpShieldColor;
                starImage.sprite = config.powerUpStarColor;
                playerShips[currentShip].sprite = config.shipsColor[currentShip];
                playerLifeImage.sprite = config.lifesColor[currentShip];
                endGameUI.sprite = config.menuColor;
                endGameUIButton.sprite = config.buttonColor;
            }
        }
    }
}
