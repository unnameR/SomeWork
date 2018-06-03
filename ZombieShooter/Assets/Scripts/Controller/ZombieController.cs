using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public int damage = 10;
    public int zombPower=0;
    public float speed = 5f;
    public float attackSpeed = 0.5f;
    public bool faceingRight;

    public GameObject DropGo;
    public Rigidbody2D rb;
    public Animator anim;
    public AudioClip attackSound;
    //private int animControl = 0;//0-idle, 1,2,3,4.. activate animations

    private GameObject[] targets = new GameObject[3];
    private GameObject target;
    private AudioSource asourse;
    Vector3 faceing;
    private float distanceforTarget = 4f;
    private bool attacking;
    private bool spawned;
    private bool dead;
    
	// Use this for initialization
	void Start () {

        damage *= GameManager.currentDay;

        asourse = GetComponent<AudioSource>();

        targets[0] = GameObject.FindGameObjectWithTag("Player");
        targets[1] = GameObject.FindGameObjectWithTag("House");
        targets[2] = GameObject.FindGameObjectWithTag("Turret");

        if (transform.localScale.x >= 0)
            faceing = transform.right;
        else faceing = -transform.right;
	}

    void Update()
    {
        dead = GetComponent<Heals>().Dead;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, faceing, distanceforTarget);
        if (hit != null)
        {
            foreach (GameObject t in targets)
            {
                if (t != null && hit.transform == t.transform)
                {
                    //anim.SetBool("run", true);
                    speed = 6;
                }
            }
        }
        if (!spawned)
            if (dead)
            {
                GameManager.score += 10 * (GameManager.currentDay + zombPower);
                SpawnDrop();
                attacking = false;
                
            }
    }
    void FixedUpdate()
    {
        if (!attacking && !dead)
        {
            anim.SetFloat("speed", speed);
            rb.velocity = new Vector2(transform.localScale.x * speed, rb.velocity.y);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        foreach (GameObject t in targets)
        {
            if (t != null && col.tag == t.tag)
            {
                attacking = true;
                anim.SetBool("attack", true);
                target = t;
                InvokeRepeating("Attack",0,attackSpeed);
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        anim.SetBool("attack", false);
        attacking = false;
    }
    void Attack()
    {
        if (attacking && !dead && target != null)
        {
            asourse.PlayOneShot(attackSound);
            target.GetComponent<Heals>().TakeDamage(damage);
        }
    }
    void SpawnDrop()
    {
        GameObject go = Instantiate(DropGo, transform.position, transform.rotation);
        spawned = true;
    }
}
