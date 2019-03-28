using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player Instance;

	public float speed = 2f;
	public float health = 10f;
	public float armorCount = 10f;
	public float ammoCount = 0f;

	public bool mobileInput = false;
	public bool isInvincible = false;

	[SerializeField] AudioSource audioSource = null;

	[SerializeField] AudioClip[] shootClips = null;
	[SerializeField] AudioClip playerDeathClip = null;

	[SerializeField] Sprite defaultSprite = null;
	[SerializeField] Sprite[] armorLayers = null;

	[SerializeField] ParticleSystem enginePS = null;

	[SerializeField] GameObject shrapnelGO = null;
	[SerializeField] GameObject fireGO = null;
	[SerializeField] GameObject smokeGO = null;
	[SerializeField] GameObject lightGO = null;

	[SerializeField] GameObject projectile = null;

	private SpriteRenderer sR = null;

	private void Awake()
	{
		if (!Instance)
		{
			Instance = this;
		}
	}

	private void Start()
	{
		GetComponent<Animator>().enabled = false;

		sR = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	private void FixedUpdate()
	{
		if (transform.position.x >= 21f || transform.position.x <= -21f)
		{
			Death();
			GameManager.Instance.ResetGame();
		}

		if (health <= 0f)
		{
			Death();
			GameManager.Instance.ResetGame();
		}
		else if (health > 10f)
		{
			health = 10f;
		}

		if (!GameManager.Instance.gamePaused)
		{
			Shoot();
			Move();
		}

		if (armorCount == 10f)
		{
			sR.sprite = armorLayers[3];
		} else if (armorCount == 7.5f)
		{
			sR.sprite = armorLayers[2];
		}
		else if (armorCount == 5f)
		{
			sR.sprite = armorLayers[1];
		}
		else if (armorCount == 2.5f)
		{
			sR.sprite = armorLayers[0];
		}
		else if (armorCount == 0f)
		{
			sR.sprite = defaultSprite;	
		}
	}

	private void Death()
	{
		enginePS.Stop();
		sR.enabled = false;

		fireGO.transform.rotation = Quaternion.identity;
		smokeGO.transform.rotation = Quaternion.identity;
		lightGO.transform.rotation = Quaternion.identity;
		shrapnelGO.transform.rotation = Quaternion.identity;

		fireGO.SetActive(true);
		smokeGO.SetActive(true);
		lightGO.SetActive(true);
		shrapnelGO.SetActive(true);

		GetComponent<Animator>().enabled = true;
		audioSource.clip = playerDeathClip;
		audioSource.Play();
	}

	private void Move()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, -3f);
		transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

		if(mobileInput)
		{
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);
				Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

				if (touchPos.x > 0)
				{
					transform.localRotation = Quaternion.Euler(0f, 45f, 0f);
					transform.Translate(GameManager.Instance.realTimeSpeed, 0f, 0f);
				}
				else if (touchPos.x < 0)
				{
					transform.localRotation = Quaternion.Euler(0f, -45f, 0f);
					transform.Translate(-GameManager.Instance.realTimeSpeed, 0f, 0f);
				}
			}

			transform.Translate(0f, GameManager.Instance.realTimeSpeed, 0f);
		}else
		{
			float moveHorizontal = Input.GetAxis("Horizontal");
			if(moveHorizontal > 0)
			{
				transform.localRotation = Quaternion.Euler(0f, 45f, 0f);
			}
			else if(moveHorizontal < 0)
			{
				transform.localRotation = Quaternion.Euler(0f, -45f, 0f);
			}
			transform.Translate(moveHorizontal * GameManager.Instance.realTimeSpeed, GameManager.Instance.realTimeSpeed, 0f);
		}
	}

	private void Shoot()
	{
		if(Input.GetKeyDown(KeyCode.Space) && ammoCount != 0f)
		{
			ammoCount -= 2f;
			Instantiate(projectile, transform.position, Quaternion.identity);

			audioSource.clip = shootClips[Random.Range(0, shootClips.Length)];
			audioSource.Play();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Consumable consumable = collision.GetComponent<Consumable>();
		if(consumable)
		{
			switch (consumable.consumableType)
			{
				case ConsumableType.Invincible:
					collision.gameObject.SetActive(false);
					StartCoroutine(Invincible());
					consumable.needsReset = true;
					break;
				case ConsumableType.AddArmor:
					collision.gameObject.SetActive(false);
					armorCount = 10f;
					consumable.needsReset = true;
					break;
				case ConsumableType.AddHP:
					collision.gameObject.SetActive(false);
					health += 5;
					consumable.needsReset = true;
					break;
				case ConsumableType.AddAmmo:
					collision.gameObject.SetActive(false);
					ammoCount += 10f;
					consumable.needsReset = true;
					break;
				default:
					Debug.Log("This is not a consumable");
					break;
			}
		}
	}

	IEnumerator Invincible ()
	{
		isInvincible = true;
		Color tmp = sR.color;
		tmp.a = 0.5f;
		sR.color = tmp;
		yield return new WaitForSeconds(5);
		isInvincible = false;
		tmp.a = 1f;
		sR.color = tmp;
	}
}
