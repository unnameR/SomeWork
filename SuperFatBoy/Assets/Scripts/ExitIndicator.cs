using UnityEngine;

public class ExitIndicator : MonoBehaviour {

    public float maxDistanseFromCenter = 5;
    [SerializeField]
    Transform target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Exit").transform;
    }
    void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0,angle-90);

        Vector3 centerOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        transform.position = Vector3.MoveTowards(centerOfScreen, target.transform.position, maxDistanseFromCenter);
    }
}
