using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public float realTimeSpeed;

	public ObstacleSpawner obstacleSpawner;
	public GameObject player = null;
	public GameObject wall = null;

	public void ResetGame()
	{
		player.transform.position = Vector3.zero;
		wall.transform.position = Vector3.zero;
		Camera.main.transform.position = new Vector3(0f, 0f, -10f);
		obstacleSpawner.currentSpawnY = obstacleSpawner.startSpawnY;
	}

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
	}

	private void FixedUpdate()
	{
		realTimeSpeed = Player.Instance.speed * Time.fixedDeltaTime;
	}
}
