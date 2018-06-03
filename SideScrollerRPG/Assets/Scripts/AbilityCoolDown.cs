using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCoolDown : MonoBehaviour {

    public Button btn;
    public Image darkMask;
    public PlayerSO playerParam;
    public AbilityTypes ability;
    
    private float coolDownDuration;
    private float nextReadyTime;
    private float coolDownTimeLeft;
    private bool isReady;

    void Awake()
    {
        switch (ability)
        {
            case AbilityTypes.Heal:
                coolDownDuration = playerParam.healCD;
                break;
            case AbilityTypes.Grenade:
                coolDownDuration = playerParam.grenadeCD;
                break;
            case AbilityTypes.Freeze:
                coolDownDuration = playerParam.freezeCD;
                break;
        }
    }
    void Update() 
    {
        if (isReady)
            return;
        bool coolDownComplete = (Time.time >= nextReadyTime);
        if (coolDownComplete) 
        {
            isReady = true;
            btn.interactable = true;
            
        } else
        {
            coolDownTimeLeft -= Time.deltaTime;
            darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
        }
    }
    public void CoolDown()
    {
        nextReadyTime = coolDownDuration + Time.time;
        coolDownTimeLeft = coolDownDuration;
        btn.interactable = false;
        isReady = false;
    }
}
public enum AbilityTypes {Heal, Grenade, Freeze }
