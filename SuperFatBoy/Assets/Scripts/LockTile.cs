using System.Collections;
using UnityEngine;

public class LockTile : MonoBehaviour {

    public SoundSO sound;
    public float waitTime = 0.5f;
    Animator[] tilesAnim;

    bool isUnlock;
    void Awake()
    {
        tilesAnim = new Animator[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            tilesAnim[i] = transform.GetChild(i).GetComponent<Animator>();
        }
        PlayerSpawner.spawnEvent += Refresh;
    }
    public void Unlock()
    {
        isUnlock = true;
        StartCoroutine(Active());
    }
    void Refresh(Transform tr)//Открывать тайлы не после смерти, а после спауна игрока. Но тогда появляется ненужный трансформ..
    {
        if (isUnlock)
            StartCoroutine(Active());
    }
    IEnumerator Active()
    {
        for (int i = 0; i < tilesAnim.Length; i++)
        {
            yield return new WaitForSeconds(waitTime);
            AudioManager._instance.PlaySoundEffect(sound);
            tilesAnim[i].SetTrigger("unlock");
        }
    }
    void OnDisable()
    {
        PlayerSpawner.spawnEvent -= Refresh;
    }
}
