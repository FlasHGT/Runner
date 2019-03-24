using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
	public ConsumableType consumableType;

	public bool needsReset = false;

	[SerializeField] Sprite armorSprite = null;
	[SerializeField] Sprite invincibleSprite = null;
	[SerializeField] Sprite hpSprite = null;
	[SerializeField] Sprite ammoSprite = null;

	private SpriteRenderer currentRenderer = null;
	private BoxCollider2D boxCollider = null;

	private void Start()
	{
		consumableType = (ConsumableType)Random.Range(0, (float)ConsumableType.COUNT);

		currentRenderer = GetComponent<SpriteRenderer>();
		boxCollider = GetComponent<BoxCollider2D>();
	}

	private void FixedUpdate()
	{
		if(needsReset || Player.Instance.transform.position.y > transform.position.y + 12f)
		{
			consumableType = (ConsumableType)Random.Range(0, (int)ConsumableType.COUNT);
			switch (consumableType)
			{
				case ConsumableType.AddArmor:
					currentRenderer.sprite = armorSprite;
					break;
				case ConsumableType.Invincible:
					currentRenderer.sprite = invincibleSprite;
					break;
				case ConsumableType.AddHP:
					currentRenderer.sprite = hpSprite;
					break;
				case ConsumableType.AddAmmo:
					currentRenderer.sprite = ammoSprite;
					break;
				default:
					Debug.Log("Not a consumable");
					break;
			}

			needsReset = false;
		}
	}
}

public enum ConsumableType
{
	Invincible,
	AddArmor,
	AddHP,
	AddAmmo,
	COUNT
}
