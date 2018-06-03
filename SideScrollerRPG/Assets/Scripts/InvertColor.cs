using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertColor : MonoBehaviour {
    
    public SpriteRenderer sr;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "ColorInverter")
        {
            sr.material.SetFloat("_InvertColors", 1);
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "ColorInverter")
            sr.material.SetFloat("_InvertColors", 0);
    }
}
