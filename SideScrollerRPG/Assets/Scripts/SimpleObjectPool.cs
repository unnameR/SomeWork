﻿using UnityEngine;
using System.Collections.Generic;

// A very simple object pooling class
public class SimpleObjectPool : MonoBehaviour
{
	// collection of currently inactive instances of the prefab
	private Stack<GameObject> inactiveInstances = new Stack<GameObject>();

	// Returns an instance of the prefab
    public GameObject GetObject(GameObject prefab) 
	{
		GameObject spawnedGameObject;

		// if there is an inactive instance of the prefab ready to return, return that
		if (inactiveInstances.Count > 0) 
		{
			// remove the instance from teh collection of inactive instances
			spawnedGameObject = inactiveInstances.Pop();//
		}
		// otherwise, create a new instance
		else 
		{
			spawnedGameObject = (GameObject)GameObject.Instantiate(prefab);
            spawnedGameObject.SetActive(false);
			// Check if we have a PooledObject component attached
			PooledObject pooledObject = spawnedGameObject.GetComponent<PooledObject> ();

			if (pooledObject == null) 
			{
				// add the PooledObject component to the prefab so we know it came from this pool
				pooledObject = spawnedGameObject.AddComponent<PooledObject>();
			}

			pooledObject.pool = this;
		}

		// put the instance in the root of the scene and enable it
		spawnedGameObject.transform.SetParent(null);
		//spawnedGameObject.SetActive(true);

		// return a reference to the instance
		return spawnedGameObject;
	}

	// Return an instance of the prefab to the pool
	public void ReturnObject(GameObject toReturn) 
	{
		PooledObject pooledObject = toReturn.GetComponent<PooledObject>();

		// if the instance came from this pool, return it to the pool
        if (pooledObject != null && pooledObject.pool == this)//toReturn.transform.SetParent(transform);
		{
			// make the instance a child of this and disable it
			toReturn.transform.SetParent(transform);//Ошибка. Обьект уничтожен, но пули пытаются на него вернутся.
			toReturn.SetActive(false);

			// add the instance to the collection of inactive instances
			inactiveInstances.Push(toReturn);
		}
		// otherwise, just destroy it
		else
		{
			Debug.LogWarning(toReturn.name + " was returned to a pool it wasn't spawned from! Destroying.");
			Destroy(toReturn);
		}
	}
}

