using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour {

    //public int _id;
    public Text _name;
    public Text _progress;
    public Text _revard;
    public Image _icon;
    public Animator anim;

    MissionSO currentMission;

    MissionSO nextMission;

    public void Init(MissionSO mission)
    {
        currentMission = mission;
        _name.text = mission.missionName;
        _progress.text = mission.currentProgress + "/" + mission.maxProgress;
        _revard.text = mission.revard.ToString();
        _icon.sprite = mission.medalIcon;
    }
    public void SetNextActiveMission()
    {
        currentMission = nextMission;
        _name.text = nextMission.missionName;
        _progress.text = nextMission.currentProgress + "/" + nextMission.maxProgress;
        _revard.text = nextMission.revard.ToString();
        _icon.sprite = nextMission.medalIcon;
    }
    public void MissionSkip()
    {
        anim.SetTrigger("skip");
    }
    public IEnumerator PlayeMissionCompleteAnim()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        anim.SetTrigger("mComplete");
    }
    public MissionSO CurrentMission
    {
        get { return currentMission; }
    }
    public MissionSO NextMission
    {
        set{ nextMission = value; }
    }
    public void PlaySound(string sName)
    {
        AudioManager._instance.PlaySoundEffect(sName);
    }
}
