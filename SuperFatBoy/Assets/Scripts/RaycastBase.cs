using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBase : MonoBehaviour {

    public LayerMask mask;
    public float radius = 0.3f;

    protected Vector3 center;
    protected virtual void Start()
    {
        center = transform.position;
    }
    protected bool OverlapCircleCast()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, radius, mask);
        if (coll != null && coll.tag == "Player")
        {
            return true;
        }
        return false;
    }
    protected bool OverlapCircleCast(Vector3 offset)
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position + offset, radius, mask);
        if (coll != null && coll.tag == "Player")
        {
            return true;
        }
        return false;
    }
    public static bool OverlapCircleCastPlayer(Vector2 center, float radius, int musk)
    {
        Collider2D coll = Physics2D.OverlapCircle(center, radius, musk);
        if (coll != null && coll.tag == "Player")
        {
            return true;
        }
        return false;
    }
    public static bool OverlapBoxCastPlayer(Vector2 point, Vector2 size, float angle, int musk)
    {
        Collider2D coll = Physics2D.OverlapBox(point, size, angle, musk);
        if (coll != null && coll.tag == "Player")
        {
            return true;
        }
        return false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(center, radius);
    }
}
