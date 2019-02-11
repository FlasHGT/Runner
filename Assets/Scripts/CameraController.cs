using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public static CameraController Instance = null;

	public float time = 0f;
	[SerializeField] float addTime = 2f;
	[SerializeField] float loseCameraSize = 5f;

	private float minCameraSize = 5f;
	private float maxCameraSize = 10f;

	float value; 

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
	}

	// Start is called before the first frame update
	private void Start()
    {
		value = maxCameraSize - minCameraSize;
	}

    // Update is called once per frame
    private void FixedUpdate()
	{
		if (!GameManager.Instance.gamePaused)
		{
			if (Camera.main.orthographicSize == loseCameraSize)
			{
				GameManager.Instance.ResetGame();
			}
			MoveCamera();
			ChangeCameraSize();
		}
	}

	private void ChangeCameraSize()
	{
		if (time < -15f)
		{
			time = -15f;
		}
		Camera.main.orthographicSize = Mathf.Lerp(maxCameraSize, minCameraSize, time * Time.fixedDeltaTime * 0.5f);
	}

	private void MoveCamera()
	{
		transform.Translate(0f, GameManager.Instance.realTimeSpeed, 0f);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Obstacle"))
		{
			time -= addTime;
		}
	}
}
