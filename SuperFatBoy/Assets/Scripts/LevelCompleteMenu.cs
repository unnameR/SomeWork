using UnityEngine;
using System.Collections;

public class LevelCompleteMenu : MonoBehaviour {

    public GameParamSO gameParam;
    public Animator anim;
    public MenuSwitcher ms;
    public GameObject levelEndMenu;
    public float lifeTime = 5f;

    bool skip;
    /*void OnEnable()
    {
        if (gameParam.currentLevel.medalM)//одна анимация перебиает другую...
            anim.SetTrigger("activeM");
        if (gameParam.currentLevel.medalS)//Ошибка. Если медалька уже была взята ранее, анимация всеравно будет запускатся
            anim.SetTrigger("activeS");

        StartCoroutine(MenuLife());
    }*/
    public void LevelComplete(bool medal, bool secret)
    {
        if (medal)//одна анимация перебиает другую...
            anim.SetTrigger("activeM");
        if (secret)
            anim.SetTrigger("activeS");
        StartCoroutine(MenuLife());
    }
    IEnumerator MenuLife()
    {
        if (skip)//???????
            yield break;
        yield return new WaitForSeconds(lifeTime);
        if (skip)//??????? да-да, автор незнает как эту штука работает))
            yield break;
        ms.SetCurrentMenu(gameObject);
        ms.SetNewMenu(levelEndMenu);
    }
    public void Skip()
    {
        skip = true;

        ms.SetCurrentMenu(gameObject);
        ms.SetNewMenu(levelEndMenu);
    }
}
