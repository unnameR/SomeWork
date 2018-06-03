using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    //Bow to the God Object!!

    public static int _money;
    public static int _enemyDied;
    public static bool _waveEnd;

    public Text waveValueDisplay;
	public Text killValueDisplay;//возможно будет отображатся убитых врагов
    public Text winLoseText;
    public Text unlockUpLogoText;
	//public GameObject endLevelMenu;
    public PlayerSO playerParam;
    public EnemySO[] enemies;
    public GameObject healCast;
    public GameObject freezeCast;
    public GameObject[] nextUpgradeUIs;
    public Animator manualMenuAnim;
    public Animator letsGoAnim;
    public Animator unlockUpLogo;
    public Animator levelEndAnim;
    public Animator pauseAnim;
    public Animator enemyKillAnim;

    public string mainThemeSoundName;
    public string levelEndSoundName;
    public string explosionSoundEffectName;
    public string freezeSoundEffectName;

    void Awake()
    {
        _money = 0;
        _enemyDied = 0;
        _waveEnd = false;
        playerParam.isExitGame = false;
        Time.timeScale = 1;
        AudioManager._instance.PlaySoundEffect(mainThemeSoundName);

        AudioListener.volume = playerParam.soundVolume;
        waveValueDisplay.text = (playerParam.waveCount+1).ToString();
        killValueDisplay.text = _enemyDied.ToString();

        healCast.SetActive(playerParam.waveCount >= 5);//5 открытие хила. Сделать нормально
        freezeCast.SetActive(playerParam.waveCount >= 20);//20 открытие стана. Сделать нормально

        if (playerParam.waveCount == 0)
            ManualsMenu(true);

        PlayerHeals.deathEvent += EndGame;
        EnemyHeals.enemyDeathEvent += EnemyDie;
        playerParam.RefreshLevelEndStat();
    }
    
    public void DropGrenade()
    {
        Debug.Log("KABOOM!");
        AchievementController._internal.AdjustAchievement("Grenade used", 1);

        AudioManager._instance.PlaySoundEffect(explosionSoundEffectName);

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in allEnemies)
        {
            enemy.GetComponent<Heals>().TakeDamage(playerParam.grenadeDamage);//if(EnemyHeals!=null)
        }

        int g = 0;
        foreach (var enemy in allEnemies)//
        {
            if (enemy.GetComponent<Heals>().IsDead)//если убили врага гранатой
                g++;
        }
        if (g >= 2)
        {
            AchievementController._internal.AdjustAchievement("Kill 2 with one grenade", g);
        }
        if (g >= 5)
        {
            AchievementController._internal.AdjustAchievement("Kill 5 with one grenade", g);
        }

    }
    public void FreezeAll()
    {
        Debug.Log("freeze!");
        AudioManager._instance.PlaySoundEffect(freezeSoundEffectName);

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in allEnemies)
        {
            enemy.GetComponent<EnemyBase>().Freeze(playerParam.freezeTime);
        }
    }
    public void LetsGoAnim()
    {
        letsGoAnim.SetTrigger("play");
    }
    public void ManualsMenu(bool isOpen)
    {
        manualMenuAnim.SetTrigger((isOpen) ? "open" : "close");
        Time.timeScale = (isOpen) ? 0 : 1;
    }
    public void Pause(bool isPause)
    {
        pauseAnim.SetTrigger((isPause)?"open":"close");

        Time.timeScale = (isPause) ? 0 : 1;
    }
    public void ExitLevel()
    {
        playerParam.isExitGame = true;
        playerParam.levelEndHealthLeft = 0;
    }
    public void EnemyDie()
    {
        enemyKillAnim.SetTrigger("enemyDie");
        killValueDisplay.text = _enemyDied.ToString();
        CheckWin();
    }
    public void CheckWin()
    {
        //после того как закончился спаун мобов проверять всех ли ботов убили.
        //если да, то победа
        //Debug.LogError(_waveEnd+" "+ WaveSpawner.enemySpawned+" : "+_enemyDied);
        if (_waveEnd && WaveSpawner.enemySpawned.Equals(_enemyDied))
        {
            //если количество заспауненых ботов равно количеству убитых = победа

            StartCoroutine(DelayLvlCompleteAnim());

            playerParam.waveCount++;
            playerParam.CheckMaxUpLevel();
            EnemyIncreseMaxHeals(3);

            AchievementController._internal.AdjustAchievement("Wave Complete", 1);
            winLoseText.text = "Wave Complete";
            EndGame();
        }
    }
	public void EndGame()
	{
        AudioManager._instance.StopPlay(mainThemeSoundName);
        AudioManager._instance.PlaySoundEffect(levelEndSoundName);

        AchievementController._internal.AdjustAchievement("Enemy Killed", _enemyDied);
        AchievementController._internal.AdjustAchievement("Gold Collected", _money);

        PlayerHeals.deathEvent -= EndGame;
        EnemyHeals.enemyDeathEvent -= EnemyDie;

		Time.timeScale = 0.1f;
        SetActiveLevelCompleteIcon();

        playerParam.levelEnd = true;
        playerParam.levelEndMoneyDrop = _money;
        playerParam.levelEndEnemyKilled = _enemyDied;
        //endLevelMenu.SetActive(true);
        levelEndAnim.SetTrigger("levelEnd");
        AchievementController._internal.Init();//инициализировать показ мисий
	}
    private IEnumerator DelayLvlCompleteAnim()
    {
        int s = playerParam.waveCount % nextUpgradeUIs.Length;//5шт

        yield return new WaitForSecondsRealtime(2);
        nextUpgradeUIs[s].GetComponent<Animator>().SetTrigger("complete");//не работает возможно из за того, что меню включается после выполнения кода

        //разблокировка какого то улучшения. Нужен список всех улучшений и их уровни открытия
        if (s == 4 && playerParam.waveCount <= 30)//0,1,2,3,4. 5-й босс будет 4
        {
            yield return new WaitForSecondsRealtime(1);
            switch (playerParam.waveCount)
            {
                case 5:
                    unlockUpLogoText.text = "Health Unlock!";
                    break;
                case 10:
                    unlockUpLogoText.text = "Armor Unlock!";
                    break;
                case 15:
                    unlockUpLogoText.text = "Upgrade Limit: LVL 8!";
                    break;
                case 20:
                    unlockUpLogoText.text = "Freeze Unlock!";
                    break;
                case 25:
                    unlockUpLogoText.text = "Gun Canon Unlock!";
                    break;
                case 30:
                    unlockUpLogoText.text = "Upgrade Limit: LVL 10!";
                    break;
            }
            unlockUpLogo.SetTrigger("unlock");
        }

    }
    private void SetActiveLevelCompleteIcon()//доделать. Анимация должна запускатся только после прохождения уровня
    {
        //если игрок вышел из игры до конца уровня, то анимации нету.
        //анимация должна проверятся до того как waveCount плюсонется.
        int s = _waveEnd ? (playerParam.waveCount - 1) % nextUpgradeUIs.Length : playerParam.waveCount % nextUpgradeUIs.Length;//5шт
        //s = _waveEnd ? s - 1 : s;//если уровень пройден, значит waveCount плюсонуло, значит нужно не учитывать этот +1
        for (int i = 0; i < nextUpgradeUIs.Length; i++)
        {
            nextUpgradeUIs[i].GetComponent<Animator>().enabled = !(i < s);
            nextUpgradeUIs[i].transform.GetChild(0).gameObject.SetActive(i < s);
        }
    }
    private void EnemyIncreseMaxHeals(int value)
    {
        foreach (EnemySO enemy in enemies)
        {
            enemy.maxHeals += value;
        }
    }
    void OnDisable()
    {
        PlayerHeals.deathEvent -= EndGame;
        EnemyHeals.enemyDeathEvent -= EnemyDie;
        Time.timeScale = 1;
    }
}
