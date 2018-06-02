using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerGameParameters {

    //базовые параметры, не изменять извне
    public static bool isFirstStart = true;
    public static int pillCount = 0;
    public static int shieldCount = 0;
    public static int starCount = 0;
    public static int playerLifeCount = 3;
    public static float fireRate = 0.3f;
    public static float moveSpeed = 5f;
    public static string UIColor="Blue";
    public static int playerShip = 0;//max 3
    public static int enableColorCount = 1;//max 4, min 1
    public static int minScoreToUp = 100;
    public static int maxScore = 0;

    //возможно стоит сделать подругому
    public static int gradePower_Shield = 0;
    public static int gradePower_Star = 0;
    public static int gradePower_Pill = 0;
    public static int gradePower_Life = 0;
    public static int gradePower_NextShip = 0;
    public static int gradePower_NextColor = 0;

    public static void NewGame()
    {
        pillCount = 0;
        shieldCount = 0;
        starCount = 0;
        playerLifeCount = 3;
        //fireRate = 0.3f;
        //moveSpeed = 5f;
        UIColor = "Blue";
        playerShip = 0;
        enableColorCount = 1;//max 4, min 1
        minScoreToUp = 100;
        maxScore = 0;

        gradePower_Shield = 0;
        gradePower_Star = 0;
        gradePower_Pill = 0;
        gradePower_Life = 0;
        gradePower_NextShip = 0;
        gradePower_NextColor = 0;
    }
}
