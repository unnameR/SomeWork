using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private float damage;
    private float destrTime = 2f;

    public void SetDamage(float dmg) { damage = dmg; }
    void Start()
    {
        Destroy(gameObject, destrTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="Enamy")
        {
            col.transform.GetComponent<Heals>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
