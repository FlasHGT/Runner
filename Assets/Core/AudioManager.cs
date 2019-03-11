using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance = null;

	[SerializeField] AudioSource buttonAudioSource = null;
	[SerializeField] AudioSource musicAudioSource = null;
	[SerializeField] AudioSource afxAudioSource = null;

	[SerializeField] AudioClip deathClip = null;
	[SerializeField] AudioClip armorHitClip = null;
	[SerializeField] AudioClip healthHitClip = null;
	[SerializeField] AudioClip[] mainMenuAudioClips = null;

	private bool songNeeded = true;

	public void PlayButtonClick ()
	{
		buttonAudioSource.Play();
	}

	public void PlayDeathClip()
	{
		afxAudioSource.clip = deathClip;
		afxAudioSource.Play();
	}

	public void PlayArmorHitClip()
	{
		afxAudioSource.clip = armorHitClip;
		afxAudioSource.Play();
	}

	public void PlayHealthHitClip()
	{
		afxAudioSource.clip = healthHitClip;
		afxAudioSource.Play();
	}

	private void Awake()
	{
		if(Instance == null) 
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
		if (songNeeded && musicAudioSource)
		{
			musicAudioSource.clip = mainMenuAudioClips[Random.Range(0, mainMenuAudioClips.Length)];
			musicAudioSource.Play();
			StartCoroutine(SwitchSong(musicAudioSource.clip.length));
		}
	}

	IEnumerator SwitchSong(float clipLength)
	{
		songNeeded = false;
		yield return new WaitForSeconds(clipLength);
		songNeeded = true;
	}
}
