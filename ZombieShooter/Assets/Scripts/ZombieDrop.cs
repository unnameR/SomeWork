using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDrop : MonoBehaviour {

    public Sprite[] sprites;

    private SpriteRenderer sr;

    private int dropIndex;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        int r = Random.Range(0,10);
        if (r < 5)            //50%
            dropIndex = 1;
        if (r >= 5 && r < 8)  //30%
            dropIndex = 0;
        if (r >= 8)           //20%
            dropIndex = 2;
        sr.sprite = sprites[dropIndex];
	}

    public int DropIndex
    {
        get { return dropIndex; }
        set { dropIndex = value; }
    }
}
