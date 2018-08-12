using UnityEngine;

public class Chest : RaycastBase
{
    public SoundSO sound;
    public Animator anim;

    public float offsetY=0.2f;
    bool isOpen;
    bool fromStartOpen;
    void Awake()
    {
        Player.deathEvent += Refresh;
    }
    protected override void Start()
    {
        center = transform.position + offsetY * Vector3.up;

        if (LevelManager.instance.GetSecter())
        {
            anim.SetTrigger("open");
            isOpen = true;
            fromStartOpen = true;
        }
    }
    void Update()
    {
        if (!isOpen&& OverlapCircleCast(offsetY * Vector3.up))
        {
            Open();
        }
    }
    void Open()
    {
        AudioManager._instance.PlaySoundEffect(sound);
        anim.SetTrigger("open");
        isOpen = true;
        LevelManager.instance.SecretFind(true);
    }
    void Refresh()
    {
        if (!fromStartOpen)
        {
            anim.Rebind();
            isOpen = false;

            LevelManager.instance.SecretFind(false);
        }
    }
    void OnDisable()
    {
        Player.deathEvent -= Refresh;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + offsetY * Vector3.up, radius);
    }
}
