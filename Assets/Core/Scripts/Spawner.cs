using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject prefab = null;

	public float safezone = 15f;
	public float distanceBetweenObstacles = 5f;
	public float startSpawnY;
	public float currentSpawnY = 5f;
	public int obstaclesOnScreen = 10;

	private float minX;
	private float maxX;

	// Start is called before the first frame update
	private void Start()
    {
		startSpawnY = currentSpawnY;
		PoolManager.Instance.CreatePool(prefab, obstaclesOnScreen);
	}

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(currentSpawnY - obstaclesOnScreen * distanceBetweenObstacles < Player.Instance.transform.position.y - safezone)
		{
			ActivateObstacle();
		}
	}

	private void ActivateObstacle()
	{
		minX = -(Camera.main.orthographicSize * 2f * Screen.width / Screen.height / 2);
		maxX = Camera.main.orthographicSize * 2f * Screen.width / Screen.height / 2;
		float xPos = Random.Range(minX, maxX);
		PoolManager.Instance.ReuseObject(prefab, new Vector3(xPos, currentSpawnY, 0f));
		currentSpawnY += distanceBetweenObstacles;
	}
}
