using UnityEngine;

public class AnimationComtroller : MonoBehaviour {

    public Animator changeMenuAnim;
    public Animator pauseMenuAnim;
    public Animator missionMenuAnim;
    public GameObject newGameMenu;
    public GameObject resultMenu;
    public PlayerSO playerParam;
    
    GameObject from;
    GameObject to;

    void Start()
    {
        AudioManager._instance.PlaySoundEffect("MenuMusic");
        if (playerParam.levelEnd)//при запуске игры активировать меню новой игры. После конца уровня активировать меню результатов
        {
            newGameMenu.SetActive(false);
            resultMenu.SetActive(true);
        }
    }
    public void Settings(string triggerName)
    {
        pauseMenuAnim.SetTrigger(triggerName);
    }
    public void Missions(string triggerName)
    {
        missionMenuAnim.SetTrigger(triggerName);
    }
    public void ChangeMenu()
    {
        from.SetActive(false);
        to.SetActive(true);

        changeMenuAnim.SetTrigger("changeOut");
    }
    public void ChangeFrom(GameObject f)
    {
        from = f;
    }
    public void ChangeTo(GameObject t)
    {
        to = t;
        changeMenuAnim.SetTrigger("changeIn");
    }
    public void NoMoneyAnim()
    {
        changeMenuAnim.SetTrigger("noMoney");
    }
}
