using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ResultMenu : MonoBehaviour {

    public PlayerSO playerParam;
    public GameObject healthStat;
    public Text totalText;
    public Text waveText;

    public Text enemyKilledCountText;
    public Text enemyKilledMoneyText;
    public Text healthLeftCountText;
    public Text healthLeftMoneyText;

    public Text timeLeftCountText;
    public Text timeLeftMoneyText;

    const int WAVEBONUS = 42;
    const int baseLevelMoney = 104;

    void OnEnable()
    {
        healthStat.SetActive(!playerParam.isExitGame);
        Init();
    }

    void Init()
    {
        float killedMoney = (playerParam.levelEndMoneyDrop==0)?0:(playerParam.levelEndMoneyDrop + playerParam.waveCount * 2) * playerParam.moneyBonus;//*2 при 0 убитых, будет давать деньги
        float healthLeftPersent = ((float)playerParam.levelEndHealthLeft / playerParam.maxHeals);
        float healthMoney = (((baseLevelMoney + playerParam.waveCount * WAVEBONUS) * healthLeftPersent)) * playerParam.moneyBonus;
        int totalMoney = Mathf.RoundToInt(killedMoney+healthMoney);

        waveText.text = playerParam.waveCount.ToString();

        enemyKilledCountText.text = playerParam.levelEndEnemyKilled.ToString();
        enemyKilledMoneyText.text = Mathf.RoundToInt(killedMoney).ToString();

        healthLeftCountText.text = Mathf.RoundToInt(healthLeftPersent * 100).ToString() + "%";
        healthLeftMoneyText.text = Mathf.RoundToInt(healthMoney).ToString();
        
        //enemyKilledCountText.text = playerParam.levelEndTimeLeft.ToString();//Может и не будет
        //enemyKilledCountText.text = playerParam.levelEndTimeLeft.ToString();
        totalText.text = totalMoney.ToString();
        playerParam.money += totalMoney;
        playerParam.levelEnd = false;
        //DataController.instance.SaveGame(playerParam, playerParam.currentPath);
        DataSaver.SaveData<PlayerSO>(playerParam, playerParam.currentFileName);
    }

}
