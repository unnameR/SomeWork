using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour {

    public static DataController instance;

    PlayerParamToNonstatic playerParam=new PlayerParamToNonstatic();
    string path = "/PlayerParam.json";
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        LoadGame();
    }
    public void LoadGame()
    {
        string filePath = Application.dataPath + path;
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            playerParam = JsonUtility.FromJson<PlayerParamToNonstatic>(dataAsJson);
            playerParam.SetData();
            Debug.Log("Loaded");
        }
    }
    public void SaveGame()
    {
        playerParam.GetData();
        string dataAsJson = JsonUtility.ToJson(playerParam);
        string filePath = Application.dataPath + path;
        File.WriteAllText(filePath, dataAsJson);
    }
}
public class PlayerParamToNonstatic
{
    public bool isFirstStart = PlayerGameParameters.isFirstStart;
    public int pillCount = PlayerGameParameters.pillCount;
    public int shieldCount = PlayerGameParameters.shieldCount;
    public int starCount = PlayerGameParameters.starCount;
    public int playerLifeCount = PlayerGameParameters.playerLifeCount;
    public float fireRate = PlayerGameParameters.fireRate;
    public float moveSpeed = PlayerGameParameters.moveSpeed;
    public string UIColor = PlayerGameParameters.UIColor;
    public int playerShip = PlayerGameParameters.playerShip;//max 3
    public int enableColorCount = PlayerGameParameters.enableColorCount;//max 4, min 1
    public int minScoreToUp = PlayerGameParameters.minScoreToUp;
    public int maxScore = PlayerGameParameters.maxScore;

    public int gradePower_Shield = PlayerGameParameters.gradePower_Shield;
    public int gradePower_Star = PlayerGameParameters.gradePower_Star;
    public int gradePower_Pill = PlayerGameParameters.gradePower_Pill;
    public int gradePower_Life = PlayerGameParameters.gradePower_Life;
    public int gradePower_NextShip = PlayerGameParameters.gradePower_NextShip;
    public int gradePower_NextColor = PlayerGameParameters.gradePower_NextColor;

    public void GetData()
    {
        isFirstStart = PlayerGameParameters.isFirstStart;
        pillCount = PlayerGameParameters.pillCount;
        shieldCount = PlayerGameParameters.shieldCount;
        starCount = PlayerGameParameters.starCount;
        playerLifeCount = PlayerGameParameters.playerLifeCount;
        fireRate = PlayerGameParameters.fireRate;
        moveSpeed = PlayerGameParameters.moveSpeed;
        UIColor = PlayerGameParameters.UIColor;
        playerShip = PlayerGameParameters.playerShip;//max 3
        enableColorCount = PlayerGameParameters.enableColorCount;//max 4, min 1
        minScoreToUp = PlayerGameParameters.minScoreToUp;
        maxScore = PlayerGameParameters.maxScore;

        gradePower_Shield = PlayerGameParameters.gradePower_Shield;
        gradePower_Star = PlayerGameParameters.gradePower_Star;
        gradePower_Pill = PlayerGameParameters.gradePower_Pill;
        gradePower_Life = PlayerGameParameters.gradePower_Life;
        gradePower_NextShip = PlayerGameParameters.gradePower_NextShip;
        gradePower_NextColor = PlayerGameParameters.gradePower_NextColor;
    }
    public void SetData()
    {
        PlayerGameParameters.isFirstStart = isFirstStart;
        PlayerGameParameters.pillCount = pillCount;
        PlayerGameParameters.shieldCount = shieldCount;
        PlayerGameParameters.starCount = starCount;
        PlayerGameParameters.playerLifeCount = playerLifeCount;
        PlayerGameParameters.fireRate = fireRate;
        PlayerGameParameters.moveSpeed = moveSpeed;
        PlayerGameParameters.UIColor = UIColor;
        PlayerGameParameters.playerShip = playerShip;//max 3
        PlayerGameParameters.enableColorCount = enableColorCount;//max 4,
        PlayerGameParameters.minScoreToUp = minScoreToUp;
        PlayerGameParameters.maxScore = maxScore;

        PlayerGameParameters.gradePower_Shield = gradePower_Shield;
        PlayerGameParameters.gradePower_Star = gradePower_Star;
        PlayerGameParameters.gradePower_Pill = gradePower_Pill;
        PlayerGameParameters.gradePower_Life = gradePower_Life;
        PlayerGameParameters.gradePower_NextShip = gradePower_NextShip;
        PlayerGameParameters.gradePower_NextColor = gradePower_NextColor;
    }
}
