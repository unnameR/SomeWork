using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooter : MonoBehaviour {

    public Rigidbody2D bullet;
    public Transform shotingPoint;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    

    public float bulletSpeed = 10f;
    public float fireRate = 10f;
    public float range = 100;
    public float reloadTime = 3f;

    public int damade = 10;
    public int maxAmmo = 100; //максимальный боезапас
    public int magazineAmmo = 20; //потронов в магазине

    public State ammoType;

    [HideInInspector]
    public int currentAmmo;
    [HideInInspector]
    public int ammoLeft;       //всего патронов в оружии осталось

    public bool infiniteAmmo;
    public bool semiAuto;

    private GameObject noAmmoImg;
    private GameObject gunHandler;
    private PlayerController plc;
    private AudioSource asourse;

    private float nextShoot;
    

    private bool reloading;

	void Start () {
        asourse = GetComponent<AudioSource>();
        noAmmoImg = GameObject.Find("NotificationImg");
        gunHandler = GameObject.Find("GunHandler");
        plc = transform.root.GetComponentInChildren<PlayerController>();
        ammoLeft = maxAmmo;
        currentAmmo = magazineAmmo;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (reloading)
            return;
        if (semiAuto)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextShoot)
            {
                if (currentAmmo <= 0)
                {
                    if (ammoLeft <= 0)
                    {
                        noAmmoImg.GetComponent<Animator>().Play("NoammoAnim");
                        return;
                    }

                    StartCoroutine(Reload());
                    return;
                }
                nextShoot = Time.time + 1 / fireRate;
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextShoot)
            {
                if (currentAmmo <= 0)
                {
                    if (ammoLeft <= 0)
                    {
                        noAmmoImg.GetComponent<Animator>().Play("NoammoAnim");
                        return;
                    }

                    StartCoroutine(Reload());
                    return;
                }
                nextShoot = Time.time + 1 / fireRate;
                Shoot();
            }
        }
        if (Input.GetKey(KeyCode.R))
        {
            if (currentAmmo < magazineAmmo)
                StartCoroutine(Reload());
        }
	}
    void Shoot()
    {//переделать. Поставить правильный угол пули и стрельбы
        Vector2 targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.parent.position;
        //float angle = Vector2.Angle(Vector2.right, (target - shotingPoint.position));
        float angle = Mathf.Atan2(targetDir.x, targetDir.y) * Mathf.Rad2Deg;

        Rigidbody2D bulletinst = Instantiate(bullet, shotingPoint.position, Quaternion.Euler(0, 0, -angle)) as Rigidbody2D;
        bulletinst.GetComponent<Bullet>().SetDamage(damade);
        //bulletinst.transform.LookAt(targetDir, bulletinst.transform.right);
        
        bulletinst.velocity = bulletSpeed * targetDir.normalized;

        asourse.PlayOneShot(fireSound);
        currentAmmo--;
    }
    IEnumerator Reload()
    {

        noAmmoImg.GetComponent<Animator>().SetBool("Reloading",true);
        //asourse.clip = reloadSound;
        
        asourse.PlayOneShot(reloadSound);
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        if ((currentAmmo+ammoLeft) - magazineAmmo >= 0)
        {
            if (!infiniteAmmo)
                ammoLeft -= magazineAmmo - currentAmmo;
            currentAmmo = magazineAmmo;
        }
        else
        {
            currentAmmo = ammoLeft + currentAmmo;
            ammoLeft = 0;
        }
        reloading = false;
        noAmmoImg.GetComponent<Animator>().SetBool("Reloading", false);
    }
}
public enum State
{
    pistol, shotgun, semiauto, rifle, minigun
}