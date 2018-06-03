using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour {

    public int selectedWeapon = 0;

	void Start () {
        SelectWeapon();
	}
	void Update () {
        if (transform.childCount > 0)
        {
            int prewWeapon = selectedWeapon;
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (selectedWeapon >= transform.childCount - 1)
                    selectedWeapon = 0;
                else
                    selectedWeapon++;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (selectedWeapon <= 0)
                    selectedWeapon = transform.childCount - 1;
                else
                    selectedWeapon--;
            }

            if (prewWeapon != selectedWeapon)
                SelectWeapon();
        }
	}
    public void AddWeapon(GameObject newGun)
    {
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(false);
        }
        GameObject gun= Instantiate(newGun,transform.parent);
        gun.transform.SetParent(transform);
        PlayerController.currentWeapon = gun;

    }
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                PlayerController.currentWeapon = weapon.gameObject;
            }
            else weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
