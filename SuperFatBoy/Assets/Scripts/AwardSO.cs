using UnityEngine;

[CreateAssetMenu]
public class AwardSO : ScriptableObject {

    public string awardName;
    public string conditionName;
    public int condition = 100;
    public int revard = 50;
    public bool isComplete;
    public bool isGetReward;
    public Sprite icon;
    [HideInInspector]public int currentCondition;

    public void SetAward()
    {
        if (!isComplete)
            currentCondition++;

        if (currentCondition == condition)
        {
            isComplete = true;
        }
    }
}
