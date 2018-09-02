using UnityEngine.UI;
using UnityEngine;

public class WastedMenuUI : MonoBehaviour {

    public GameParamSO gameParam;
    public Text attemptTxt;
    public Button respawnBtn;
    public Text text;
    public Text costTxt;

    public GameObject respContainer;
    int cost;
    bool haveCheckpoint;
    void OnEnable()
    {
        if (haveCheckpoint)
        {
            cost = 2 * LevelManager.instance.respawnBuyCount;//если будут бесплатные респы, надо поменять.
            attemptTxt.text = "ATTEMPT " + gameParam.currentLevel.attempts.ToString();
            costTxt.text = cost.ToString();

            if (gameParam.playerMoney < cost)
            {
                respawnBtn.interactable = false;
                text.color = Color.grey;
                costTxt.color = Color.grey;
            }
        }
        else respContainer.SetActive(false);

    }
    public void SetCheckpoint(bool checkpoint)
    {
        haveCheckpoint = checkpoint;
    }
    public void BuyRespawn()
    {
        gameParam.playerMoney -= cost;
        LevelManager.instance.respawnBuyCount++;
        LevelManager.instance.Respawn();
    }
}
