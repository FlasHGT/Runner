using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public float realTimeSpeed;

	public bool gamePaused = false;
	public bool retryButtonPressed = false;

	public void ResetGame()
	{
		gamePaused = true;
		UIManager.Instance.loseScreen.SetActive(true);

		if (retryButtonPressed)
		{
			gamePaused = false;
			int i = SceneManager.GetActiveScene().buildIndex;
			SceneManager.LoadScene(i);
			retryButtonPressed = false;
			UIManager.Instance.loseScreen.SetActive(false);
		}
	}

	private void Awake()
	{
		if(!Instance)
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

		if (Player.Instance != null)
		{
			realTimeSpeed = Player.Instance.speed * Time.fixedDeltaTime;
		}else
		{
			realTimeSpeed = 2f * Time.fixedDeltaTime;
		}
	}
}
