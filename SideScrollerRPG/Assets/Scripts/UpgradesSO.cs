using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class UpgradesSO : ScriptableObject {

    public string _name;
    [TextArea(1, 3)]
    public string description;
    public int defaultPrise;
    public int prise = 400;
    public int upLevel = 0;
    public int unlockWave = 0;
    public float increaseStat = 1;
    public Sprite icon;

    public bool canUp;//подсвечивать если можно прокачать
    public bool isLock;//закрыт ли обьект для прокачки
    public string lockText;

    public void LoadUpgrade(int upLvl)
    {
        upLevel = upLvl;
        prise = defaultPrise + upLvl * 800;
    }
    public void ToDefault()
    {
        prise = defaultPrise;
        upLevel = 0;
        canUp = false;
        isLock = true;
    }
}
