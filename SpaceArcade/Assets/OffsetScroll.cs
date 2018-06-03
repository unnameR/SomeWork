using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetScroll : MonoBehaviour {

    public float scrollSpeed=0.5f;

	void Update () {
        Vector2 offset = new Vector2(0, Time.time * scrollSpeed);
        GetComponent<MeshRenderer>().material.mainTextureOffset = offset;
	}
}
