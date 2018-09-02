using UnityEngine;

public class Key : RaycastBase
{
    public SoundSO sound;
    public LockTile lockTiles;

    //bool isDestroing;

	void Update () 
    {
        if (OverlapCircleCast())
        {
            AudioManager._instance.PlaySoundEffect(sound);
            lockTiles.Unlock();
            //if(isDestroing)
            Destroy(gameObject);
        }
	}
}
