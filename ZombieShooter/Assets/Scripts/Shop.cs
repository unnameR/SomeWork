using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    public Heals houseHP;
    public WeaponSwitching ws;
    public Animator anim;

    private Transform btn;
    private Transform costtext;
    private Transform bayedtext;

    private Transform turretSpawner;

    //private GameObject turretLeft;
    //private GameObject turretRight;
    private int itemCost;

    void Update()
    {
        //если тюрель или стена уничтожена - дать возможность купить еще
    }
    public void TurretSpawner(Transform spawner)
    {
        turretSpawner = spawner;
    }
    public void BayTurret(GameObject item)
    {
        if (PlayerController.money >= itemCost)
        {
            PlayerController.money -= itemCost;

            Instantiate(item, turretSpawner.position, Quaternion.identity, turretSpawner);
            //t.transform.localScale

            btn.gameObject.SetActive(false);
            costtext.gameObject.SetActive(false);
            bayedtext.gameObject.SetActive(true);
        }
        else anim.Play("NoMoneyAnim");
    }
    public void BayItem(GameObject item)
    {
        if (PlayerController.money >= itemCost)
        {
            PlayerController.money -= itemCost;

            ws.AddWeapon(item);

            btn.gameObject.SetActive(false);
            costtext.gameObject.SetActive(false);
            bayedtext.gameObject.SetActive(true);
        }
        else anim.Play("NoMoneyAnim");
    }
    public void BayHouseMaxHP()
    {
        if (PlayerController.money >= itemCost)
        {
            if (houseHP.CurrentHeals < houseHP.maxHeals)
            {
                PlayerController.money -= itemCost;

                houseHP.CurrentHeals = houseHP.maxHeals;
            }
            else Debug.Log("Already have maximum hp");
        }
        else anim.Play("NoMoneyAnim");
    }
    public void ItemCost(int cost)
    {
        itemCost = cost;
    }
    public void CurrentItem(GameObject item)
    {
        //можнo не делать
        btn = item.transform.Find("Button");
        costtext = item.transform.Find("Cost");
        bayedtext = item.transform.Find("Bayed");
    }
}
