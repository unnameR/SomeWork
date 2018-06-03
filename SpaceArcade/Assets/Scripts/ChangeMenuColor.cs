using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMenuColor : MonoBehaviour {

    public Sprite disableToglleSprite;
    public Image meinMenu;
    public Image upgradeMenu;
    public Image preview;
    public Sprite[] meinMenuSprites;
    public Sprite[] upgradeMenuSprites;
    public Image[] buttons;
    public Sprite[] buttonsSprites;
    public Image[] progressBars;
    public Sprite[] progressBarsSprites;
    public Sprite[] previewSprites;

    private Toggle currToggle;
    public void SetToggle(Toggle t)
    {
        currToggle = t;
    }
    public void DisableToglle(Image background)
    {
        if (!currToggle.isOn)
            background.sprite = disableToglleSprite;
	}
	
    public void ChangeUIColor(string colorName)
    {
        SetColor(buttons, buttonsSprites, colorName);
        SetColor(progressBars, progressBarsSprites, colorName);
        
        switch (colorName)
        {
            case "Blue":
                PlayerGameParameters.UIColor = "Blue";
                meinMenu.sprite = meinMenuSprites[0];
                upgradeMenu.sprite = upgradeMenuSprites[0];
                preview.sprite = previewSprites[0];

                break;
            case "Green":
                PlayerGameParameters.UIColor = "Green";
                meinMenu.sprite = meinMenuSprites[1];
                upgradeMenu.sprite = upgradeMenuSprites[1];
                preview.sprite = previewSprites[1];
                break;
            case "Red":
                PlayerGameParameters.UIColor = "Red";
                meinMenu.sprite = meinMenuSprites[2];
                upgradeMenu.sprite = upgradeMenuSprites[2];
                preview.sprite = previewSprites[2];
                break;
            case "Yellow":
                PlayerGameParameters.UIColor = "Yellow";
                meinMenu.sprite = meinMenuSprites[3];
                upgradeMenu.sprite = upgradeMenuSprites[3];
                preview.sprite = previewSprites[3];
                break;
        }
        
    }
    void SetColor(Image[] images, Sprite[] sprits,string colorName)
    {
        for (int i = 0; i < images.Length; i++)
        {
            switch (colorName)
            {
                case "Blue":
                    images[i].sprite = sprits[0];
                    break;
                case "Green":
                    images[i].sprite = sprits[1];
                    break;
                case "Red":
                    images[i].sprite = sprits[2];
                    break;
                case "Yellow":
                    images[i].sprite = sprits[3];
                    break;
            }
        }
    }
}
