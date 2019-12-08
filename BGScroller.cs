using UnityEngine;
using System.Collections;

public class Done_BGScroller : MonoBehaviour
{
	public float scrollSpeed;
	public float tileSizeZ;
    public Done_GameController gamecon;

	private Vector3 startPosition;

	void Start ()
	{
		startPosition = transform.position;
        gamecon = gamecon.GetComponent<Done_GameController>();
    }

	void Update ()
    { 
        if (gamecon.score >= 100)
            {
            scrollSpeed = -30;
            }

        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.forward * newPosition;
	}
}