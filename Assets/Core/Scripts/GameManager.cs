using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public float realTimeSpeed;

	public bool mobileInput = false;
	public bool gamePaused = false;
	public bool retryButtonPressed = false;

	public Slider afxSlider = null;
	public Slider musicSlider = null;
	public Slider uiSlider = null;

	public void ResetGame()
	{
		gamePaused = true;
		UIManager.Instance.loseScreen.SetActive(true);
		PlayGamesController.Instance.PostToLeaderboard(long.Parse(Player.Instance.transform.position.y.ToString("f0")));

		if (retryButtonPressed)
		{
			UIManager.Instance.loseScreen.SetActive(false);
			int i = SceneManager.GetActiveScene().buildIndex;
			SceneManager.LoadScene(i);
			retryButtonPressed = false;
			gamePaused = false;
		}
	}

	public void AuthenticateUser()
	{
		PlayGamesController.Instance.AuthenticateUser();
	}

	public void ShowLeaderboardUI()
	{
		PlayGamesController.Instance.ShowLeaderboardUI();
	}

	public void SetSliderValues()
	{
		AudioManager.Instance.SetSliderValues();
	}

	public void GetSliderValues()
	{
		AudioManager.Instance.GetSlidersValues();
	}

	public void PlayDeathSound()
	{
		AudioManager.Instance.PlayDeathSound();
	}

	public void PlayUISound ()
	{
		AudioManager.Instance.PlayButtonClick();
	}

	private void Awake()
	{
		if(!Instance)
		{
			Instance = this;
		}
	}

	private void Start()
	{
		if (SceneManager.GetActiveScene().buildIndex == 0)
		{
			SetSliderValues();
		}
	}

	private void FixedUpdate()
	{
		if (gamePaused)
		{
			ResetGame();
		}

		if (Player.Instance)
		{
			float playerSpeed = Player.Instance.speed;

			for (int i = 250; i < Player.Instance.transform.position.y; i += 250)
			{
				playerSpeed += 1f;
			}

			realTimeSpeed = playerSpeed * Time.fixedDeltaTime;
		}
		else
		{
			realTimeSpeed = 2f * Time.fixedDeltaTime;
		}
	}
}
