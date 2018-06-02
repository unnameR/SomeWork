using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heals : MonoBehaviour {

    public AudioClip dieSound;
    public int maxHeals = 1;
    public float dyingTime = 2f; //столько же сколько и длится анимация смерти

    private Animator anim;
    private AudioSource aSourse;
    private int currentHeals;
	// Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        aSourse = GetComponent<AudioSource>();
    }
	void Start () {
        currentHeals = maxHeals;
	}
	
    public void TakeDamage(int damage)
    {
        currentHeals -= damage;
        if (currentHeals <= 0)
            Die();
    }
    void Die()
    {
        anim.SetTrigger("die");
        aSourse.PlayOneShot(dieSound);
        Destroy(gameObject, dyingTime);
    }
    public int CurrentHeals
    {
        get { return currentHeals; }
        set { currentHeals = value; }
    }
}
