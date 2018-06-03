using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorAndBonusMove : MonoBehaviour {

    public float speed=5f;
    void Start()
    {
        Animator anim = GetComponent<Animator>();
        if (anim != null)
            anim.SetInteger("duraction",Random.Range(-3,4));
    }
	// Update is called once per frame
	void FixedUpdate () {
        transform.position += Vector3.down*speed*Time.deltaTime;
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
