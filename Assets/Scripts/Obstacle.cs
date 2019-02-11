using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField]float removeTime = 25f;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			transform.gameObject.SetActive(false);
			CameraController.Instance.time += removeTime;
		}
	}
}
