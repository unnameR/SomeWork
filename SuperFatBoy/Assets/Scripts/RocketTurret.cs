using UnityEngine;

public class RocketTurret : MonoBehaviour {

    public SoundSO sound;
    public Transform partToRotate;
    public Transform bulletSpawnpoint;
    public GameObject bullet;
    public LayerMask mask;
    public float radius;
    public float reloadTime;

    [SerializeField]
    private Transform target;
    private float timeToNextShoot;

    void Awake()
    {
        PlayerSpawner.spawnEvent += SetTardet;
        //target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void SetTardet(Transform player)
    {
        target = player;
    }
    void Update()
    {
        if (target == null)
            return;

        float distanceToEnemy = Vector3.Distance(target.position, transform.position);

        if (distanceToEnemy <= radius)
        {
            Vector3 dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            partToRotate.rotation = Quaternion.Euler(0, 0, 90+angle);

            if (RayToTarget(dir) && Time.time >= timeToNextShoot)
            {
                timeToNextShoot = Time.time + reloadTime;
                Fire();
            }
        }
    }
    bool RayToTarget(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(bulletSpawnpoint.position, direction.normalized, radius, mask);
        if (hit.collider != null && hit.collider.tag == "Player")        
            return true;
        else return false;
    }
    void Fire()
    {
        AudioManager._instance.PlaySoundEffect(sound);
        RocketBullet rb = Instantiate(bullet, bulletSpawnpoint.position, bulletSpawnpoint.rotation).GetComponent<RocketBullet>();
        rb.SetTarget(target);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
