using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	// Start is called before the first frame update
	private void Start()
    {
		
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
