using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance = null;

	[Header("Main Menu")]
	public string[] quotes;

	[SerializeField] GameObject defaultScreen = null;
	[SerializeField] GameObject optionsScreen = null;
	[SerializeField] GameObject tutorialScreen = null;

	[SerializeField] Text quoteText = null;

	private bool needQuote = true;

	[Header("Game")]
	public GameObject loseScreen = null;

	[SerializeField] Text scoreText = null;

	[SerializeField] Image healthImage = null;
	[SerializeField] Image armorImage = null;
	[SerializeField] Image ammoImage = null;

	[Header("Tutorial")]
	[SerializeField] Image coreImage = null;
	[SerializeField] Image consumableImage = null;

	public void RetryButtonPressed()
	{
		AudioManager.Instance.PlayButtonClick();
		GameManager.Instance.retryButtonPressed = true;
	}

	public void QuitButtonPressed()
	{
		AudioManager.Instance.PlayButtonClick();
		Application.Quit();
	}

	public void MainMenuButtonPressed()
	{
		AudioManager.Instance.PlayButtonClick();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		AudioManager.Instance.hasVisitedMainMenu = true;
	}

	public void StartButtonPressed()
	{
		AudioManager.Instance.PlayButtonClick();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void TutorialButtonPressed()
	{
		AudioManager.Instance.PlayButtonClick();
		defaultScreen.SetActive(false);
		tutorialScreen.SetActive(true);
	}

	public void TutorialButtonExit()
	{
		AudioManager.Instance.PlayButtonClick();
		tutorialScreen.SetActive(false);
		defaultScreen.SetActive(true);
	}

	public void TutorialChangeImage()
	{
		AudioManager.Instance.PlayButtonClick();

		if (coreImage.gameObject.activeInHierarchy)
		{
			coreImage.gameObject.SetActive(false);
			consumableImage.gameObject.SetActive(true);
		}else
		{
			consumableImage.gameObject.SetActive(false);
			coreImage.gameObject.SetActive(true);
		}
	}

	public void OptionsButtonPressed()
	{
		AudioManager.Instance.PlayButtonClick();
		defaultScreen.SetActive(false);
		optionsScreen.SetActive(true);
	}

	public void OptionsButtonExit()
	{
		AudioManager.Instance.PlayButtonClick();
		optionsScreen.SetActive(false);
		defaultScreen.SetActive(true);
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

		if (scoreText != null && armorImage != null && healthImage != null)
		{
			if (Player.Instance.transform.position.y >= 0f)
			{
				scoreText.text = "Score : " + Player.Instance.transform.position.y.ToString("f0");
			}
			armorImage.fillAmount = Player.Instance.armorCount / 10f;
			healthImage.fillAmount = Player.Instance.health / 10f;
			ammoImage.fillAmount = Player.Instance.ammoCount / 10f;
		}

		if (quoteText != null && quotes.Length != 0f && needQuote)
		{
			StartCoroutine(SwitchQuote());
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
