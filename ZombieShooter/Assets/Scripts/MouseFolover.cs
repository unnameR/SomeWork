using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFolover : MonoBehaviour {

    public Transform target;
    public Transform gun;

	// Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.position = mousePos;
        float angle = Vector2.Angle(Vector2.right, mousePos - gun.position);
        gun.eulerAngles = new Vector3(0, 0, gun.position.y < mousePos.y ? angle : -angle);
    }
}
