using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPurple : EnemyBase
{
    //Фиолетовый. Подходит немного. Стреляет по 3-м точкам, рандомно. Пробивает игрока в окопе.

    Transform shootPoints;

    float nextShoot;

    protected override void OnEnable()
    {
        base.OnEnable();
        shootPoints = GameObject.FindGameObjectWithTag("PurpleShootPoints").transform;
        EnemyHeals.enemyDeathEvent += PurpleDie;
    }
    void Update()
    {
        if (canShoot && Time.time >= nextShoot)
        {
            nextShoot = Time.fixedTime + enemyParam.fireRate;
            GameObject shootGO = shootPoints.GetChild(Random.Range(0, shootPoints.childCount)).gameObject;
            shootGO.GetComponent<Animator>().SetTrigger("shoot");
            AudioManager._instance.PlaySoundEffect("PurpleAim");
            StartCoroutine(ShowShootPoint(shootGO.transform.position));
            //targetDirection = shootPoint - (Vector2)gunHandle.position;
            //Shoot(player.position);//пуля должна попадать в конкретные точки и там взрыватся нанося урон игроку.
        }
    }
    IEnumerator ShowShootPoint(Vector2 target)
    {
        AudioManager._instance.PlaySoundEffect("PurpleAim");
        yield return new WaitForSeconds(0.6f);
        Shoot(target);
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
    void PurpleDie()
    {
        EnemyHeals.enemyDeathEvent -= PurpleDie;

        AchievementController._internal.AdjustAchievement("Purple Killed", 1);
    }
    void OnDisable()
    {
        EnemyHeals.enemyDeathEvent -= PurpleDie;
    }
}
