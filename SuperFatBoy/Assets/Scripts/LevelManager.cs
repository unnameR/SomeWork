using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;

    public GameParamSO gameParam;
    public PlayerSpawner ps;
    public AwardsController awardsController;
    public LevelUI levelUI;
    public WastedMenuUI wastedMenu;
    public LevelCompleteMenu levelCompleteMenu;

    public SoundSO hitExitSound;
    public int spawnCount = 3;
    public int respawnBayCount=1;
    //int attempt;
    float currentTime;
    bool isSecretFind;
    bool stopTimer=true;
    GameLevelSO nextlevel;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);
        
        Player.deathEvent += PlayerDie;
        currentTime = 0;
        gameParam.currentLevel.attempts = 1;
        levelUI.SetControllUISize(gameParam.controllSize);
    }
    void Start()
    {
        isSecretFind = false;
        Instantiate(gameParam.currentLevel.levelPrefab);
        Transform spawn= GameObject.FindGameObjectWithTag("Respawn").transform;
        if(spawn!=null)
            ps.SetStartSpawnPoint(spawn);
        //на всю главу 1 саундтре. Он заканчивается, начинается следущий
        //AudioManager._instance.PlaySoundEffect(gameParam.currentChapter.chapterMainSound);
        if (gameParam.currentLevel.levelName != "BOSS")//привязка к имени - гавнецо.
        {
            nextlevel = gameParam.currentChapter.levels[gameParam.currentLevel.levelN];
        }
        else nextlevel = gameParam.currentLevel;
    }
    void Update()
    {
        if (!stopTimer)
        {
            currentTime += Time.deltaTime;
            levelUI.UpdateTimer(currentTime);
        }
    }
    public void StartTimer()
    {
        stopTimer = false;
    }
    public float GetTime()
    {
        return currentTime;
    }
    public void SecretFind(bool find)
    {
        isSecretFind = find;
    }
    public bool GetSecter()
    {
       return gameParam.currentLevel.medalS;
    }
    public void RestartLevel()
    {
        StartCoroutine(Restart());
    }
    public void NextLevel()
    {
        if (gameParam.currentLevel.levelName == "BOSS")//привязка к имени - гавнецо.
        {
            levelUI.ChapterComplete();
            return;
        }
        if (nextlevel.levelName == "BOSS")//если следущий уровень боссо - выйти в текущую главу
        {
            BackToMainMenu();//доделать
        }
        gameParam.currentLevel = nextlevel;
        
        SceneManager.LoadScene(1);
    }
    public void PauseGame(bool isPause)
    {
        Time.timeScale = isPause ? 0 : 1;
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void SetCheckpoint(Transform point)
    {
        ps.SetCurrentSpawnPoint(point);
    }
    public void HitExit()
    {
        //win level
        //Записывать данные об уровне.
        bool medal = false;
        bool secret = false;

        AudioManager._instance.PlaySoundEffect(hitExitSound);
        stopTimer = true;

        gameParam.currentLevel.isLevelComplete = true;//Возможно стоит добавлять прогресс главы тут, а не потом делать 3 цикла.
        //gameParam.currentChapter.progress++;
        
        if (!gameParam.currentLevel.medalS && isSecretFind)
        {
            awardsController.SetAward("FIRST SECRET");
            awardsController.SetAward("SPEED");
            gameParam.currentLevel.medalS = true;
            secret = true;
            gameParam.playerMoney += 4;
            //gameParam.currentChapter.progress++;
        }
        if (!gameParam.currentLevel.medalM && currentTime <= gameParam.currentLevel.forMedalTime)
        {
            awardsController.SetAward("FIRST MEDAL");
            awardsController.SetAward("EXPLORER");
            gameParam.currentLevel.medalM = true;
            medal = true;
            //gameParam.currentChapter.progress++;
        }
        gameParam.currentLevel.currentTime = currentTime;
        //gameParam.currentLevel.attempts = attempt;


        if (nextlevel.levelName != "BOSS")
            nextlevel.isLevelLock = false;

        levelUI.gameObject.SetActive(false);
        levelCompleteMenu.gameObject.SetActive(true);//
        levelCompleteMenu.LevelComplete(medal, secret);
        //levelUI.LevelComplete();
    }
    public void Respawn()
    {
        //StartCoroutine(Restart());//разделить респаун и рестарт уровня.
        gameParam.currentLevel.attempts++;
        ps.SpawnPlayer();
        Player.deathEvent += PlayerDie;
    }
    void PlayerDie()
    {
        stopTimer = true;
        isSecretFind = false;
        ReplayManager.instance.StopRecording();
        Player.deathEvent -= PlayerDie;
        if (ps.HaveCheckpoint()&&spawnCount > 0)//Если есть чекпоинт и у игрока есть бесплатные воскрешалки. Доделать
        {
            spawnCount--;//бесплатные воскрешалки
            Respawn();
        }
        else
        {
            wastedMenu.SetCheckpoint(ps.HaveCheckpoint());
            StartCoroutine(Wasted());
        }
    }
    
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(0.5f);
        //currentTime = 0;
        SceneManager.LoadScene(1);//перезагружаем сцену.Плохое решение..
    }
    IEnumerator Wasted()
    {
        levelUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        wastedMenu.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        Player.deathEvent -= PlayerDie;
    }
}
