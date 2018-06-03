using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class MissionSO : ScriptableObject
{
    public string missionName;
    public int maxProgress = 10;
    public int currentProgress = 0;
    public int revard = 500;
    public int skipFor = 150;
    public Sprite medalIcon;

    public bool isComplete;
    public bool isActive; //показывают ли её сейчас на экране игроку.

    public void ToDefault()
    {
        currentProgress = 0;
        skipFor = 150;
        isComplete = false;
        isActive = false;
    }
}
