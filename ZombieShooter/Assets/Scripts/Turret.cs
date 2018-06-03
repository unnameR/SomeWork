using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public Rigidbody2D bullet;
    public Transform shotingPoint;

    public float turretMaxAngle=50f;
    public float bulletSpeed = 10f;
    public float fireRate = 1f;
    public float range = 5f;

    public int damade = 10;

    private Animator anim;
    private Vector3 direction;
    private float lastshoot;
	void Start () {
        anim = GetComponent<Animator>();
        if (transform.parent.localScale.x > 0)
            direction = -shotingPoint.transform.right;
        else direction = shotingPoint.transform.right;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(shotingPoint.transform.position, direction, range);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Enamy")
            {
                Debug.DrawLine(transform.position, hit.transform.position);
                if (Time.time >= lastshoot)
                {
                    anim.SetBool("attack", true);
                    Shoot();
                }
            }
            else anim.SetBool("attack", false);
        }
	}
    private void Shoot()
    {
        lastshoot = Time.time + fireRate;
        Rigidbody2D bulletinst = Instantiate(bullet, shotingPoint.transform.position, shotingPoint.transform.rotation) as Rigidbody2D;
        bulletinst.GetComponent<Bullet>().SetDamage(damade);
        //bulletinst.transform.rotation = Quaternion.Euler(0,0,90);
        bulletinst.velocity = direction * bulletSpeed;//new Vector2(bulletSpeed,0);
        bulletinst.transform.rotation = Quaternion.Euler(0, 0, 90);
    }
}
