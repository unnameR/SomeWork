using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{

    public void EnemyInit(int dmg, float s, Vector2 dir)//, Sprite sprite)
    {
        damage = dmg;
        speed = s;
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

        if (col.gameObject.tag == "Player")
        {
            if (!PlayerController.isInThePit)
            {
                col.transform.GetComponent<Heals>().TakeDamage(damage); 
                
                if (pool.pool != null)
                    pool.pool.ReturnObject(this.gameObject);
                else Destroy(this.gameObject);

                isAlive = false;
            }
        }
    }
}
