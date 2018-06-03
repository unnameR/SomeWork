using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHeals : Heals{

    public EnemySO enemyParam;
    public Image healsBar;
    public PooledObject myPool;

    public static event System.Action enemyDeathEvent;

    void OnEnable()
    {
        IsDead = false;
        CurrentHeals = enemyParam.maxHeals;
    }
	void Start () 
    {
        CurrentHeals = enemyParam.maxHeals;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!IsDead)
        {
            healsBar.fillAmount = (float)CurrentHeals / enemyParam.maxHeals;
        }
        else
        {
            GameController._enemyDied++;
            GameController._money += enemyParam.moneyForKill;

            if (enemyDeathEvent != null)
                enemyDeathEvent();
            else Debug.LogError("Event is Null!");

            if (myPool.pool != null)
                myPool.pool.ReturnObject(this.gameObject);
            else Destroy(this.gameObject);
        }
	}
}
