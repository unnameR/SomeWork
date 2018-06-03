using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrey : EnemyBase
{
    //Серый. Подходит впритык, делает выстрел пока не умрёт. Наводится на игрока. Пули пробивают окоп.

    float nextShoot;

    protected override void OnEnable()
    {
        RefreshFlags();
        this.RebuildWay();

        EnemyHeals.enemyDeathEvent += GreyDie;
    }
    private void RebuildWay()
    {
        Vector2 moveto = GetShortestEndPos(GameObject.FindGameObjectWithTag("GreyEndPoints").transform);

        moveto = new Vector2(moveto.x + Random.Range(-OFFSET, OFFSET), moveto.y + Random.Range(-OFFSET, OFFSET));//offset
        way = BuildWay(transform.position, moveto, enemyParam.pathCount);
        canMove = true;
    }
    private Vector2 GetShortestEndPos(Transform endObjArr)
    {
        float d = 999;
        Vector2 end = new Vector2();
        foreach (Transform item in endObjArr)
        {
            float dist = Vector2.Distance(item.position, transform.position);
            if (dist <= d)
            {
                d = dist;
                end = item.position;
            }
        }
        return end;
    }
    void Update()
    {
        AimToPlayer();
        if (canShoot && Time.time >= nextShoot)
        {
            nextShoot = Time.fixedTime + enemyParam.fireRate;
            Shoot(player.position);//пуля летит по параболической траектории и пробивает врага когда он сидит.
        }
    }
    protected override void Shoot(Vector2 target)
    {
        AudioManager._instance.PlaySoundEffect(shootSoundName);
        GameObject blet = pool.GetObject(bullet);
        blet.transform.position = gunHandle.GetChild(0).position;//точка вылета пули.
        blet.transform.rotation = gunHandle.rotation;
        blet.SetActive(true);
        blet.GetComponent<EnemyBullet_Roket>().EnemyRoketInit(enemyParam.damage, enemyParam.bulletSpeed, target);
    }
    void GreyDie()
    {
        EnemyHeals.enemyDeathEvent -= GreyDie;

        AchievementController._internal.AdjustAchievement("Grey Killed", 1);
    }
    void OnDisable()
    {
        EnemyHeals.enemyDeathEvent -= GreyDie;
    }
}
