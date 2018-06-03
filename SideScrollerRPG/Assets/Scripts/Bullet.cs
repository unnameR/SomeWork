using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public PooledObject pool;
    public float destrTime = 2;
    public AudioClip[] explosionSounds;

    private AudioSource aSource;
    protected Rigidbody2D rb2d;
    private SpriteRenderer sr;
    protected int damage;
    protected float speed;

    protected Vector2 lookDir;

    private float age;
    protected bool isAlive = true;
    
    void OnEnable()
    {
        isAlive = true;
        age = 0;
    }
    void Awake()
    {
        aSource = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    protected void BullerMove()
    {
        age += Time.deltaTime;
        if (age > destrTime)
        {
            if (pool.pool != null)
                pool.pool.ReturnObject(this.gameObject);
            else Destroy(this.gameObject);
        }

        rb2d.velocity = lookDir * speed;
    }
}
