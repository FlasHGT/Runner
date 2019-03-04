using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
	
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if(!GameManager.Instance.gamePaused)
		{
			transform.localScale = new Vector3(Camera.main.orthographicSize * 2f * Screen.width / Screen.height - 2, Camera.main.orthographicSize * 2f * Screen.height / Screen.height, 1f);

			transform.Translate(0f, GameManager.Instance.realTimeSpeed, 0f);
		}
	}
}
