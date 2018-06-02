using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour {

    public GameObject obj;
    bool isActive;

	public void Click () {
        if (isActive&&obj.activeSelf)
        {
            isActive = false;
            obj.SetActive(false);
        }
        else
        {
            isActive = true;
            obj.SetActive(true);
        }
	}
}
