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

	[SerializeField] Sprite defaultSprite = null;
	[SerializeField] Sprite[] armorLayers = null;

	[SerializeField] ParticleSystem enginePS = null;

	[SerializeField] GameObject shrapnelGO = null;
	[SerializeField] GameObject fireGO = null;
	[SerializeField] GameObject smokeGO = null;
	[SerializeField] GameObject lightGO = null;

	[SerializeField] GameObject projectile = null;

	private SpriteRenderer sR = null;
	private Color tmp;

	private bool hasCalled = false;

	public void Death()
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

		AudioManager.Instance.PlayDeathSound();
		GetComponent<Animator>().enabled = true;
	}

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
		if (health <= 0f)
		{
			if(!hasCalled)
			{
				Death();
				GameManager.Instance.ResetGame();
				hasCalled = true;
			}
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
		}
		else if (armorCount == 7.5f)
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
		if(ammoCount > 10)
		{
			ammoCount = 10;
		}

		if(mobileInput)
		{
			if(Input.touchCount == 2 && ammoCount != 0f)
			{
				Touch touch1 = Input.GetTouch(0);
				Touch touch2 = Input.GetTouch(1);

				if(touch1.position.x < 0 && touch2.position.x > 0 || touch1.position.x > 0 && touch2.position.x < 0)
				{
					ammoCount -= 2f;
					Instantiate(projectile, transform.position, Quaternion.identity);

					AudioManager.Instance.PlayProjectileShot();
				}
			}
		}
		else
		{
			if (Input.GetKeyUp(KeyCode.Space) && ammoCount != 0f)
			{
				ammoCount -= 2f;
				Instantiate(projectile, transform.position, Quaternion.identity);

				AudioManager.Instance.PlayProjectileShot();
			}
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
					if(armorCount > 10f)
					{
						armorCount = 10f;
					}
					consumable.needsReset = true;
					break;
				case ConsumableType.AddHP:
					collision.gameObject.SetActive(false);
					health += 10;
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

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("MainCamera"))
		{
			Death();
			GameManager.Instance.ResetGame();
		}
	}

	private IEnumerator Blink()
	{
		for (int x = 0; x < 20; x++)
		{
			if (tmp.a == 0.25f)
			{
				tmp.a = 1f;
			}
			else
			{
				tmp.a = 0.25f;
			}

			if(x != 19)
			{
				sR.color = tmp;
			}
			else
			{
				isInvincible = false;
				tmp.a = 1.0f;
				sR.color = tmp;
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	private IEnumerator Invincible ()
	{
		tmp = sR.color;
		tmp.a = 0.25f;
		sR.color = tmp;
		isInvincible = true;
		yield return new WaitForSeconds(3f);
		StartCoroutine(Blink());
	}
}
