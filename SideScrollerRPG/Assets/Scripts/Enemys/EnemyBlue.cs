using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlue : EnemyBase
{

    //Синий. Подходит немного, делает выстрел в 4 направления(типо дробовик), подходит еще немного, еще выстрел. 
    //И так до своей конечной точки. Наводится на игрока.

    int bulletSplash = 4;//4 патрона вылетает одновременно.
    float nextShoot;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        EnemyHeals.enemyDeathEvent += BlueDie;
    }
    void Update()
    {
        AimToPlayer();
        if (canShoot && Time.time >= nextShoot)
        {
            nextShoot = Time.fixedTime + enemyParam.fireRate;

            float stepAngleSize = (1f / bulletSplash) + bulletSplash / 2;

            for (int i = 0; i < bulletSplash; i++)
            {
                float angle = (-gunHandle.localEulerAngles.z + 90 - stepAngleSize / 2 + (bulletSplash * stepAngleSize / 2)) - (stepAngleSize * (i));

                Shoot(DirFromAngle(angle, true).normalized);
            }
        }
    }

    Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
            angleInDegrees -= gunHandle.localEulerAngles.z - 90;
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    void BlueDie()
    {
        EnemyHeals.enemyDeathEvent -= BlueDie;

        AchievementController._internal.AdjustAchievement("Blue Killed", 1);
    }
    void OnDisable()
    {
        EnemyHeals.enemyDeathEvent -= BlueDie;
    }
}
