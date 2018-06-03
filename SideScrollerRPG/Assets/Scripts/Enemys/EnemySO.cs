using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public int maxHeals = 100;
    public int damage = 10;
    public float fireRate = 3; //секунд между выстрелами
    public float bulletSpeed = 100f;
    public float moveSpeed = 2f;

    public int moneyForKill=10;
    public int pathCount = 3;
    public int minWaveToSpawn = 1;
    //public float chanceToSpawn = 0.5f;//шанс спауна моба в спорной ситуации.
    public float endDistance = 5;

    public void LoadStats(int waveCount)
    {
        maxHeals = 100 + waveCount * 3;
    }
}
