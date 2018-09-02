using UnityEngine;

public class RocketBullet : MonoBehaviour {

    //public SoundSO flySound;
    public SoundSO explosionSound;
    public SoundSO hitSound;
    public float flySpeed;
    public float rotationSpeed;
    public float radius = 0.3f;
    public LayerMask mask;
    [SerializeField]
    private Transform target;

    void OnEnable()
    {
        //AudioManager._instance.PlaySoundEffect(flySound);
    }
    void FixedUpdate()
    {
        if (target == null)
        {
            AudioManager._instance.PlaySoundEffect(explosionSound);
            DestroyBullet();
            return;
        }

        Vector2 direction = target.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(90+angle,Vector3.forward), rotationSpeed * Time.fixedDeltaTime);
        
        transform.position +=-transform.up * flySpeed * Time.fixedDeltaTime;
        //hit wall поменять растояние на еукаст сферы, и сделать маску на препятствие и игрока.
        Collider2D col = Physics2D.OverlapCircle(transform.position, radius, mask);
        if (col!=null)
        {
            if (col.tag == "Player")
                HitPlayer();
            else DestroyBullet();
        }
    }
    public void SetTarget(Transform t)
    {
        target = t;
    }
    void HitPlayer()
    {
        target.GetComponent<Player>().Die();
        //explosion
        AudioManager._instance.PlaySoundEffect(hitSound);
        DestroyBullet();
    }
    void DestroyBullet()
    {
        //explosion effect
        

        Destroy(gameObject);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
