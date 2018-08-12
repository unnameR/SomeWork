using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampline : MonoBehaviour {

    public SoundSO sound;
    public float jumpPower=10;
    public SpriteRenderer sr;
    public Sprite defaultSprite;
    public Sprite triggeredSprite;
    public LayerMask mask;
    public float radius;

    void Update()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, radius, mask);
        if (col != null && col.tag == "Player")
        {
            AudioManager._instance.PlaySoundEffect(sound);
            StartCoroutine(Jump(col));
        }
    }
    IEnumerator Jump(Collider2D col)
    {
        col.GetComponent<Player>().Trapmline(jumpPower);//
        sr.sprite = triggeredSprite;
        yield return new WaitForSeconds(0.2f);
        sr.sprite = defaultSprite;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
