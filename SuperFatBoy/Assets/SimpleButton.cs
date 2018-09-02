using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleButton : MonoBehaviour {

    public float radius;
    public LayerMask mask;
    public BossChapter2 boss;

    public Sprite triggeredSprite;

    SpriteRenderer sr;
    bool isTriggered;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isTriggered)
        {
            Collider2D playerColl = Physics2D.OverlapCircle(transform.position, radius);
            if (playerColl&&playerColl.tag=="Player")
            {
                Action();
            }
        }
    }
    void Action()
    {
        isTriggered = true;
        sr.sprite = triggeredSprite;
        boss.DropKey();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
