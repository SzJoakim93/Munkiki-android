using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour {

	float movementSpeed = 1.0f;
	float x;
	const float maxValue = 2 * Mathf.PI;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0.0f, movementSpeed * Time.deltaTime, 0.0f);
		x += 0.01f;

		if (x > maxValue)
			x = 0.0f;
		
		movementSpeed = Mathf.Cos(x);
	}
}
