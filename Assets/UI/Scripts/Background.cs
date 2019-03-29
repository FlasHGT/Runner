using UnityEngine;

public class Background : MonoBehaviour
{
	public float parralax = 2f;

	private MeshRenderer mr = null;
	private Material background = null;

    // Start is called before the first frame update
    private void Start()
    {
		mr = GetComponent<MeshRenderer>();
		background = mr.material;

		transform.localScale = new Vector3(Camera.main.orthographicSize * 2f * Screen.width / Screen.height, Camera.main.orthographicSize * 2f * Screen.height / Screen.height, 1f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
		Vector2 offset = background.mainTextureOffset;

		offset.x = transform.position.x / transform.localScale.x / parralax;
		offset.y = transform.position.y / transform.localScale.y / parralax;

		background.mainTextureOffset = offset;
    }
}
