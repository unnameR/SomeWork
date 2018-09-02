using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChapter1 : MonoBehaviour {
    //у боса есть обзор, вейпоинты, когда бос видит игрока он кидает гранаты в сторону игрока, 2шт с рандомным 
    //углом и силой, после чего перемещается к следущей точке и там ждёт игрока. Так же должен соприкосатся с 
    //кубом. При смерти дропается ключь которым открывают дверь.
    public LayerMask mask;
    public GameObject grenade;
    public GameObject key;
    public float visionRadius;
    public float moveSpeed;
    public Vector2[] localWaypoints;
    Vector2[] globalWaypoints;

    int currentWaypoint;
    bool isMovind;
    void Start()
    {
        globalWaypoints = new Vector2[localWaypoints.Length];
        for (int i = 0; i < localWaypoints.Length; i++)
        {
            globalWaypoints[i] = localWaypoints[i] + (Vector2)transform.position;
        }
    }
    void Update()
    {
        if (isMovind)//если бос двжется и соприкасается с игроком, то пока он не дойдёт до точки, он не перейдёт к следущей.
            return;

        Collider2D coll = Physics2D.OverlapCircle(transform.position, visionRadius, mask);
        if (coll)
        {
            if (coll.tag == "Player")//go to next waypoint
            {
                isMovind = true;
                
                StartCoroutine(MoveTo());
                DropGrenade(coll.transform);//2grenade drop
                DropGrenade(coll.transform);
            }
            if (coll.tag == "MoveBox")
            {
                key.SetActive(true);
                key.transform.position = transform.position;
                key.GetComponent<Rigidbody2D>().AddForce(Vector2.one*3, ForceMode2D.Impulse);
                Destroy(gameObject);
            }
        }
    }

    private void DropGrenade(Transform player)
    {
        //спаун гранат
        GameObject gr = Instantiate(grenade, transform.position, transform.rotation);
        Vector2 dir = player.position - transform.position;
        float dropForce = Random.Range(3f, 10f);
        gr.GetComponent<Rigidbody2D>().AddForce(dir.normalized * dropForce, ForceMode2D.Impulse);
    }
    
    IEnumerator MoveTo()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, globalWaypoints[currentWaypoint], moveSpeed*Time.deltaTime);//easedPercentBetweenWaypoints);
            if (transform.position == (Vector3)globalWaypoints[currentWaypoint])
            {
                //DropGrenade();//

                if (currentWaypoint < globalWaypoints.Length - 1)
                    currentWaypoint++;
                isMovind = false;
                break;
            }
            yield return null;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        if (localWaypoints != null)
        {
            Gizmos.color = Color.red;
            float size = .3f;

            for (int i = 0; i < localWaypoints.Length; i++)
            {
                Vector3 globalWaypointPos = (Vector3)localWaypoints[i] + transform.position;
                Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
            }
        }
    }
}
