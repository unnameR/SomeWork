using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet_Roket : Bullet
{
    Vector2 endPoint;
    public void EnemyRoketInit(int dmg, float s, Vector2 endP)//, Sprite sprite)
    {
        damage = dmg;
        speed = s;
        endPoint = endP;
        //sr.sprite = sprite;
    }
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
        if ((Vector2)transform.position == endPoint)
        {
            //explosion
            if (pool.pool != null)
                pool.pool.ReturnObject(this.gameObject);
            else Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!isAlive)
            return;

        //Добавить анимацию взрыва

        //не успевает проиграть музыку так как обьект уничтожается раньше
        //aSource.PlayOneShot(explosionSounds[Random.Range(0, explosionSounds.Length)]);

        if (col.gameObject.tag == "Player")
        {
            col.transform.GetComponent<Heals>().TakeDamage(damage);
            if (pool.pool != null)
                pool.pool.ReturnObject(this.gameObject);
            else Destroy(this.gameObject); 
            
            isAlive = false;
        }
    }
	
}
