using UnityEngine;

public class Obstacle : MonoBehaviour
{ 
	[SerializeField] Sprite[] obstacleSprites = null;

	[SerializeField] GameObject destructionPS = null;

	private float rotateSpeed = 0f;

	private void Start()
	{
		rotateSpeed = Random.Range(-45f, 45f);

		SpriteRenderer currentSprite = GetComponent<SpriteRenderer>();
		currentSprite.sprite = obstacleSprites[Random.Range(0, obstacleSprites.Length)];

		transform.localScale = new Vector2(Random.Range(3f, 4f), Random.Range(3f, 4f));
	}

	private void FixedUpdate()
	{
		if(!GameManager.Instance.gamePaused)
		{
			transform.Rotate(new Vector3(0f, 0f, rotateSpeed * Time.fixedDeltaTime), Space.Self);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Consumable"))
		{
			transform.gameObject.SetActive(false);
		}
		else if(collision.gameObject.CompareTag("Projectile"))
		{
			Destroy(collision.gameObject);
			transform.gameObject.SetActive(false);
			GameObject newObject = Instantiate(destructionPS, transform.position, transform.rotation);
			Destroy(newObject, 1f);

			AudioManager.Instance.PlayHitByProjectile();
		}else if (collision.gameObject.CompareTag("Player") && !Player.Instance.isInvincible)
		{
			GameObject newObject = Instantiate(destructionPS, transform.position, transform.rotation);
			Destroy(newObject, 1f);
			transform.gameObject.SetActive(false);

			if (Player.Instance.armorCount <= 0)
			{
				Player.Instance.health -= 2f;

				AudioManager.Instance.PlayHitByPlayer();
			}
			else
			{
				Player.Instance.armorCount -= 2.5f;

				AudioManager.Instance.PlayHitByPlayerArmor();
			}
		}
		else if(!collision.gameObject.CompareTag("MainCamera"))
		{
			AudioManager.Instance.PlayHitInvincible();
		}
	}
}
