using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance = null;

	public bool hasVisitedMainMenu = false;

	[SerializeField] AudioSource uiSource = null;
	[SerializeField] AudioSource musicSource = null;
	[SerializeField] AudioSource obstacleSource = null;
	[SerializeField] AudioSource consumableSource = null;
	[SerializeField] AudioSource projectileSource = null;

	[SerializeField] AudioClip playerDeathClip = null;
	[SerializeField] AudioClip healthUpClip = null;
	[SerializeField] AudioClip armorUpClip = null;
	[SerializeField] AudioClip ammoUpClip = null;
	[SerializeField] AudioClip[] shootClips = null;

	[SerializeField] AudioClip hitByProjectile = null;
	[SerializeField] AudioClip hitByPlayerWithArmor = null;
	[SerializeField] AudioClip hitByPlayerWithoutArmor = null;
	[SerializeField] AudioClip hitInvincible = null;

	[SerializeField] AudioClip[] mainMenuAudioClips = null;

	private bool songNeeded = true;

	public void GetSlidersValues()
	{
		PlayerPrefs.SetFloat("uiSlider", GameManager.Instance.uiSlider.value);
		PlayerPrefs.SetFloat("musicSlider", GameManager.Instance.musicSlider.value);
		PlayerPrefs.SetFloat("afxSlider", GameManager.Instance.afxSlider.value);
	}

	public void SetSliderValues()
	{
		if(hasVisitedMainMenu)
		{
			GameManager.Instance.uiSlider.value = PlayerPrefs.GetFloat("uiSlider");
			GameManager.Instance.musicSlider.value = PlayerPrefs.GetFloat("musicSlider");
			GameManager.Instance.afxSlider.value = PlayerPrefs.GetFloat("afxSlider");
		}
	}

	public void PlayDeathSound()
	{
		projectileSource.clip = playerDeathClip;
		projectileSource.Play();
	}

	public void PlayProjectileShot()
	{
		projectileSource.clip = shootClips[Random.Range(0, shootClips.Length)];
		projectileSource.Play();
	}

	public void PlayButtonClick ()
	{
		uiSource.Play();
	}

	public void PlayHitByProjectile()
	{
		obstacleSource.clip = hitByProjectile;
		obstacleSource.Play();
	}

	public void PlayHitByPlayerArmor()
	{
		obstacleSource.clip = hitByPlayerWithArmor;
		obstacleSource.Play();
	}

	public void PlayHitByPlayer()
	{
		obstacleSource.clip = hitByPlayerWithoutArmor;
		obstacleSource.Play();
	}

	public void PlayHitInvincible()
	{
		obstacleSource.clip = hitInvincible;
		obstacleSource.Play();
	}

	public void PlayHealthUp()
	{
		consumableSource.clip = healthUpClip;
		consumableSource.Play();
	}

	public void PlayAmmoUp()
	{
		consumableSource.clip = ammoUpClip;
		consumableSource.Play();
	}

	public void PlayArmorUp()
	{
		consumableSource.clip = armorUpClip;
		consumableSource.Play();
	}

	public void SetAudioSourcesValues()
	{
		uiSource.volume = GameManager.Instance.uiSlider.value;
		musicSource.volume = GameManager.Instance.musicSlider.value;
		consumableSource.volume = GameManager.Instance.afxSlider.value;
		projectileSource.volume = GameManager.Instance.afxSlider.value;
		obstacleSource.volume = GameManager.Instance.afxSlider.value;
	}

	private void Awake()
	{
		if(!Instance) 
		{
			Instance = this;
			DontDestroyOnLoad(this);
			return;
		}
		Destroy(this);
	}

	// Start is called before the first frame update
	private void Start()
	{
		
	}

    // Update is called once per frame
    private void FixedUpdate()
	{
		int i = SceneManager.GetActiveScene().buildIndex;
		if (i == 0)
		{
			SetAudioSourcesValues();
		}

		if (songNeeded && musicSource)
		{
			musicSource.clip = mainMenuAudioClips[Random.Range(0, mainMenuAudioClips.Length)];
			musicSource.Play();
			StartCoroutine(SwitchSong(musicSource.clip.length));
		}
	}

	IEnumerator SwitchSong(float clipLength)
	{
		songNeeded = false;
		yield return new WaitForSeconds(clipLength);
		songNeeded = true;
	}
}
