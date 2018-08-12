using UnityEngine;

public class LaserBeem : MonoBehaviour {

    public SoundSO sound;
    public LineRenderer laser;
    public LayerMask mask;
    public Gradient activeGrad;
    public Gradient deactiveGrad;

    public bool useTimer;
    public float laserDuration;

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
        if (isActive)
            ActiveLaser();
        else DeActiveLaser();
    }
    void ActiveLaser()
    {
        //AudioManager._instance.PlaySoundEffect(sound);

        laser.colorGradient = activeGrad;
        Vector2 localStart = transform.localToWorldMatrix.MultiplyPoint(laser.GetPosition(0));
        Vector2 localEnd = transform.localToWorldMatrix.MultiplyPoint(laser.GetPosition(1));
        Vector2 direction = localEnd - localStart;
        float distance = Vector2.Distance(localStart, localEnd);

        RaycastHit2D hit = Physics2D.Raycast(localStart, direction.normalized, distance, mask);
        //Debug.DrawLine(localStart, localEnd, Color.green);
        if (hit.collider != null && hit.collider.tag == "Player")
        {
            hit.collider.GetComponent<Player>().Die();
        }
    }
    void DeActiveLaser()
    {
        laser.colorGradient = deactiveGrad;
        AudioManager._instance.StopPlay(sound);
    }
}
