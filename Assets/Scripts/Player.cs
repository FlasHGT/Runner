using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player Instance;

	public float speed = 2f;

	public int armorCount = 0;

	public bool mobileInput = false;
	public bool isInvincible = false;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	// Update is called once per frame
	private void FixedUpdate()
	{
		if(!GameManager.Instance.gamePaused)
		{
			Move();
		}
	}

	private void Move()
	{
		if(mobileInput)
		{
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);
				Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

				if (touchPos.x > 0)
				{
					transform.Translate(GameManager.Instance.realTimeSpeed, 0f, 0f);
				}
				else if (touchPos.x < 0)
				{
					transform.Translate(-GameManager.Instance.realTimeSpeed, 0f, 0f);
				}
			}

			transform.Translate(0f, GameManager.Instance.realTimeSpeed, 0f);
		}else
		{
			float moveHorizontal = Input.GetAxis("Horizontal");

			transform.Translate(moveHorizontal * GameManager.Instance.realTimeSpeed, GameManager.Instance.realTimeSpeed, 0f);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Wall"))
		{
			GameManager.Instance.ResetGame();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Consumable consumable = collision.GetComponent<Consumable>();
		switch (consumable.consumableType)
		{
			case ConsumableType.Invincible:
				collision.gameObject.SetActive(false);
				StartCoroutine(Invincible());
				consumable.needsReset = true;
				break;
			case ConsumableType.ResetCamera:
				collision.gameObject.SetActive(false);
				CameraController.Instance.time = -15f;
				consumable.needsReset = true;
				break;
			case ConsumableType.AddArmor:
				collision.gameObject.SetActive(false);
				armorCount = 3;
				consumable.needsReset = true;
				break;
			default:
				Debug.Log("This is not a consumable");
				break;
		}
	}

	IEnumerator Invincible ()
	{
		isInvincible = true;
		yield return new WaitForSeconds(3);
		isInvincible = false;
	}
}
