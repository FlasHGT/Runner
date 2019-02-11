using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public bool gamePaused = false;

	public float realTimeSpeed;

	[SerializeField] ObstacleSpawner obstacleSpawner;
	[SerializeField] GameObject player = null;
	[SerializeField] GameObject wall = null;
	[SerializeField] GameObject loseScreen = null;

	public bool retryButtonPressed = false;

	public void ResetGame()
	{
		gamePaused = true;
		loseScreen.SetActive(true);

		if(retryButtonPressed)
		{
			gamePaused = false;
			int i = SceneManager.GetActiveScene().buildIndex;
			SceneManager.LoadScene(i);
			retryButtonPressed = false;
			loseScreen.SetActive(false);
		}
	}

	public void RetryButtonPressed()
	{
		retryButtonPressed = true;
	}

	public void QuitButtonPressed()
	{

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
		if (gamePaused)
		{
			ResetGame();
		}

		realTimeSpeed = Player.Instance.speed * Time.fixedDeltaTime;
	}
}
