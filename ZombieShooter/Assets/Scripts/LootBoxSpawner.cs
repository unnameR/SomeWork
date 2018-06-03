using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxSpawner : MonoBehaviour {

	// Use this for initialization
    public GameObject lootBox;
    public float timeSpawn=60f;

    private bool stopSpawn;
    private GameObject box;

    void Start()
    {
        InvokeRepeating("Spawn",60, timeSpawn);
    }
	// Update is called once per frame
    void Update()
    {
        if (box != null)
        {
            stopSpawn = true;
        }
        else stopSpawn = false;
    }
	private void Spawn () {
        if (stopSpawn)
            return;
        
       box= Instantiate(lootBox, transform.position, transform.rotation, transform.parent);
	}
}
