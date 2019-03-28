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

	[SerializeField] AudioSource uiSource = null;
	[SerializeField] AudioSource musicSource = null;
	[SerializeField] AudioSource obstacleSource = null;
	[SerializeField] AudioSource consumableSource = null;

	[SerializeField] AudioClip healthUpClip = null;
	[SerializeField] AudioClip armorUpClip = null;
	[SerializeField] AudioClip ammoUpClip = null;

	[SerializeField] AudioClip hitByProjectile = null;
	[SerializeField] AudioClip hitByPlayerWithArmor = null;
	[SerializeField] AudioClip hitByPlayerWithoutArmor = null;
	[SerializeField] AudioClip hitInvincible = null;

	[SerializeField] AudioClip[] mainMenuAudioClips = null;

	private bool songNeeded = true;

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
