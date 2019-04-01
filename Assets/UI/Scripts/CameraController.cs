using UnityEngine;

public class CameraController : MonoBehaviour
{
	private BoxCollider2D boxCollider = null;

	// Start is called before the first frame update
	private void Start()
    {
		boxCollider = GetComponent<BoxCollider2D>();
		boxCollider.size = new Vector2((Camera.main.orthographicSize * 2f * Screen.width / Screen.height) - 3f, Camera.main.orthographicSize * 2f * Screen.height / Screen.height);
	}

    // Update is called once per frame
    private void FixedUpdate()
	{
		if (!GameManager.Instance.gamePaused)
		{
			MoveCamera();
		}
	}

	private void MoveCamera()
	{
		transform.Translate(0f, GameManager.Instance.realTimeSpeed, 0f);
	}
}
