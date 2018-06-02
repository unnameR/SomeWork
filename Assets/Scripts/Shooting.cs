using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    public GameObject bullet;
    public AudioClip[] fireSounds;

    public Transform[] guns;
    [HideInInspector]
    public float fireRate=0.5f;
    public bool playerShoot;

    private float bulletSpeed = 10f;
    private AudioSource aSourse;
    private float currFireTime;
    void Awake()
    {
        aSourse = GetComponent<AudioSource>();
    }
    public void ActiveBonus(float duration)
    {
        StartCoroutine("Active", duration);
    }
    IEnumerator Active(float duration)//Увеличивает скорость атаки.
    {
        fireRate = GameManager.fireRate / 2;
        yield return new WaitForSeconds(duration);
        fireRate = GameManager.fireRate;
    }
    public void Fire()
    {
        if (Time.time >= currFireTime)
        {
            aSourse.PlayOneShot(fireSounds[Random.Range(0, fireSounds.Length)]);

            foreach (Transform gun in guns)
            {
                GameObject spawnBullet = Instantiate(bullet, gun.position, Quaternion.identity);
                Bullet b = spawnBullet.GetComponent<Bullet>();
                b.speed = bulletSpeed;
                b.playerShoot = playerShoot;
            }
            currFireTime = Time.time + fireRate;
        }
    }
    public float BulletSpeed
    {
        get { return bulletSpeed; }
        set { bulletSpeed = value; }
    }
}
