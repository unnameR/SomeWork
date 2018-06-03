using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdateHealsbar : MonoBehaviour {

    public Image healsBar;

    private Heals heals;
    private float maxHeals;
	void Start () {
        heals = GetComponent<Heals>();
        maxHeals = heals.maxHeals;
	}
	
	// Update is called once per frame
	void Update () {
        float hp = heals.CurrentHeals;
        healsBar.fillAmount = hp / maxHeals;
	}
}
