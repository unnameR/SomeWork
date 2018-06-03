using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heals : MonoBehaviour {

    public float maxHeals = 100;
    public float destrTime = 2f;

    public AudioClip hurtSound;
    public AudioClip dieSound;

    private Animator anim;

    private AudioSource asourse;

    private bool dead;

    private float currentHeals;
    void Start()
    {
        asourse = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        maxHeals *= GameManager.currentDay;//только для зомбаков
        currentHeals = maxHeals;
    }
    public float CurrentHeals 
    { 
        get { return currentHeals; } 
        set { currentHeals = value; } 
    }
    public bool Dead
    {
        get { return dead; }
        set { dead = value; }
    }
    public void TakeDamage(float damage)
    {
        asourse.PlayOneShot(hurtSound);
        currentHeals -= damage;
        if (currentHeals <= 0)
            Die();

        //бредятина
        if (!dead && gameObject.tag == "Enamy" && anim.GetBool("attack"))
            anim.Play("Zomb_Hurt");

        if (gameObject.tag == "House")
        {
            GameObject canvas = GameObject.Find("Canvas");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //float distance =Vector3.Distance(gameObject.transform.position, player.transform.position);
            float distance = gameObject.transform.position.x + player.transform.position.x;
            if (distance >= 12)
                canvas.GetComponent<Animator>().Play("DamageChecker_Left");
            if (distance <= -3)
                canvas.GetComponent<Animator>().Play("DamageChecker_Right");
        }

    }
    private void Die()
    {
        //play die animation
        asourse.PlayOneShot(dieSound);
        dead = true;
        GetComponent<Animator>().SetBool("Died",true);//анимация апускаеться пока тру, а нужно проиграть 1 раз
        Destroy(gameObject, destrTime);
    }
}
