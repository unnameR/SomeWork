using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heals: MonoBehaviour
{
    public string dieSoundName;
    public string dieVoiceSoundName;
    private Animator anim;

    private bool isDead;

    int currentHeals;
   
    public int CurrentHeals 
    { 
        get { return currentHeals; } 
        set { currentHeals = value; } 
    }
    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }
    public void TakeDamage(int damage)
    {
        if (isDead)
            return;
        //asourse.PlayOneShot(hurtSound);
        currentHeals -= damage;

        if (currentHeals <= 0)
            Died();

    }
    private void Died()
    {
        //play die animation

        AudioManager._instance.PlaySoundEffect(dieSoundName);
        AudioManager._instance.PlaySoundEffect(dieVoiceSoundName);
        isDead = true;
    }
}
