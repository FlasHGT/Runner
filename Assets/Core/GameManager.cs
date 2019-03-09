using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public bool gamePaused = false;

	public float realTimeSpeed;

	[Header("Main Menu")]
	[SerializeField] GameObject defaultScreen = null;

	[Header ("Game")]
	[SerializeField] Text scoreText = null;
	[SerializeField] Image healthImage = null;
	[SerializeField] Image armorImage = null;
	[SerializeField] Spawner obstacleSpawner = null;
	[SerializeField] GameObject loseScreen = null;

	public bool retryButtonPressed = false;

	public void ResetGame()
	{
		gamePaused = true;
		loseScreen.SetActive(true);

		if (retryButtonPressed)
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
		Debug.Log("Application quit");
		Application.Quit();
	}

	public void MainMenuButtonPressed()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

	public void StartButtonPressed()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void OptionsButtonPressed()
	{
		Debug.Log("Options enabled");
		defaultScreen.SetActive(false);
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
		if(scoreText != null && armorImage != null && healthImage != null)
		{
			scoreText.text = "Score : " + Player.Instance.transform.position.y.ToString("f0");
			armorImage.fillAmount = Player.Instance.armorCount / 10f;
			healthImage.fillAmount = Player.Instance.health / 10f;
		}

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
