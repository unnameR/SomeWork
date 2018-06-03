using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementController : MonoBehaviour
{
    public static AchievementController _internal;

    public PlayerSO playerParam;
    public MissionSO[] allMissions;
    public Mission[] activeMissions;

    void Awake()
    {
        if (_internal == null)
            _internal = this;
        else
        {
            Destroy(this);
            return;
        }
    }
    public void AdjustAchievement(string missionName, int progress)
    {
        foreach (var missoin in allMissions)
        {
            if (missoin.missionName == missionName)//могут быть 2 ачивки с одинаковым именем.
            {
                if (missoin.isComplete || !missoin.isActive) break;

                missoin.currentProgress += progress;

                if (missoin.currentProgress >= missoin.maxProgress)
                {
                    missoin.currentProgress = missoin.maxProgress;
                    missoin.isComplete = true;
                }
            }
        }
    }

    public void Init()
    {
        int i = 0;
        foreach (var mission in allMissions)
        {
            if (mission.isActive && i < activeMissions.Length)
            {
                activeMissions[i].Init(mission);
                i++;
            }
        }
        //
        foreach (var mi in activeMissions)
        {
            CheckCopleteMission(mi);
        }
    }
    void SetActiveMission(Mission currM)
    {
        bool find = false;//если не осталось мисий которые можно пропустить
        foreach (var mission in allMissions)
        {
            if (!mission.isComplete && !mission.isActive)//если мисия не завершена и её нету в списке пропущеных мисий. Делаем её активной.
            {
                find = true;
                mission.isActive = true;
                currM.NextMission = mission;
                break;
            }
        }
        if (!find)
        {
            //пустое окно
        }
    }
    void CheckCopleteMission(Mission currM)
    {
        if (currM.CurrentMission.isActive && currM.CurrentMission.isComplete)
        {
            //запустить анимацию смены ачивки
            StartCoroutine(currM.PlayeMissionCompleteAnim());

            playerParam.money += currM.CurrentMission.revard;
            currM.CurrentMission.isActive = false;
            SetActiveMission(currM);//позиция мисии на gameobject
        }

    }
}
