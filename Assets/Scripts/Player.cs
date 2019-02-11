using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player Instance;

	public float speed = 2f;

	public bool mobileInput = false;

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
}
