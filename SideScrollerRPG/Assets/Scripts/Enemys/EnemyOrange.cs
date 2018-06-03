using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrange : EnemyBase
{
    //Оранжевый. Камикадзе. Бежит прямо на игрока и взрывается.
    public string runSoundEffect;

    protected override void OnEnable()
    {
        RefreshFlags();
        EnemyHeals.enemyDeathEvent += OrangeDie;
        StartCoroutine(Run());

        //AudioManager._instance.PlaySoundEffect(runSoundEffect);
    }
    IEnumerator Run()
    {
        while (true)//гавнецо
        {
            if (transform.position.x == player.position.x)
            {
                //explosion
                player.GetComponent<Heals>().TakeDamage(enemyParam.damage);
                //OrangeDie();
                //Destroy(gameObject);
                GetComponent<EnemyHeals>().IsDead = true;
                yield break;
            }
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemyParam.moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
    void OrangeDie()
    {
        EnemyHeals.enemyDeathEvent -= OrangeDie;
        //AudioManager._instance.StopPlay(runSoundEffect);
        AchievementController._internal.AdjustAchievement("Orange Killed", 1);
    }
    void OnDisable()
    {
        EnemyHeals.enemyDeathEvent -= OrangeDie;
    }
}
