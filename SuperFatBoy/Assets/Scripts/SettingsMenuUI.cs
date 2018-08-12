using UnityEngine.UI;
using UnityEngine;

public class SettingsMenuUI : MonoBehaviour {

    public GameParamSO gameParam;
    public GameObject[] soundMasks;
    public GameObject[] musicMasks;
    public GameObject[] controllMasks;

    public RectTransform[] playerControllersUI;

    int currSound;
    int currMusic;
    int currController;

    void OnEnable()
    {
        //convert param
        currSound = Mathf.RoundToInt(gameParam.soundVolume * soundMasks.Length);
        currMusic = Mathf.RoundToInt(gameParam.musicVolume * musicMasks.Length);
        currController = gameParam.controllSize;
        SetSound();
        SetMusic();
        SetController();
    }
    public void ChangeSound(bool isLeft)
    {
        if (isLeft && currSound > 0)
            currSound--;

        if (!isLeft && currSound < soundMasks.Length)
            currSound++;

        SetSound();
        gameParam.soundVolume = (float)currSound / soundMasks.Length;
    }
    public void ChangeMusic(bool isLeft)
    {
        if (isLeft && currMusic > 0)
            currMusic--;

        if (!isLeft && currMusic < musicMasks.Length)
            currMusic++;

        SetMusic();
        gameParam.musicVolume = (float)currMusic / musicMasks.Length;
    }
    public void ChangeController(bool isLeft)
    {
        if (isLeft && currController > 0)
            currController--;

        if (!isLeft && currController < controllMasks.Length)
            currController++;

        SetController();

        gameParam.controllSize = currController;
    }
    
    void SetSound()
    {
        for (int i = 0; i < soundMasks.Length; i++)
        {
            soundMasks[i].SetActive(i < currSound);
        }
    }
    void SetMusic()
    {
        for (int i = 0; i < musicMasks.Length; i++)
        {
            musicMasks[i].SetActive(i < currMusic);
        }
    }
    void SetController()
    {
        for (int i = 0; i < controllMasks.Length; i++)
        {
            controllMasks[i].SetActive(i < currController);
        }
        foreach (var ui in playerControllersUI)
        {
            ui.sizeDelta = new Vector2(80 + 10 * currController, 80 + 10 * currController);
        }
    }
}
