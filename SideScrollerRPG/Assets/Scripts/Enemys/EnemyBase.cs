using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour,IPushback {

    [SerializeField]
    public EnemySO enemyParam;

    [SerializeField]
    protected Transform gunHandle;
    [SerializeField]
    protected GameObject bullet;
    [SerializeField]
    protected SimpleObjectPool pool;
    [SerializeField]
    protected string shootSoundName;

    protected Transform player;
    protected Rigidbody2D rb2d;
    protected Vector2[] way;
    protected Vector2 targetDirection;
    protected bool canShoot;
    private bool isFreeze;
    protected const float OFFSET = 0.4f;
    private int curentPointIndex = 0;
    protected bool canMove;
    Vector2 moveToPoints;

    
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
    }
    protected virtual void OnEnable()
    {
        RefreshFlags();
        RebuildWay();
    }
    private void RebuildWay()//возможно оно меняет свою позицию когда спаунится после построения пути
    {
        Vector2 moveto = new Vector2(player.position.x + Random.Range(-OFFSET, OFFSET) + enemyParam.endDistance, transform.position.y);//отнимаем от игрока растояние. Это и есть точка куда бот будет двигатся.
        way = BuildWay(transform.position, moveto, enemyParam.pathCount);

        canMove = true;
    }
    protected void RefreshFlags()
    {
        canMove = false;
        canShoot = false;
        isFreeze = false;
        curentPointIndex = 0;
    }
    public void Pushback(float power)
    {
        transform.localPosition -= Vector3.left * power;
    }
    public void Freeze(float freezeTime)
    {
        StartCoroutine(Freezeing(freezeTime));
    }
    private IEnumerator Freezeing(float freezeTime)
    {
        isFreeze = true;
        canShoot = false;
        yield return new WaitForSeconds(freezeTime);
        isFreeze = false;
        canShoot = true;
    }

    protected Vector2[] BuildWay(Vector2 moveFrom, Vector2 moveTo, int pathCount)
    {
        Vector2[] ways = new Vector2[pathCount];
        Vector2 dir = (moveTo - moveFrom);
        float distance = Vector2.Distance(moveFrom, moveTo) / pathCount;

        for (int i = 0; i < pathCount; i++)
        {
            ways[i] = moveFrom + (distance * (i + 1)) * dir.normalized;//ways[i] последний будет равент moveTo
        }
        return ways;
    }
    protected IEnumerator MovementCooldown()
    {
        yield return new WaitForSeconds(1f);
        canMove = true;
    }
    void FixedUpdate()
    {
        if (way == null || curentPointIndex >= way.Length)
            return;
        if (way != null && !isFreeze && canMove && MoveTo(way[curentPointIndex]))
        {
            canShoot = true;
            canMove = false;
            StartCoroutine(MovementCooldown());
        }
    }
    private bool MoveTo(Vector2 moveToPoint)
    {
        Vector2 dir = (moveToPoint - rb2d.position).normalized;
        rb2d.MovePosition(rb2d.position + dir * enemyParam.moveSpeed * Time.fixedDeltaTime);

        float sqrMag = (moveToPoint-rb2d.position).sqrMagnitude;
        if (sqrMag < 0.01f)
        {
            rb2d.position = moveToPoint;
            if (curentPointIndex < way.Length-1)
                curentPointIndex++;
            return true;
        }
        return false;
    }
    protected virtual void Shoot(Vector2 normalDirection)
    {
        AudioManager._instance.PlaySoundEffect(shootSoundName);
        GameObject blet = pool.GetObject(bullet);
        blet.transform.position = gunHandle.GetChild(0).position;//точка вылета пули.
        blet.transform.rotation = gunHandle.rotation;
        blet.SetActive(true);
        blet.GetComponent<EnemyBullet>().EnemyInit(enemyParam.damage, enemyParam.bulletSpeed, normalDirection);
    }
    protected void AimToPlayer()
    {
        targetDirection = player.position - gunHandle.position;
        float angle = Mathf.Atan2(targetDirection.x, targetDirection.y) * Mathf.Rad2Deg;
        gunHandle.localRotation = Quaternion.Euler(0, 0, 90 - angle);
    }
}
public interface IPushback
{
    void Pushback(float power);
}
