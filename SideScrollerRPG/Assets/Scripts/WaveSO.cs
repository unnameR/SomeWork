using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave")]
public class WaveSO : ScriptableObject
{
    public float triggerChance = 0.1f; //шанс срабатывания
    //public float repeatTime = 0.5f; //время спауна всех обьктов
    public int enemyCount = 5;//количество юнитов которые заспаунятся
    public EnemySO enemy;
    public GameObject enemyGO;

    public bool IsEventTriggered(int waveCount)
    {
        if (waveCount < enemy.minWaveToSpawn)
            return false;

        float r = Random.Range(0.0f, 1.0f);
        return r <= triggerChance;
    }
}
