using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChapter2 : MonoBehaviour {

    public GameObject grenade;
    public GameObject[] keys;
    public GameObject finalKey;
    public GameObject dieKey;
    public LineRenderer laser;
    public float moveSpeed;
    public float laserCooldown;
    public float grenadeCooldown;
    Transform player;
    LaserBeem laserBeem;

    float laserWait = 3f;
    float laserDuration = 5f;
    float nextShootTime;
    int keysDroped;
    void Awake()
    {
        PlayerSpawner.spawnEvent += SetPlayerPos;
        laserBeem = laser.GetComponent<LaserBeem>();
    }
	void OnEnable ()
    {
        keys = ShaffleArray<GameObject>(keys);	//тасуем ключи
        nextShootTime = Time.time + laserCooldown + laserWait;
        InvokeRepeating("DropGrenade", 2f, grenadeCooldown);
        Invoke("DropKey",5);
	}
	
	void Update () 
    {
        if (nextShootTime <= Time.time)
        {
            nextShootTime = Time.time + laserCooldown + laserWait + laserDuration;
            StartCoroutine(LaserShoot());
        }
        //движение
	}
    public void DropKey()
    {
        if (keysDroped < keys.Length)
        {
            KeyDrop(keys[keysDroped]);
            keysDroped++;
        }
        else
        {
            KeyDrop(finalKey);
        }
    }
    public void Die()
    {
        KeyDrop(dieKey);
        Destroy(gameObject);
    }
    void KeyDrop(GameObject key)
    {
        key.SetActive(true);
        key.transform.position = transform.position;
        key.GetComponent<Rigidbody2D>().AddForce(Vector2.one * 4, ForceMode2D.Impulse);
    }
    IEnumerator LaserShoot()
    {
        laser.enabled = true;
        yield return new WaitForSeconds(laserWait);
        laserBeem.SetActiveLaser(true);
        yield return new WaitForSeconds(laserDuration);
        laserBeem.SetActiveLaser(false);
        laser.enabled = false;
    }
    void DropGrenade()
    {
        if (!player)
            return;
        GameObject gr = Instantiate(grenade, transform.position, transform.rotation);
        Vector2 dir = player.position - transform.position;
        float dropForce = Random.Range(5f, 20f);
        gr.GetComponent<Rigidbody2D>().AddForce(dir.normalized * dropForce, ForceMode2D.Impulse);
    }
    void SetPlayerPos(Transform playerPos)
    {
        player = playerPos;
    }

    T[] ShaffleArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            int randomInder = Random.Range(i, array.Length);
            T tempItem = array[randomInder];
            array[randomInder] = array[i];
            array[i] = tempItem;
        }
        return array;
    }
    void OnDisable()
    {
        PlayerSpawner.spawnEvent -= SetPlayerPos;
    }
}
