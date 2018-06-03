using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTurretSparns : MonoBehaviour {

    public Transform turretSpawnPoint;

    public GameObject btn;
    public GameObject costtext;
    public GameObject bayedtext;

	// Update is called once per frame
	void Update () {

        if (turretSpawnPoint.childCount <= 0)
        {
            btn.SetActive(true);
            costtext.SetActive(true);
            bayedtext.SetActive(false);
        }
        else 
        {
            btn.SetActive(false);
            costtext.SetActive(false);
            bayedtext.SetActive(true);
        }
	}
}
