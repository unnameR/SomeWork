using UnityEngine;

public class Key : RaycastBase
{
    public SoundSO sound;
    public LockTile lockTiles;

	void Update () 
    {
        if (OverlapCircleCast())
        {
            AudioManager._instance.PlaySoundEffect(sound);
            lockTiles.Unlock();
            Destroy(gameObject);
        }
	}
}
