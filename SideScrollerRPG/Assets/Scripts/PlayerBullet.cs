using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet 
{
    private float pushbackPower;

    public void PlayerInit(int dmg, float s, float pb, Vector2 dir)//, Sprite sprite)
    {
        damage = dmg;
        speed = s;
        pushbackPower = pb;
        lookDir = dir;
        //sr.sprite = sprite;

    }
    void Update()
    {
        BullerMove();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!isAlive)
            return;

        //Добавить анимацию взрыва

        //не успевает проиграть музыку так как обьект уничтожается раньше
        //aSource.PlayOneShot(explosionSounds[Random.Range(0, explosionSounds.Length)]);

        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<IPushback>().Pushback(pushbackPower);
            col.transform.GetComponent<Heals>().TakeDamage(damage);

            AchievementController._internal.AdjustAchievement("Damage Dealt", damage);

            if (pool.pool != null)
                pool.pool.ReturnObject(this.gameObject);
            else Destroy(this.gameObject);

            isAlive = false;
        }
    }
}
