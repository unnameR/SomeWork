using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatsController : MonoBehaviour {


    public float zoneMin = 0f;
    public float zoneMax = 4f;

    private Animator anim;

    private float speed = 1f;
    private float speedChangeTime = 10;
    private bool faceingLeft;
    //рандомно выбираем анимации
    //нужна зона в которой будут бегать коты

    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("SpeedChange", 0, speedChangeTime);
    }
    void SpeedChange()
    {
        speed = Random.Range(0.2f,2.2f);
    }
    void Update()
    {
        if (transform.position.x <= zoneMin)
        {
            anim.SetBool("left", false);
            faceingLeft = false;
        }
        if (transform.position.x >= zoneMax)
        {
            anim.SetBool("left", true);
            faceingLeft = true;
        }
    }
	void FixedUpdate () {

        if (faceingLeft)
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y);
	}
}
