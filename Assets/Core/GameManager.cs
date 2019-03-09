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

	[Header("Main Menu")]
	public string[] quotes;

	[SerializeField] GameObject defaultScreen = null;
	[SerializeField] GameObject optionsScreen = null;

	[SerializeField] Text quoteText = null;

	private bool needQuote = true;

	[Header ("Game")]
	[SerializeField] Text scoreText = null;
	[SerializeField] Image healthImage = null;
	[SerializeField] Image armorImage = null;
	[SerializeField] Spawner obstacleSpawner = null;
	[SerializeField] GameObject loseScreen = null;

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
		AudioManager.Instance.buttonAudioSource.Play();
		retryButtonPressed = true;
	}

	public void QuitButtonPressed()
	{
		AudioManager.Instance.buttonAudioSource.Play();
		Application.Quit();
	}

	public void MainMenuButtonPressed()
	{
		AudioManager.Instance.buttonAudioSource.Play();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

	public void StartButtonPressed()
	{
		AudioManager.Instance.buttonAudioSource.Play();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void OptionsButtonPressed()
	{
		AudioManager.Instance.buttonAudioSource.Play();
		defaultScreen.SetActive(false);
		optionsScreen.SetActive(true);
	}

	public void OptionsButtonExit()
	{
		AudioManager.Instance.buttonAudioSource.Play();
		optionsScreen.SetActive(false);
		defaultScreen.SetActive(true);
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

		if (scoreText != null && armorImage != null && healthImage != null)
		{
			scoreText.text = "Score : " + Player.Instance.transform.position.y.ToString("f0");
			armorImage.fillAmount = Player.Instance.armorCount / 10f;
			healthImage.fillAmount = Player.Instance.health / 10f;
		}

		if (quoteText != null && quotes.Length != 0f && needQuote)
		{
			StartCoroutine(SwitchQuote());
		}

		if (Player.Instance != null)
		{
			realTimeSpeed = Player.Instance.speed * Time.fixedDeltaTime;
		}else
		{
			realTimeSpeed = 2f * Time.fixedDeltaTime;
		}
	}

	IEnumerator SwitchQuote()
	{
		needQuote = false;
		quoteText.text = quotes[Random.Range(0, quotes.Length)];
		yield return new WaitForSeconds(5f);
		needQuote = true;
	}
}
