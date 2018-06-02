using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour {

    public GameObject[] enemys;
    public GameObject[] bonuses;
    public GameObject[] meteors;

    public float meteorSpawnTime = 0.5f;
    public float bonusSpawnTime = 1f;
    public float enemySpawnTime = 1f;

    public float minRange = -6f;
    public float maxRange = 6f;

    public bool stopSpawn;

	// Use this for initialization
	void Start () {
        InvokeRepeating("MeteorsSpawn", 0, meteorSpawnTime);
        InvokeRepeating("DropBonus", 0, bonusSpawnTime);
        InvokeRepeating("EnemySpawn", 0, enemySpawnTime);
	}

    void EnemySpawn()
    {
        GameObject obj = enemys[Random.Range(0, enemys.Length)];//spawnObjects[Random.Range(0, spawnObjects.Length)];
       Instantiate(obj, randomPos(), Quaternion.identity);
    }
	void MeteorsSpawn () {
        if (stopSpawn)
            return;
        GameObject obj = meteors[Random.Range(0, meteors.Length)];//spawnObjects[Random.Range(0, spawnObjects.Length)];
        Instantiate(obj, randomPos(), Quaternion.identity);
	}
    void BonusSpawn(GameObject bonus)
    {
        if (stopSpawn)
            return;
       Instantiate(bonus, randomPos(), Quaternion.identity);
    }
    Vector3 randomPos()
    {
        return new Vector3(Random.Range(minRange, maxRange), transform.position.y);
    }
    void DropBonus()
    {
        int totalChanceSum = 0;
        for (int i = 0; i < bonuses.Length;i++ )
        {
            totalChanceSum += bonuses[i].GetComponent<GameBonus>().dropChance;
        }
        GameObject bonus = GetBonus(bonuses, totalChanceSum);
        if (bonus != null)
            BonusSpawn(bonus);
        else Debug.Log("Плохой рандом");
    }
    private GameObject GetBonus(GameObject[] allBonus, int totalChance)
    {
        int r = Random.Range(0, totalChance);
        int currentSum = 0;
        foreach (GameObject item in bonuses)
        {
            GameBonus gb = item.GetComponent<GameBonus>();
            if (currentSum <= r && r < currentSum + gb.dropChance)
                return item;
            currentSum += gb.dropChance;
            //if (gb.dropChance <= chance)//гавнецо
            //  blist.Add(item);
        }
        return null;
    }
}
