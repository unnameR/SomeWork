using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    /// <summary>
    /// waveCount влияет на тип войск и количество юнитов которое может заспаунится.
    /// 
    /// Типы юнитов которые могут заспаунится на уровне.
    /// События которые могут возникнуть на уровне. Типо спаун сразу 5 юнитов или ящик с рокетами.
    /// -Минимальное количество событий на уровень. Зависит от waveCount
    /// </summary>

    /// <summary>
    /// Есть 6 типов юнитов. Есть готовый набор юнитов разных типов
    /// Спаунится могут как одиночные юниты тип которых не выше waveCount, так и наборы юнитов, тип которых
    /// не выше waveCount.
    /// Каждую 5-й уровень, в конце спаунится увеличеный пак юнитов.
    /// Каждый 10-й вконце - босс.
    /// 
    /// 1-й тип юнитов открывается на 1-й волне(зелёные)
    /// 2-й тип -на 2-й (серые)
    /// 3-й тип - 6-й волна(синие)
    /// 4-й тип - 9-й волна(красные)
    /// 5-й тип - 10-11-й фолне, с босом или после.(оранжевые)
    /// 6-й тип - 16-й волна(фиолет)
    /// </summary>
    /// 

    public static int enemySpawned = 0;

    public Slider waveSlider;
    public PlayerSO playerParam;
    public SimpleObjectPool pool;
    public WaveSO[] allTypeOfWaves; // готовый набор волн
    public GameObject[] enemyTypes; // типы юнитов

    public float waveCoooldownTime;
    public float startWait;

    private int maxUnitOnField = 10;
    private int currentUnitOnField;
    private int maxUnitPerWave = 20;

    Vector2 spawnPointMin;
    Vector2 spawnPointMax;

	void Start ()
    {
        maxUnitOnField = (playerParam.waveCount + 5 <= 20) ? playerParam.waveCount + 5 : playerParam.waveCount;//
        maxUnitPerWave = Random.Range(playerParam.waveCount + 1 * 15, playerParam.waveCount + 2 * 10);//
        enemySpawned = 0;
        spawnPointMin = new Vector2(transform.position.x - (transform.localScale.x / 2), transform.position.y - (transform.localScale.y / 2));
        spawnPointMax = new Vector2(transform.position.x + (transform.localScale.x / 2), transform.position.y + (transform.localScale.y / 2));
        
        EnemyHeals.enemyDeathEvent += EnemyDie;
        StartCoroutine(WaveSpawnLoop());
	}
    IEnumerator WaveSpawnLoop()
    {
        yield return new WaitForSeconds(startWait);
        while (enemySpawned < maxUnitPerWave)
        {
            SpawnOnce();
            if (enemySpawned >= maxUnitPerWave)
                GameController._waveEnd = true;

            yield return new WaitForSeconds(waveCoooldownTime);
        }

        
    }
    void EnemyDie()
    {
        currentUnitOnField = (currentUnitOnField > 0) ? --currentUnitOnField : 0;
    }
    void SpawnOnce()
    {
        if (currentUnitOnField < maxUnitOnField)
        {
            WaveSO wave = GetLevelEvent();
            if (wave == null)
            {
                GameObject[] objstoSpawn = System.Array.FindAll<GameObject>(enemyTypes, unit => unit.GetComponent<EnemyBase>().enemyParam.minWaveToSpawn <= playerParam.waveCount);
                int randomUnits = Random.Range(1, 3);//спаунить рандомно от 1 до 2-х юнитов
                for (int i = 0; i < randomUnits; i++)
                {
                    SpawnUnit(objstoSpawn[Random.Range(0, objstoSpawn.Length)]);//с большей вероятностю должны спаунится начальные враги и с меньшей сложные.
                }
            }
            else
            {
                for (int i = 0; i < wave.enemyCount; i++)
                {
                    SpawnUnit(wave.enemyGO);//желательно что бы вся волна считалась за 1-го юнита.
                }
            }
        }
        waveSlider.value = (float)enemySpawned / maxUnitPerWave;
    }
    WaveSO GetLevelEvent()//
    {
        foreach (WaveSO wave in allTypeOfWaves)
        {
            if (wave.IsEventTriggered(playerParam.waveCount))
                return wave;
        }
        return null;
    }
    void SpawnUnit(GameObject unitGO)
    {
        GameObject enemy = pool.GetObject(unitGO);
        enemy.transform.position = new Vector2(Random.Range(spawnPointMin.x, spawnPointMax.x), Random.Range(spawnPointMin.y, spawnPointMax.y));
        enemy.SetActive(true);
        enemySpawned++;
        currentUnitOnField++;
    }
    void OnDisable()
    {
        EnemyHeals.enemyDeathEvent -= EnemyDie;
    }
}
