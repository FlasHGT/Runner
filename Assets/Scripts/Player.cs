﻿using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player Instance;

	public float speed = 2f;
	public float health = 10f;

	public float armorCount = 10f;

	public bool mobileInput = false;
	public bool isInvincible = false;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	private void Start()
	{
		
	}

	// Update is called once per frame
	private void FixedUpdate()
	{
		if(health <= 0f)
		{
			GameManager.Instance.ResetGame();
		}

		if(!GameManager.Instance.gamePaused)
		{
			Move();
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
			case ConsumableType.AddArmor:
				collision.gameObject.SetActive(false);
				armorCount = 10f;
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
