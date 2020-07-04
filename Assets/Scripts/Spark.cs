using UnityEngine;
using System.Collections;

public class Spark : MonoBehaviour {
	Transform [] sparks;
	float startTime;

	// Use this for initialization
	void Start () {
		sparks = gameObject.GetComponentsInChildren<Transform> (true);
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		sparks[1].Translate(-0.1f * Time.deltaTime, -0.1f * Time.deltaTime, 0.0f);
		sparks[4].Translate(-0.1f * Time.deltaTime, -0.15f * Time.deltaTime, 0.0f);
		sparks[7].Translate(-0.1f * Time.deltaTime, -0.2f * Time.deltaTime, 0.0f);

		sparks[2].Translate(0.0f, -0.1f * Time.deltaTime, 0.0f);
		sparks[5].Translate(0.0f, -0.15f * Time.deltaTime, 0.0f);
		sparks[8].Translate(0.0f, -0.2f * Time.deltaTime, 0.0f);

		sparks[3].Translate(0.1f * Time.deltaTime, -0.1f * Time.deltaTime, 0.0f);
		sparks[6].Translate(0.1f * Time.deltaTime, -0.15f * Time.deltaTime, 0.0f);
		sparks[9].Translate(0.1f * Time.deltaTime, -0.2f * Time.deltaTime, 0.0f);

		if (Time.time - startTime > 10.0f)
			gameObject.SetActive(false);
	}

	public void SetSparks(Transform place) {
		transform.position = place.position;
		transform.eulerAngles = place.eulerAngles;
		startTime = Time.time;
		
		sparks[1].localPosition = new Vector3(-0.4f, 0.4f, 0.0f);
		sparks[2].localPosition = new Vector3(0.0f, 0.4f, 0.0f);
		sparks[3].localPosition = new Vector3(0.4f, 0.4f, 0.0f);
		sparks[4].localPosition = new Vector3(-0.4f, 0.0f, 0.0f);
		sparks[5].localPosition = new Vector3(0.0f, 0.0f, 0.0f);
		sparks[6].localPosition = new Vector3(0.4f, 0.0f, 0.0f);
		sparks[7].localPosition = new Vector3(-0.4f, -0.4f, 0.0f);
		sparks[8].localPosition = new Vector3(0.0f, -0.4f, 0.0f);
		sparks[9].localPosition = new Vector3(0.4f, -0.4f, 0.0f);
	}
}
