using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {

    public Transform[] zombieSpawnPoints;
    public GameObject[] zombiesPref;

    private float spawnTime;
    private float currentTime;
    private float nightTime;

    private bool stopSpawn=true;

    private int[] wave;


    int zombiesPower = 0;
    int zombiesCount = 0;
	// Use this for initialization
	void Start () 
    {
        wave=new int[4];
        wave[0] = 4; //первая волна будет 4 слабых мобa
        nightTime = GameManager.nightTimer-10;
        spawnTime = nightTime / CaclulateZombies();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (GameManager.isNight)
        {
            if (Time.time >= currentTime)
            {
                stopSpawn = false;
                currentTime = Time.time + spawnTime;

                if (zombiesPower<=wave.Length-1 && wave[zombiesPower] != null && wave[zombiesPower] > zombiesCount)
                {
                    Spawn(zombiesPower);
                    zombiesCount++;
                }
                else// some errors
                {
                    zombiesPower++;
                    zombiesCount = 0;
                    if (zombiesPower <= wave.Length - 1 && wave[zombiesPower] != null && wave[zombiesPower] > 0)
                    {
                        Spawn(zombiesPower);
                        zombiesCount++;
                    }
                }
            }
        }
        else
        {
            if (!stopSpawn)
            {
                zombiesCount = 0;
                zombiesPower = 0;
                currentTime = 0;
                CreateWave(GameManager.currentDay);
                spawnTime = nightTime / CaclulateZombies();
                stopSpawn = true;
            }
        }
	}
    void Spawn(int zombPower)
    {
        //сделать мощь волны, которая будет увеличиваться с каждым днём, чем больше мощ, 
        //тем сильнее мобы будут спауниться и в большем количестве

        //слабый моб - 100%-%+currentDay
        //средний моб - 0%+2х%+currentDay
        //сильный моб - 0%+1х%+currentDay
        //количество - n*currentDay

        //фигня
        //мобы спаунятся волнами, в каждой волне чётко прописано сколько и каких мобов должно создаться 
        //количество * currentDay;
        //сначала создаються слабые, потом средние и под конец сильные мобы.
        
        //Нужно создать масив мобов, в котором прописать сколько и каких мобов будет создано
        //потом по масиву спаунить мобов: wave[index]=count, индекс указывает на силу моба, count - количество мобов

        //Всю волну нужно заспаунить за одну ночь, то есть надо регулировать время создания.

        Transform spawnPoint = zombieSpawnPoints[Random.Range(0, zombieSpawnPoints.Length)]; //transform.GetChild(Random.Range(0, zombieSpawnPoints.Length));
        GameObject zombie = Instantiate(zombiesPref[zombPower], spawnPoint.position, spawnPoint.rotation);
        Vector3 theScale = zombie.transform.localScale;
        theScale.x *= spawnPoint.localScale.x;
        zombie.transform.localScale = theScale;
    }
    void CreateWave(int day)
    {
        //с каждым днём увеличиваеться количество спаунящихся мобов на 1, которое зависит от количества мобов 
        ///в прошлый день. Так же с каждым днём открываються новый вид мобов, но не более 5-ти видов 
        if (day >= wave.Length - 1)
            day = wave.Length - 1;

        for (int i = 0; i < day; i++)
        {
            int c = wave[i] + 1;
            wave[i] = c;
        }
    }
    private int CaclulateZombies()
    {
        int count=0;
        for (int i = 0; i < wave.Length-1; i++)
        {
            count += wave[i];
        }
        return count;
    }
    /*public void StartSpawnZombies()
    {
        stopSpawn = false;
    }*/
}
