using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpgradeBtn : MonoBehaviour {

    public UpgradesSO upgrade;
    public Text upLevelTxt;
    public Image upIcon;
    public GameObject canUpNitify;
    public GameObject lockMask;
    public Animator anim;

    
    public void Init(int playerMoney, int wave, int maxUpLevel,bool playAnim)
    {
        if (wave == upgrade.unlockWave && playAnim)
            anim.SetTrigger("unlock");
        else anim.enabled = false;
        

        upgrade.isLock = (wave < upgrade.unlockWave);
        upgrade.canUp = (playerMoney >= upgrade.prise && !upgrade.isLock && upgrade.upLevel <= maxUpLevel);

        upLevelTxt.text = upgrade.isLock?"-":upgrade.upLevel.ToString();
        upIcon.sprite = upgrade.icon;
        canUpNitify.SetActive(upgrade.canUp);

        lockMask.SetActive(upgrade.isLock);

        upgrade.hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
    public void PlaySound()
    {
        AudioManager._instance.PlaySoundEffect("UpgradeUnlock");
    }
}
