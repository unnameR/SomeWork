using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBonus : MonoBehaviour {

    //public Sprite[] bonusColor;
    public BonusType bonusType;

    [Range(0, 100)]
    public int dropChance = 50;

    private float duration;
	public void TakeBonus () {
        GameManager gm= GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        switch (bonusType)
        {
            case BonusType.pill:
                GameManager.pillCount++;
                if (PlayerGameParameters.isFirstStart && !GameManager.isPillRecived)
                {
                    string text = "Press Q to activate the Pill";
                    gm.ShowHelp(text);
                }
                break;
            case BonusType.shield:
                GameManager.shieldCount++;
                if (PlayerGameParameters.isFirstStart && !GameManager.isPillRecived)
                {
                    string text = "Press Right Mouse Button to activate the Shield";
                    gm.ShowHelp(text);
                }
                break;
            case BonusType.star:
                if (PlayerGameParameters.isFirstStart && !GameManager.isStarRecived)
                {
                    string text = "Collect 10 stars and you get 1 life";
                    gm.ShowHelp(text);
                }
                int stars = GameManager.starCount;
                if (stars >= 9)
                {
                    GameManager.playerLifeCount++;
                    stars = 0;
                }
                else stars++;
                GameManager.starCount = stars;

                break;
            case BonusType.bolt_gold:
                ActiveBolt(7);
                break;
            case BonusType.bolt_silver:
                ActiveBolt(5);
                break;
            case BonusType.bolt_bronze:
                ActiveBolt(3);
                break;
           
            case BonusType.thing_gold:
                ActiveThing(7);
                break;
            case BonusType.thing_silver:
                ActiveThing(5);
                break;
            case BonusType.thing_bronze:
                ActiveThing(3);
                break;
        }
        Destroy(gameObject);
	}
    void ActiveThing(float duration)//Увеличивает скорость атаки.
    {
        Shooting s = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        s.ActiveBonus(duration);
    }
    void ActiveBolt(float duration)//Увеличивает скорость передвижения.
    {
        PlayerController pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pc.ActiveBonus(duration);
    }
}
public enum BonusType { pill, shield, star, bolt_silver, bolt_gold, bolt_bronze, thing_gold, thing_silver, thing_bronze }

