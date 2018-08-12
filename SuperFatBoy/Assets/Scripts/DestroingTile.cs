using System.Collections;
using UnityEngine;

public class DestroingTile : MonoBehaviour {

    public SoundSO sound;
    public LayerMask mask;
    SpriteRenderer sr;
    Animator anim;
    bool isActive;
    void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        isActive = false;
        sr.color = Color.white;
    }
	void Update () {
        if (!isActive)
        {
            Collider2D coll = Physics2D.OverlapBox(transform.position, Vector2.one * 0.7f, 0, mask);
            if (coll != null && coll.tag == "Player")
            {
                isActive = true;
                Active();
            }
        }
	}
    void Active()
    {
        AudioManager._instance.PlaySoundEffect(sound);
        anim.SetTrigger("destroy");
        StartCoroutine(Destr());//по окончанию клипа запустить выключение обьекта
    }
    IEnumerator Destr()
    {
        yield return new WaitForSeconds(0.6f);
        sr.color = Color.white;
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, Vector2.one*0.7f);
    }
}
