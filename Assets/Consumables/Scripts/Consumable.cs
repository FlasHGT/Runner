using UnityEngine;

public class Consumable : MonoBehaviour
{
	public ConsumableType consumableType;

	public bool needsReset = false;

	[SerializeField] Sprite armorSprite = null;
	[SerializeField] Sprite invincibleSprite = null;
	[SerializeField] Sprite hpSprite = null;
	[SerializeField] Sprite ammoSprite = null;

	[SerializeField] GameObject arrow = null;

	private SpriteRenderer currentRenderer = null;

	private void Start()
	{
		consumableType = (ConsumableType)Random.Range(0, (float)ConsumableType.COUNT);

		currentRenderer = GetComponent<SpriteRenderer>();
	}

	private void FixedUpdate()
	{
		PointConsumableArrow();

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

	private void PointConsumableArrow()
	{
		if (transform.position.y - Player.Instance.transform.position.y <= 35f && transform.position.y - Player.Instance.transform.position.y >= 5f && !GameManager.Instance.gamePaused)
		{
			if (!arrow.activeInHierarchy)
			{
				arrow.SetActive(true);
			}

			Vector3 difference = transform.position - Player.Instance.transform.position;
			float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			arrow.transform.position = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 2.2f, Player.Instance.transform.position.z);
			arrow.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90f);
		}
		else
		{
			arrow.SetActive(false);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			switch (consumableType)
			{
				case ConsumableType.AddArmor:
					AudioManager.Instance.PlayArmorUp();
					break;
				case ConsumableType.AddHP:
					AudioManager.Instance.PlayHealthUp();
					break;
				case ConsumableType.AddAmmo:
					AudioManager.Instance.PlayAmmoUp();
					break;
				default:
					Debug.Log("Not a consumable");
					break;
			}
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
