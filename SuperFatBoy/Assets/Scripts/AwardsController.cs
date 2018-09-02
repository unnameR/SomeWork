using UnityEngine;

public class AwardsController : MonoBehaviour {

    public AwardSO[] awards;

    public void SetAward(string name)
    {
        AwardSO aw = GetAward(name);

        if (aw != null)
        {
            aw.SetAward();
            DataSaver.SaveData(aw, aw.awardName);
        }
        else Debug.LogError("noname");
    }
    public bool IsComplete(string name)
    {
        return GetAward(name).isComplete;
    }
    AwardSO GetAward(string name)
    {
       return System.Array.Find<AwardSO>(awards, n => n.awardName == name);
    }
}
