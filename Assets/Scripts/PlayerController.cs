using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public float respawnTime = 2f;

    public Vector2 stopMoveRectDown;
    public Vector2 stopMoveRectUp;

    [HideInInspector]
    public bool IsRespawned;

    private float velocityX;
    private float velocityY;
    private float moveSpeed; 

    private GameManager gManager;
    private AudioSource aSourse;
    private Animator anim;
    private Rigidbody2D rb;
    private Shield shield;
    private Shooting shoot;
	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        shield = GetComponent<Shield>();
        shoot = GetComponent<Shooting>();

        anim = GetComponent<Animator>();
        aSourse = GetComponent<AudioSource>();

        gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

	}
    void Start()
    {
        moveSpeed = GameManager.moveSpeed;
        shoot.fireRate = GameManager.fireRate;
        if (IsRespawned)
        {
            StartCoroutine(Respawn());
        }
    }
	// Update is called once per frame
	void Update () {

        velocityX = Input.GetAxis("Horizontal");
        velocityY = Input.GetAxis("Vertical");

        anim.SetFloat("velocityX",velocityX);
        anim.SetFloat("velocityY", velocityY);

        if (Input.GetButton("Fire1"))
        {
            shoot.Fire();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            if (GameManager.shieldCount > 0)
            {
                shield.ShieldUp();
            }
        } 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GameManager.pillCount > 0)
            {
                gManager.ActiveSlowMo();
            }
        }
	}
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(velocityX, velocityY) * moveSpeed;
        //if (rb.position.x >= stopMoveRectDown.x && rb.position.y >= stopMoveRectDown.y && rb.position.x <= stopMoveRectUp.x && rb.position.y <= stopMoveRectUp.y)
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //гавнецо
        if (col.tag == "meteor" || col.tag == "Enemy")
        {
            TakeDamage();
            Destroy(col.gameObject);
        }
        if (col.tag == "Bonus")
        {
            col.GetComponent<GameBonus>().TakeBonus();
        }
    }
    IEnumerator Respawn()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        anim.SetBool("respawn",true);
        yield return new WaitForSeconds(respawnTime);
        GetComponent<CircleCollider2D>().enabled = true;
        anim.SetBool("respawn", false);
    }
    IEnumerator Active(float duration)//Увеличивает скорость передвижения.
    {
        moveSpeed = GameManager.moveSpeed * 2;
        yield return new WaitForSeconds(duration);
        moveSpeed = GameManager.moveSpeed;
    }
    public void ActiveBonus(float duration)
    {
        StartCoroutine("Active", duration);
    }
    public void TakeDamage()
    {
        if (shield.IsShieldDown) 
            Die();
        else shield.DamageToShield();        
    }
    void Die()
    {
        gManager.PlayerDie();
        Destroy(gameObject);
    }
}
