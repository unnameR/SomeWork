using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTile : MonoBehaviour {

    public float pushPower;
    public PushDirection direction;
    public Vector2 size;
    public LayerMask mask;
    private Animator anim;
    bool isPushed;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
	void Update () {

        //if (isPushed)
        //   return;
        Collider2D coll = Physics2D.OverlapBox(transform.position, size, 0, mask);
        if (coll != null && coll.tag == "Player")
        {
            coll.GetComponent<Player>().Push(pushPower, direction);
            anim.SetTrigger("push");
            isPushed = true;
        }
	}
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }
}
public enum PushDirection {Up, Down, Left, Right}
