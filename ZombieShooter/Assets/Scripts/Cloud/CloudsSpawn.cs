using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsSpawn : MonoBehaviour {

    public GameObject objToSpawn;
    public float spawnTime = 10f;
    public float startTime;
    public bool facingLeft;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", startTime, spawnTime);
	}
	
	// Update is called once per frame
	void Spawn () {
        if (!facingLeft)
        {
            GameObject go = Instantiate(objToSpawn, transform.position,Quaternion.identity);
            Vector3 scale = go.transform.localScale;
            scale.x *= -1;
            go.transform.localScale = scale;
        }
        else { Instantiate(objToSpawn, transform.position, Quaternion.identity); }
	}
}
