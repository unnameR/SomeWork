using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDeactiveButton : MonoBehaviour {

    public SoundSO sound;
    public LaserBeem[] lasers;
    public SpriteRenderer sr;
    public Sprite defaultSprite;
    public Sprite triggeredSprite;

    public LayerMask mask;
    public float radius;

    public bool activate;//Если тру
    public bool useTimer;
    public float deactiveTime;

    bool isPressing;

    void Update()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, radius, mask);
        if (col != null && col.tag == "Player")
        {
            //AudioManager._instance.PlaySoundEffect(sound);
            isPressing = true;
            SetLasers(activate);
            sr.sprite = triggeredSprite;
        }
        else
        {
            if (isPressing)
            {
                if (useTimer)
                    StartCoroutine(DeactiveTime(!activate));
                else SetLasers(!activate);
            }
            isPressing = false;
        }
    }
    void SetLasers(bool action)
    {
        AllLasers(action);
    }
    IEnumerator DeactiveTime(bool action)
    {
        yield return new WaitForSeconds(deactiveTime);
        AllLasers(action);
        sr.sprite = defaultSprite;
    }
    void AllLasers(bool active)
    {
        foreach (var laser in lasers)
        {
            laser.SetActiveLaser(active);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
