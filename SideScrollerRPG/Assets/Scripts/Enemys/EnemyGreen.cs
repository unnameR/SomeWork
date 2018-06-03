using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGreen : EnemyBase
{
    //Зелёный. Подходит немного, делает выстрел, подходит еще немного, еще выстрел. И так до своей конечной точки
    //Наводится на игрока. Есть текущая позиция бота, есть конечная точка. Путь делится на несколько частей, 
    //бот идёт по одной части делает выстрел, потом начинает ити по сл части, и так до конца пути.


    float nextShoot;

    protected override void OnEnable()
    {
        base.OnEnable();
        EnemyHeals.enemyDeathEvent += GreenDie;
    }
    void Update()
    {
        AimToPlayer();
        if (canShoot &&Time.time >= nextShoot)
        {
            nextShoot = Time.fixedTime + enemyParam.fireRate;
            Shoot(targetDirection.normalized);
        }
    }
    void GreenDie()
    {
        EnemyHeals.enemyDeathEvent -= GreenDie;

        AchievementController._internal.AdjustAchievement("Green Killed", 1);
    }
    void OnDisable()
    {
        EnemyHeals.enemyDeathEvent -= GreenDie;
    }
}
