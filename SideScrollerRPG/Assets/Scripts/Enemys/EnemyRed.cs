using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRed : EnemyBase
{
    //Красный. Подходит немного, делает серию выстрелов, подходит еще немного, еще выстрел. 
    //конечная точка должна совпадать с 1-й из 3-х точек позиций игрока

    float nextShoot;
    int shootSeries = 15;

    protected override void OnEnable()
    {
        RefreshFlags();
        RebuildWay();
        EnemyHeals.enemyDeathEvent += RedDie;
    }
    private void RebuildWay()
    {
        Vector2 moveto = GetShortestEndPos(GameObject.FindGameObjectWithTag("RedEndPoints").transform);

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
        if (canShoot && Time.time >= nextShoot)
        {
            nextShoot = Time.fixedTime + enemyParam.fireRate + 0.1f * shootSeries;
            StartCoroutine(Series());
        }
    }
    IEnumerator Series()
    {
        for (int i = 0; i < shootSeries; i++)
        {
            Shoot(Vector2.left);
            yield return new WaitForSeconds(0.1f);
        }
    }
    void RedDie()
    {
        EnemyHeals.enemyDeathEvent -= RedDie;

        AchievementController._internal.AdjustAchievement("Red Killed", 1);
    }
    void OnDisable()
    {
        EnemyHeals.enemyDeathEvent -= RedDie;
    }
}
