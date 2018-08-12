using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    public static event System.Action<Transform> spawnEvent;
    public GameObject playerPref;
    public LevelManager lManager;

    Transform currentSpawnPoint;
    bool haveCheckpoint;


	public void SetStartSpawnPoint (Transform point) 
    {
        currentSpawnPoint = point; 
        SpawnPlayer();
	}
    public void SetCurrentSpawnPoint(Transform point)
    {        
         Checkpoint current = currentSpawnPoint.GetComponent<Checkpoint>();
         if (current != null)//проблема в стартовой позиции. ОНа не чекпоинт, но записывается как чекпоинт..
             current.SetActive();

        currentSpawnPoint = point;
        haveCheckpoint = true;
    }
    
    public void SpawnPlayer()
    {
        Checkpoint current = currentSpawnPoint.GetComponent<Checkpoint>();
        if (current != null)//проблема в стартовой позиции. ОНа не чекпоинт, но записывается как чекпоинт..
            current.SetActive();

        GameObject player = Instantiate(playerPref, currentSpawnPoint.position, currentSpawnPoint.rotation);
        spawnEvent(player.transform);

        ReplayManager.instance.SetRecordObj(player.transform);
        ReplayManager.instance.StartRecording();
        
    }
    public bool HaveCheckpoint()
    {
        return haveCheckpoint;
    }
}
