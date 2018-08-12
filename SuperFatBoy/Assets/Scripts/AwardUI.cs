using UnityEngine.UI;
using UnityEngine;

public class AwardUI : MonoBehaviour {

    public GameParamSO gameParam;
	public Text aNameTxt;
	public Text aRequestTxt;
    public Text rewardCount;
    public Image icon;
    public GameObject revardBtn;
    public AwardSO award;
    public Color activeColor;

    void Start()
    {
        UpdateStat();
    }
    void UpdateStat()
    {
        aNameTxt.text = award.awardName;
        aRequestTxt.text = award.conditionName;//не везде есть текст. Хотя думаю следует его создать, но сделать невидимым.
        rewardCount.text = award.revard.ToString();
        icon.sprite = award.icon;
        revardBtn.SetActive(award.isComplete && !award.isGetReward);

        aNameTxt.color = award.isComplete ? Color.white : Color.gray;
        aRequestTxt.color = award.isComplete ? Color.white : Color.gray;
        rewardCount.color = award.isComplete ? Color.white : Color.gray;
        icon.color = award.isComplete ? activeColor : Color.gray;
    }
    public void Complete()
    {
        award.isGetReward = true;
        gameParam.playerMoney += award.revard;
        UpdateStat();
    }
}
