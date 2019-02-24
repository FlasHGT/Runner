using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
	public ConsumableType consumableType;

	public bool needsReset = false;

	[SerializeField] Sprite armorSprite = null;
	[SerializeField] Sprite invincibleSprite = null;
	[SerializeField] Sprite resetCameraSprite = null;

	private SpriteRenderer currentRenderer = null;
	private BoxCollider2D boxCollider = null;

	private void Start()
	{
		consumableType = (ConsumableType)Random.Range(0, 2);

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
					boxCollider.size = new Vector2(0.52f, 0.51f);
					boxCollider.offset = new Vector2(-0.01f, 0.03f);
					break;
				case ConsumableType.Invincible:
					currentRenderer.sprite = invincibleSprite;
					boxCollider.size = new Vector2(0.43f, 0.54f);
					boxCollider.offset = new Vector2(-0.04f, 0.09f);
					break;
				case ConsumableType.ResetCamera:
					currentRenderer.sprite = resetCameraSprite;
					boxCollider.size = new Vector2(0.44f, 0.39f);
					boxCollider.offset = new Vector2(0f, 0.03f);
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
	ResetCamera,
	AddArmor,
	COUNT
}
