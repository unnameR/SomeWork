using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour {

    public Sprite[] spr; //0-gold, 1-fullammo,2-fullHP,3-ammo,4-weapon/turret,5-Hmoney,
    public GameObject go;

    private Animator anim;
    private SpriteRenderer sr;

    private bool isPressed;
    private string[] drop1 = { "ammo", "gold" };
    private string[] drop2 = { "Hmoney", "fullammo","fullHP", "weapon", "turret" };

	void Start () {
        sr = go.GetComponent<SpriteRenderer>();//что то т ут не так)
        anim = GetComponent<Animator>();
        isPressed = false;
	}
    public string TakeLoot()
    {
        int chance = Random.Range(0,100);
        if (chance <= 90)
        {
            return drop1[Random.Range(0,drop1.Length)];
        }
        else return drop2[Random.Range(0,drop2.Length)];
    }
    public void OpenAnim()
    {
        anim.SetBool("open",true);
        Destroy(gameObject,30f);
    }
    public void DropAnim(int index)
    {
        sr.sprite = spr[index];
        anim.SetBool("showdrop", true);
    }
    public bool IsPressed { get; set; }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isPressed)
        {
            if (col.tag == "Player")
            {
                anim.SetBool("pressKey", true);
            }
            else anim.SetBool("pressKey", false);
        }
    }
}
