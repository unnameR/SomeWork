using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public EnemyParameters enemyParam;

    public float raycastDistance = 10f;
    public float changeMoveTime = 2f;

    public Vector2 stopMoveRectDown;
    public Vector2 stopMoveRectUp;

    private float moveSpeed;
    private float velocityX;
    private float velocityY;
    public float rightPositionY;

    private bool isRightPos;
    private bool isEvasion;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Shooting shoot;
    private Shield shield;


    float t;
	// Use this for initialization
    void Awake()
    {
        shield = GetComponent<Shield>();
        shield.shieldPower = enemyParam._shieldPower;

        shoot = GetComponent<Shooting>();
        shoot.BulletSpeed = enemyParam._bulletSpeed;
        shoot.fireRate = enemyParam._fireRate;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = enemyParam._ships[0];//не тут и не так

        moveSpeed = enemyParam._moveSpeed;
        
    }
    void Start()
    {
        InvokeRepeating("Shoot",1,2); // для теста
    }
    void Update()
    {
        if (Time.time >= t)
        {
            t = Time.time + changeMoveTime;
            //if (!isEvasion)
            RandonVelocity();
        }

    }
    void FixedUpdate()
    {
        if (isRightPos)
            BotMove();
        else MoveToRightPosition();
    }
    void MoveToRightPosition()
    {
        if (rb.position.y> rightPositionY)
        {
            rb.MovePosition(rb.position + Vector2.down * moveSpeed * Time.fixedDeltaTime);
        }
        else isRightPos = true;
    }
    void BotMove()
    {
        //бот должет менять направление через определёный промижуток времени,
        //направления должны быть как вертикальные так и горизонтальные, но не заходить за определённые границы
        //если бот находит снаряд который в него летит, он пытается от него уклонится
        Vector2 movement = new Vector2(velocityX, velocityY) * moveSpeed;

        Vector2 v = rb.position + movement * Time.fixedDeltaTime;
        if (v.x >= stopMoveRectDown.x && v.y >= stopMoveRectDown.y && v.x <= stopMoveRectUp.x && v.y <= stopMoveRectUp.y)
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);

        //SearchPlayerBullets();//уклонение от пуль игрока
        SearchPlayer();
    }
    void RandonVelocity()
    {
        velocityX = Random.Range(-1f, 1f);
        velocityY = Random.Range(-1f, 1f);
    }
    void SearchPlayerBullets()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, raycastDistance);//ударяется сам в сетя
        Debug.DrawLine(rb.position, new Vector2(rb.position.x, rb.position.y - raycastDistance), Color.red);
        if (hit.collider != null)
        {
            //Debug.Log(hit.collider.name);
            Bullet b=hit.collider.GetComponent<Bullet>();//возможно стоит сделать подругому
            if (b != null)
            {
                if (b.playerShoot)
                {
                    //уклоняемся
                    //Debug.Log("Evasion");
                    RandonVelocity();
                    BotMove();
                }
            }
        }
    }
    void SearchPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, raycastDistance, enemyParam.layerMask);
        if (hit.rigidbody != null)
        {
            if (hit.rigidbody.tag == "Player")//для большей надежности
                Shoot();
        }
    }
	void Shoot () {
        shoot.Fire();
	}
    public void TakeDamage()
    {
        if (shield.IsShieldDown)
        {
            GameManager.score += enemyParam._scoreToDie;
            Destroy(gameObject);
        }
        else shield.DamageToShield();  
    }
}
