using UnityEngine;

public class LaserBeem : MonoBehaviour {

    public SoundSO sound;
    public LineRenderer laser;
    public LayerMask mask;
    public LayerMask deactiveMask;
    public Gradient activeGrad;
    public Gradient deactiveGrad;

    public bool useTimer;
    public float laserDuration;
    float maxDistance = 99;

    [SerializeField] private bool isActive;
    private float currentTime;

    void Update()
    {
        if (useTimer)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= laserDuration)
            {
                currentTime = 0;
                isActive = !isActive;
            }
        }

        UpdateLaser();
    }
    void UpdateLaser()
    {
        //AudioManager._instance.PlaySoundEffect(sound);
        Vector3 localStart = transform.localToWorldMatrix.MultiplyPoint(laser.GetPosition(0));
        Vector3 localEnd = transform.localToWorldMatrix.MultiplyPoint(laser.GetPosition(1));
        Vector2 direction = localEnd - localStart;//когда direction = 0 конечная точка не перезаписывается
        //нужно отвязать направление от лазера
        RaycastHit2D hit;
        if (isActive)
        {
            hit = Physics2D.Raycast(localStart, direction.normalized, maxDistance, mask);            
            laser.colorGradient = activeGrad;
        }
        else
        {
            hit = Physics2D.Raycast(localStart, direction.normalized, maxDistance, deactiveMask);
            laser.colorGradient = deactiveGrad;
        }
        if (hit.collider != null )
        {
            if (isActive)//при деактивированом лазере, оно не видит игрока и боса. 
            {    //Но в уровне с боссом он должен видить игрока((..
            
                if (hit.collider.tag == "Player")
                    hit.collider.GetComponent<Player>().Die();
                else if (hit.collider.tag == "Boss")
                    hit.collider.GetComponent<BossChapter2>().Die();
            }
            if (hit.distance != 0)
                laser.SetPosition(1, laser.GetPosition(0) + Vector3.up * hit.distance);
        }
    }
    public void SetActiveLaser(bool active)
    {
        isActive = active;
    }
}
