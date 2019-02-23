using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField] float removeTime = 25f;

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
