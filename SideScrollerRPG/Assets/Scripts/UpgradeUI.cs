using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour {

    public UpgradesSO currentUpgrade;
    public PlayerSO playerParam;
    public AnimationComtroller aController;
    public UpgradeBtn[] upButtons;

    public Text waveCountTxt;
    public Text moneyTxt;

    [Space(10)]
    [Header("DisplayUpgrade")]
    public Image uIcon;
    public Text uName;
    public Text uDescription;
    public Text uPrice;
    public Button uBuyBtn;
    public GameObject uLevel;//!!закашиваются все до нуужного уровняю Доделать!
    public Text uLockText;

    void OnEnable()
    {
        Init(true);
    }
    void Init(bool playAnim)
    {
        waveCountTxt.text = playerParam.waveCount.ToString();
        moneyTxt.text = playerParam.money.ToString();

        foreach (var upB in upButtons)//после покупки улучшений происходит инициализация и анимации выполняются  повторно
        {
            upB.Init(playerParam.money, playerParam.waveCount, playerParam.maxUpgradeLvl, playAnim);
        }
        
        ShowUpgrade(currentUpgrade);
	}

    public void ShowUpgrade(UpgradesSO upgrade)
    {
        uIcon.sprite = upgrade.icon;
        uName.text = upgrade._name;
        uDescription.text = upgrade.description;
        uPrice.text = upgrade.prise.ToString();
        uBuyBtn.interactable = true;
        currentUpgrade = upgrade;


        if (upgrade.upLevel >= playerParam.maxUpgradeLvl)
        {
            uPrice.text = "MAXED!";
            uBuyBtn.interactable = false;
        }
        
        for (int i = 0; i < uLevel.transform.childCount; i++)
        {
            uLevel.transform.GetChild(i).gameObject.SetActive(i < playerParam.maxUpgradeLvl);
            uLevel.transform.GetChild(i).GetComponent<Image>().color =
                (i < upgrade.upLevel) ? Color.white : Color.grey;
        }
        if (upgrade.isLock)
        {
            uLevel.SetActive(false);
            uLockText.gameObject.SetActive(true);
            uLockText.text = upgrade.lockText;
            uPrice.text = "---";
            uBuyBtn.interactable = false;
            //включаем маску, делаем кнопку неаактивной, меняем текст на "-"
        }
        else
        {
            uLockText.gameObject.SetActive(false);
            uLevel.SetActive(true);
        }
    }
    public void BuyUpgrade()
    {
        if (playerParam.money < currentUpgrade.prise)
        {
            //показать табличку что недостаточно денег
            AudioManager._instance.PlaySoundEffect("EnemyDie");
            aController.NoMoneyAnim();
            return;
        }
        AudioManager._instance.PlaySoundEffect("BuyUpgrade");

        playerParam.money -= currentUpgrade.prise;
        currentUpgrade.upLevel++;
        currentUpgrade.prise += 800;//доделать

        switch (currentUpgrade._name)
        {
            case "Mags":
                playerParam.magazineSizeLvl++;
                playerParam.magazineSize += (int)currentUpgrade.increaseStat;
                break;
            case "Gun Dmg":
                playerParam.damageLvl++;
                playerParam.damage += (int)currentUpgrade.increaseStat;
                break;
            case "Gun Body":
                playerParam.pushbackPowerLvl++;
                playerParam.pushbackPower += currentUpgrade.increaseStat;
                break;
            case "Backpack":
                playerParam.moneyBonusLvl++;
                playerParam.moneyBonus += currentUpgrade.increaseStat;
                break;
            case "Armor":
                playerParam.armorLvl++;
                playerParam.maxHeals += (int)currentUpgrade.increaseStat;
                break;
            case "Gun Canon":
                playerParam.bulletSplashLvl++;
                playerParam.bulletSplash += (int)currentUpgrade.increaseStat;
                break;
            case "Grenade":
                playerParam.grenadeDamageLvl++;
                playerParam.grenadeDamage += (int)currentUpgrade.increaseStat;
                break;
            case "Health":
                playerParam.healCDLvl++;
                playerParam.healCD += currentUpgrade.increaseStat;
                break;
            case "E.M.P.":
                playerParam.freezeCDLvl++;
                playerParam.freezeCD += currentUpgrade.increaseStat;
                break;
        }
        DataSaver.SaveData<PlayerSO>(playerParam, playerParam.currentFileName);
        Init(false);
    }
    
}
