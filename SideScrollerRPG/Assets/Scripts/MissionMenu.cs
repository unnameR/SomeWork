using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MissionMenu : MonoBehaviour {

    public PlayerSO playerParam;
    public MissionSO[] missions;
    public Text progressTxt;
    public Text[] skipForTxts;
    public Mission[] activeMissionsGOs;
    /// <summary>
    /// нужно делать через Queue. Что бы та мисия которую пропустили, стала в конец очереди. Когда не останется
    /// активных мисий, нужно делать неактивные активными и показывать их.
    /// Если не завершенных мисий меньше 3, то пропустить их нельзя.
    /// </summary>

    Queue<MissionSO> skipedMissions = new Queue<MissionSO>();

    int missionsCompleteCount;

    void Start()
    {
        Init();
    }
    void Init()
    {
        missionsCompleteCount = 0;
        /*foreach (var activGO in activeMissionsGOs)
        {
            SetActiveMission(activGO);
        }*/
        int i = 0;
        foreach (var mission in missions)
        {
            if (mission.isComplete)
            {
                missionsCompleteCount++;
            }
            if (mission.isActive && i < activeMissionsGOs.Length)
            {
                activeMissionsGOs[i].Init(mission);
                i++;
            }
        }
        progressTxt.text = "Progress: " + missionsCompleteCount + "/" + missions.Length;

        UpdateSkipTxt(missions[0].skipFor);
    }
    void UpdateSkipTxt(int cost)
    {
        foreach (var skipTxt in skipForTxts)
        {
            skipTxt.text = cost.ToString();
        }
    }
    void SetActiveMission(Mission currM)//currM позиция мисии
    {
        bool find = false;//если не осталось мисий которые можно пропустить, взять с кю
        foreach (var mission in missions)
        {
            if (!mission.isComplete && !mission.isActive && !skipedMissions.Contains(mission))//если мисия не завершена и её нету в списке пропущеных мисий. Делаем её активной.
            {
                find = true;
                mission.isActive = true;
                currM.NextMission = mission;
                //currM.Init(mission);
                break;
            }
        }
        if (!find)
        {
            MissionSO mi = skipedMissions.Dequeue();
            mi.isActive = true;
            currM.NextMission = mi;
            //currM.Init(mi);
        }
    }
    public void SkipMission(Mission currM)
    {
        //берём мисию которую нужно пропустить, делаем её не активной, меняем её на другую.
        // Если не завершенных мисий меньше 3, то пропустить их нельзя.
        int unCompleteCount = missions.Length;
        foreach (var mission in missions)
        {
            if (mission.isComplete)
            {
                unCompleteCount--;
            }
        }
        if (unCompleteCount <= 3)
            return;

        int skipCost = missions[0].skipFor;

        if (playerParam.money >= skipCost)
        {
            playerParam.money -= skipCost;
            currM.CurrentMission.isActive = false;

            foreach (var mission in missions)
            {
                mission.skipFor *= 2;
            }

            UpdateSkipTxt(missions[0].skipFor);
            skipedMissions.Enqueue(currM.CurrentMission);
            SetActiveMission(currM);//позиция мисии на gameobject
        }
        else
        {
            //окно нет денег
            Debug.Log("got no money");
        }
    }
}
