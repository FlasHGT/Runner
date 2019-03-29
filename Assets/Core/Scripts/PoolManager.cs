using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	public static PoolManager Instance;

	private Dictionary<int, Queue<GameObject>> poolDictionary = new Dictionary<int, Queue<GameObject>>();

	private void Awake()
	{
		if (!Instance)
		{
			Instance = this;
		}
	}

	public void CreatePool (GameObject prefab, int poolSize)
	{
		int poolKey = prefab.GetInstanceID();

		if(!poolDictionary.ContainsKey(poolKey))
		{
			poolDictionary.Add(poolKey, new Queue<GameObject>());

			for (int i = 0; i < poolSize; i++)
			{
				GameObject newObject = Instantiate(prefab);
				newObject.SetActive(false);
				poolDictionary[poolKey].Enqueue(newObject);
			}
		}
	}

	public GameObject ReuseObject(GameObject prefab, Vector3 position)
	{
		int poolKey = prefab.GetInstanceID();
		if(poolDictionary.ContainsKey(poolKey))
		{
			GameObject objectToReuse = poolDictionary[poolKey].Dequeue();
			poolDictionary[poolKey].Enqueue(objectToReuse);

			objectToReuse.SetActive(true);
			objectToReuse.transform.position = position;

			return objectToReuse;
		}

		return null;
	}

}
