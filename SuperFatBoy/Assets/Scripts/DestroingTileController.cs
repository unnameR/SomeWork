using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroingTileController : MonoBehaviour {

    void Awake()
    {
        Player.deathEvent += RefreshTiles;
    }
    void RefreshTiles()
    {
        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(true);
        }
    }
    void OnDisable()
    {
        Player.deathEvent -= RefreshTiles;
    }
}
