using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeals : Heals {

    public static event System.Action deathEvent;

    public Slider HealsBar;
    public PlayerSO playerParam;
    public string healSoundEffectName;

    void Start()
    {
        CurrentHeals = playerParam.maxHeals;
    }
    void Update()
    {
        HealsBar.value = CurrentHeals / 100f;
        if (!playerParam.isExitGame)
            playerParam.levelEndHealthLeft = CurrentHeals;

        if (IsDead && deathEvent != null)
        {
            deathEvent();
        }
    }
    public void HealAbility()
    {
        //если хп больше половины, сделать его максимальным, в ином случае добавить половину.
        AudioManager._instance.PlaySoundEffect(healSoundEffectName);
        CurrentHeals = (CurrentHeals > playerParam.maxHeals / 2) ? playerParam.maxHeals : Mathf.RoundToInt(playerParam.maxHeals * playerParam.healPower) + CurrentHeals;
    }
}
