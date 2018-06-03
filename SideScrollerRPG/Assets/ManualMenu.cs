using UnityEngine.UI;
using UnityEngine;

public class ManualMenu : MonoBehaviour {

    public GameObject firstManual;
    public GameObject secondManual;
    public GameObject therdManual;
    public Text countText;
    public Text lableText;

    int currentManual=0;
    public void ChangeLeft()
    {
        ChangeInstruction((currentManual<=0)?0:--currentManual);
    }
    public void ChangeRight()
    {
        ChangeInstruction((currentManual >= 2) ? 2 : ++currentManual);
    }
    void ChangeInstruction(int setActive)
    {
        switch (setActive)
        {
            case 0:
                firstManual.SetActive(true);
                secondManual.SetActive(false);
                therdManual.SetActive(false);
                countText.text = "1/3";
                lableText.text = "Shooting";
                break;
            case 1:
                firstManual.SetActive(false);
                secondManual.SetActive(true);
                therdManual.SetActive(false);
                countText.text = "2/3";
                lableText.text = "Movement";
                break;
            case 2:
                firstManual.SetActive(false);
                secondManual.SetActive(false);
                therdManual.SetActive(true);
                countText.text = "3/3";
                lableText.text = "Labels";
                break;
        }
    }
}
