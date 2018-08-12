using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTilesSecret : MonoBehaviour {

    public Animator anim;
    public Vector2 boxPos;
    public Vector2 size;
    public LayerMask mask;
    public float anlge;

    bool isSecretFind;
    bool fromStartFind;
    void Awake()
    {
        Player.deathEvent += Refresh;
    }
    void Start()
    {
        if (LevelManager.instance.GetSecter())
        {
            anim.SetTrigger("hide");
            isSecretFind = true;
            fromStartFind = true;
        }
    }
    void Update()
    {
        if (isSecretFind)
            return;

        Collider2D col = Physics2D.OverlapBox(boxPos, size, anlge, mask);

        if (col != null && col.tag == "Player")
        {
            anim.SetTrigger("hide");
            isSecretFind = true;
        }
    }
    void Refresh()
    {
        if (!fromStartFind)
        {
            anim.Rebind();
            isSecretFind = false;
        }
    }
    void OnDisable()
    {
        Player.deathEvent -= Refresh;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxPos, size);
    }
}
