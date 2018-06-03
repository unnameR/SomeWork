using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsController : MonoBehaviour {

    public float minSpeed = 3f;
    public float maxSpeed = 10f;
    
    private float speed;
	// Update is called once per frame
    void Awake()
    {
        speed = Random.Range(minSpeed,maxSpeed);
    }
	void Update () {
        if(transform.localScale.x>=0)
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y);
	}
    void OnTriggerEnter2D(Collider2D col)//не работает
    {
        if (col.tag == "wall")
            Destroy(gameObject);
    }
}
