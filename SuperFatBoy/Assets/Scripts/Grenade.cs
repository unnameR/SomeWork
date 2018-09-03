using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    public float explosionTime;
    public float explosionRadius;
    public LayerMask mask;
    public LayerMask explosionMask;

    public Animator anim;

    CircleCollider2D collider;

    void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
    }
    void OnEnable()
    {
        StartCoroutine(Explode());
    }
    void Update()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, collider.radius, mask);
        if (coll != null && coll.tag == "Player")//нужно просимулировать отскоки
        {
            //coll.GetComponent<Controller2D>().collisions.hitEnemy = true;

            Destroy(gameObject);
            //anim.SetTrigger("boom");
        }
    }
    IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionTime);
        //anim.SetTrigger("boom");
        Collider2D coll = Physics2D.OverlapCircle(transform.position, explosionRadius, explosionMask);
        if (coll != null && coll.tag == "Player")
        {
            coll.GetComponent<Controller2D>().collisions.hitEnemy = true;
            Destroy(gameObject);
        }
        //уничтожить обьект после конца анимации
        Destroy(gameObject);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
