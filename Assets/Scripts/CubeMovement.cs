using UnityEngine;
using System.Collections;

public class CubeMovement : MonoBehaviour {
	public Vector3 StartCoord;
	public Vector3 EndCoord;
	bool forwardDirection = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (forwardDirection)
			transform.Translate(
				(transform.position.x > EndCoord.x + 0.1f ? -0.5f * Time.deltaTime : 
					(transform.position.x < EndCoord.x - 0.1f ? 0.5f * Time.deltaTime : 0.0f)),
				(transform.position.y > EndCoord.y + 0.1f ? -0.5f * Time.deltaTime : 
					(transform.position.y < EndCoord.y - 0.1f ? 0.5f * Time.deltaTime : 0.0f)),
				(transform.position.z > EndCoord.z + 0.1f ? -0.5f * Time.deltaTime : 
					(transform.position.z < EndCoord.z - 0.1f ? 0.5f * Time.deltaTime : 0.0f))
			);
		else
			transform.Translate(
				(transform.position.x > StartCoord.x + 0.1f ? -0.5f * Time.deltaTime : 
					(transform.position.x < StartCoord.x - 0.1f ? 0.5f * Time.deltaTime :0.0f)),
				(transform.position.y > StartCoord.y + 0.1f ? -0.5f * Time.deltaTime : 
					(transform.position.y < StartCoord.y - 0.1f ? 0.5f * Time.deltaTime : 0.0f)),
				(transform.position.z > StartCoord.z + 0.1f ? -0.5f * Time.deltaTime : 
					(transform.position.z < StartCoord.z - 0.1f ? 0.5f * Time.deltaTime : 0.0f))
			);

		if (Vector3.Distance(transform.position, EndCoord) < 0.1f)
			forwardDirection = false;
		else if (Vector3.Distance(transform.position, StartCoord) < 0.1f)
			forwardDirection = true;
	}
}
