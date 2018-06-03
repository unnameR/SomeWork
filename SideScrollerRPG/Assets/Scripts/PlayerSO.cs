using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerParam")]
public class PlayerSO : ScriptableObject {

    [Header("Battle Param")]
    public int maxHeals = 100;
    public int damage = 10;
    public int magazineSize = 10; //сколько патронов помещяется в магазин.
    public int bulletSplash = 1;//количество патронов которые вылетают при 1 выстреле. 
    public float fireRate = 10f;
    public float bulletSpeed = 12f;
    public float pushbackPower = 0; //сила отбрасывания врага
    public float moneyBonus = 1;//100% начально + 10% за ап
    public float reloadTime = 0.25f;

    [Space(10)]
    [Header("Abilities Param")]
    public int grenadeDamage = 100;
    public float grenadeCD = 20f;
    public float healPower = 0.5f; //50%
    public float healCD = 30f; //30sec
    public float freezeTime = 2f;//2sec
    public float freezeCD = 30f;


    [Space(10)]
    [Header("Upgrates Level")]
    public int maxUpgradeLvl = 5; //
    public int magazineSizeLvl = 0; //время остывания оружия
    public int damageLvl = 0;
    public int pushbackPowerLvl = 0; //сила отбрасывания врага
    public int moneyBonusLvl = 0;
    public int armorLvl = 0;//увеличивает максимальное количество хп
    public int bulletSplashLvl = 0;

    public int grenadeDamageLvl = 0;
    public int healCDLvl = 0;
    public int freezeCDLvl = 0;

    [Space(10)]
    [Header("Other Param")]
    public int score = 0;
    public int money = 0;
    public int waveCount = 0;
    public bool isExitGame = false;//если игрок вышел из уровня до его завершения.
    public bool levelEnd=false;
    public float soundVolume = 1f;
    public float musicVolume = 1f;
    public string currentFileName;

    public int levelEndMoneyDrop =0;
    public int levelEndHealthLeft=0;
    public int levelEndEnemyKilled = 0;
    public float levelEndTimeLeft = 0f;

    public void CheckMaxUpLevel()
    {
        if (waveCount >= 15)
            maxUpgradeLvl = 8;
        if (waveCount >= 30)
            maxUpgradeLvl = 10;
    }
    public void ToDefault()
    {
        maxHeals = 100;
        damage = 10;
        magazineSize = 10;
        bulletSplash = 1;
        fireRate = 10f;
        bulletSpeed = 12f;
        pushbackPower = 0;
        moneyBonus = 1;
        reloadTime = 0.25f;

        grenadeDamage = 100;
        grenadeCD = 20f;
        healPower = 0.5f;
        healCD = 30f;
        freezeTime = 2f;
        freezeCD = 30f;
        isExitGame = false;
        levelEnd = false;

        maxUpgradeLvl = 5;
        magazineSizeLvl = 0; 
        damageLvl = 0;
        pushbackPowerLvl = 0; 
        moneyBonusLvl = 0;
        armorLvl = 0;
        bulletSplashLvl = 0;

        grenadeDamageLvl = 0;
        healCDLvl = 0;
        freezeCDLvl = 0;

        score = 0;
        money = 0;
        waveCount = 0;
        soundVolume = 1f;
        musicVolume = 1f;
        currentFileName = null;

        RefreshLevelEndStat();
    }
    public void RefreshLevelEndStat()
    {
        levelEndMoneyDrop = 0;
        levelEndHealthLeft = 0;
        levelEndEnemyKilled = 0;
        levelEndTimeLeft = 0f;
    }
}       
