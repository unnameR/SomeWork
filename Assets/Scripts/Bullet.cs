using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [HideInInspector]
    public float speed=10;
    public bool playerShoot;

    public Sprite hitSprite;
    private Rigidbody2D rb;
	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.velocity = Vector2.up * speed;
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && !playerShoot)
        {
            col.GetComponent<PlayerController>().TakeDamage();
            GetComponent<SpriteRenderer>().sprite = hitSprite;
            Destroy(gameObject, 0.02f);
        }
        if (col.tag == "Enemy" && playerShoot)
        {
            col.GetComponent<Enemy>().TakeDamage();
            GetComponent<SpriteRenderer>().sprite = hitSprite;
            Destroy(gameObject, 0.02f);
        }
        /*else //попадения по метеорам. Тогда игрок сможет укрытся за метеорами от стральбы бота
        {
            GetComponent<SpriteRenderer>().sprite = hitSprite;
            Destroy(gameObject, 0.02f);
        }*/
        if (col.tag == "meteor" && playerShoot)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprite;
            Destroy(gameObject, 0.02f);
        }
        if (col.tag == "Wall")
        {
            Destroy(gameObject);
        }
            
    }
}
