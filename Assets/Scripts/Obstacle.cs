using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField] float removeTime = 25f;
	[SerializeField] Sprite[] obstacleSprites = null;

	private void Start()
	{
		SpriteRenderer currentSprite = GetComponent<SpriteRenderer>();
		currentSprite.sprite = obstacleSprites[Random.Range(0, obstacleSprites.Length)];

		transform.localScale = new Vector2(Random.Range(3f, 4f), Random.Range(3f, 4f));
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && !Player.Instance.isInvincible)
		{
			transform.gameObject.SetActive(false);
			if(Player.Instance.armorCount == 0)
			{
				Player.Instance.health -= 2f;
				CameraController.Instance.time += removeTime;
			}else
			{
				Player.Instance.armorCount -= 1;
			}
		}
	}
}
