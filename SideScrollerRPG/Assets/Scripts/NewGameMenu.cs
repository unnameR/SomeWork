using UnityEngine.UI;
using UnityEngine;

public class NewGameMenu : MonoBehaviour {

    public PlayerSO playerParam;
    public UpgradesSO[] upgrades;
    public MissionSO[] missions;
    public EnemySO[] enemies;
    public AnimationComtroller animC;
    public SceneChanger sceneC;
    public Text waveText; 
    public GameObject newGameBackground;
    public GameObject changeFromMenu;
    public GameObject changeToMenu;

    public string fileName = "Game1";

    private int waveCount;

    void OnEnable()
    {
        if (playerParam.levelEnd || !DataSaver.LoadData(playerParam, fileName))//что бы не загружать данные когда игрок завершил уровень.
        {
            newGameBackground.SetActive(true);
            return;
        }
        newGameBackground.SetActive(false);
        waveCount = playerParam.waveCount;
        waveText.text = waveCount.ToString();
    }
    void ReLoadGameSettings()
    {
        playerParam.CheckMaxUpLevel();
        SetUpgrades();
        SetEnemies();
    }
   
    private void SetUpgrades()
    {
        foreach (UpgradesSO item in upgrades)
        {
            switch (item._name)//гавнецо
            {
                case "Mags":
                     item.LoadUpgrade(playerParam.magazineSizeLvl);
                    break;
                case "Gun Dmg":
                    item.LoadUpgrade(playerParam.damageLvl);
                    break;
                case "Gun Body":
                    item.LoadUpgrade(playerParam.pushbackPowerLvl);
                    break;
                case "Backpack":
                    item.LoadUpgrade(playerParam.moneyBonusLvl);
                    break;
                case "Armor":
                    item.LoadUpgrade(playerParam.armorLvl);
                    break;
                case "Gun Canon":
                    item.LoadUpgrade(playerParam.bulletSplashLvl);
                    break;
                case "Grenade":
                    item.LoadUpgrade(playerParam.grenadeDamageLvl);
                    break;
                case "Health":
                    item.LoadUpgrade(playerParam.healCDLvl);
                    break;
                case "E.M.P.":
                    item.LoadUpgrade(playerParam.freezeCDLvl);
                    break;
            }
        }
    }
    private void SetEnemies()
    {
        foreach (EnemySO enemy in enemies)
        {
            enemy.LoadStats(waveCount);
        }
    }
    private void SetMissions()//доделать
    {
        //hz...
    }
    public void Reset()
    {
        DataSaver.LoadData(playerParam,fileName);
        if (DataSaver.DeleteData(fileName))
        {
            newGameBackground.SetActive(true);
            playerParam.ToDefault();//обнулить внутрений файл
            ReLoadGameSettings();//обнулить внутрених юнитов и улучшения
        }
    }
    public void StartGame()
    {
        if (!DataSaver.LoadData(playerParam, fileName))// если начали новую игру. кидаем сразу в бой
        {
            playerParam.ToDefault();
            ReLoadGameSettings();//обнуляем тукещих юнитов и улучшения
            playerParam.currentFileName = fileName;
            DataSaver.SaveData<PlayerSO>(playerParam, fileName);
            sceneC.SetSceneName("GameScene");
        }
        else //если продолжаем игру, переходим на маню улучшений
        {
            ReLoadGameSettings();
            animC.ChangeFrom(changeFromMenu);
            animC.ChangeTo(changeToMenu);
        }

        Debug.Log(playerParam.currentFileName);
    }
}
