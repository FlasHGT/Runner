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

	[SerializeField] Spawner obstacleSpawner;
	[SerializeField] GameObject loseScreen = null;
	[SerializeField] Text scoreText = null;
	[SerializeField] Image healthImage = null;
	[SerializeField] Image armorImage = null;

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
		scoreText.text = "Score: " + Player.Instance.transform.position.y.ToString("f0");
		armorImage.fillAmount = Player.Instance.armorCount / 10f;
		healthImage.fillAmount = Player.Instance.health / 10f;

		if (gamePaused)
		{
			ResetGame();
		}

		realTimeSpeed = Player.Instance.speed * Time.fixedDeltaTime;
	}
}
