using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitcher : MonoBehaviour {

    public SoundSO sound;
    private GameObject currentM;
    private GameObject newM;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetCurrentMenu(GameObject menu)
    {
        currentM = menu;
    }
    public void SetNewMenu(GameObject menu)
    {
        newM = menu; 
        anim.SetTrigger("switch");

        //AudioManager._instance.PlaySoundEffect(sound);
    }
    /*public void StartSwitch()
    {
        anim.SetTrigger("");
    }*/
    public void SwitchMenu()
    {
        currentM.SetActive(false);
        newM.SetActive(true);

        //AudioManager._instance.PlaySoundEffect(sound);
    }
}
