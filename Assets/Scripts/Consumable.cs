using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
	public ConsumableType consumableType;

	public bool needsReset = false;

	private void Start()
	{
		consumableType = (ConsumableType)Random.Range(0, 2);
	}

	private void FixedUpdate()
	{
		if(needsReset)
		{
			consumableType = (ConsumableType)Random.Range(0, 2);
			needsReset = false;
		}
	}
}

public enum ConsumableType
{
	Invincible,
	ResetCamera,
	AddArmor
}
