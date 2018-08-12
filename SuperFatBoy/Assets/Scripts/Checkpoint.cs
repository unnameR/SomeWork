using UnityEngine;

public class Checkpoint : RaycastBase
{
    public SoundSO sound;
    public SpriteRenderer sr;
    public TextMesh timeTxt;

    bool isHit;

    void Update()
    {
        if (!isHit)
        {
            if(OverlapCircleCast())
            {
                LevelManager.instance.SetCheckpoint(transform);
                isHit = true;
                ShowTime(LevelManager.instance.GetTime());
            }
        }
    }
    public void SetActive()
    {
        isHit = false;
        sr.color = Color.white;
        timeTxt.gameObject.SetActive(false);
    }
    public void ShowTime(float time)
    {
        AudioManager._instance.PlaySoundEffect(sound);

        timeTxt.gameObject.SetActive(true);
        Color temp = sr.color;
        temp.a = 100;
        sr.color = new Color(1,1,1,0.4f);

        timeTxt.text = time.ToString("f2");
    }
    /*public void HideTime()
    {
        timeTxt.gameObject.SetActive(false);
        sr.color = Color.white;
    }*/
    /*public Transform GetCheckpointPos()
    {
        return transform;
    }*/
}
