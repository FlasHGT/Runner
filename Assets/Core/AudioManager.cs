using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance = null;

	[SerializeField] Slider uiSlider = null;
	[SerializeField] Slider musicSlider = null;
	[SerializeField] Slider afxSlider = null;

	[SerializeField] AudioSource uiSource = null;
	[SerializeField] AudioSource musicSource = null;
	[SerializeField] AudioSource afxSource = null;

	[SerializeField] AudioClip consumableHPClip = null;
	[SerializeField] AudioClip consumableInvincibleClip = null;
	[SerializeField] AudioClip consumableArmorClip = null;
	[SerializeField] AudioClip consumableAmmoClip = null;

	[SerializeField] AudioClip deathClip = null;
	[SerializeField] AudioClip armorHitClip = null;
	[SerializeField] AudioClip healthHitClip = null;
	[SerializeField] AudioClip obstacleHitByProjectile = null;
	[SerializeField] AudioClip[] shootClip = null;

	[SerializeField] AudioClip[] mainMenuAudioClips = null;

	private bool songNeeded = true;

	public void PlayButtonClick ()
	{
		uiSource.Play();
	}

	public void PlayDeathClip()
	{
		afxSource.clip = deathClip;
		afxSource.Play();
	}

	public void PlayObstacleHitByProjectile()
	{
		afxSource.clip = obstacleHitByProjectile;
		afxSource.Play();
	}

	public void PlayShootClip()
	{
		afxSource.clip = shootClip[Random.Range(0, shootClip.Length)];
		afxSource.Play();
	}

	public void PlayAddAmmo()
	{
		afxSource.clip = consumableAmmoClip;
		afxSource.Play();
	}

	public void PlayArmorHitClip()
	{
		afxSource.clip = armorHitClip;
		afxSource.Play();
	}

	public void PlayHealthHitClip()
	{
		afxSource.clip = healthHitClip;
		afxSource.Play();
	}

	public void PlayHealthRepairClip()
	{
		afxSource.clip = consumableHPClip;
		afxSource.Play();
	}

	public void PlayArmorUpClip()
	{
		afxSource.clip = consumableArmorClip;
		afxSource.Play();
	}

	public void PlayInvincibleClip()
	{
		afxSource.clip = consumableInvincibleClip;
		afxSource.Play();
	}

	public void SetSliderValues()
	{
		uiSlider.value = uiSource.volume;
		musicSlider.value = musicSource.volume;
		afxSlider.value = afxSource.volume;
	}

	public void SaveSliderValues()
	{
		uiSource.volume = uiSlider.value;
		musicSource.volume = musicSlider.value;
		afxSource.volume = afxSlider.value;
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
