using UnityEngine;
using System.Collections;

public class Munkiki : MonoBehaviour {
	public Transform FrontSide;
	public Transform BackSide;
	public Transform Bottom;
	public Transform Camera;
	public LevelManager LevelManager;

	float actionCount = 0.0f;
	float jumpCount = 0.0f;
	int action = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (actionCount > 0.0f) {

			switch (action) {
				case 1:
					transform.Translate(0.0f, 0.0f, 1.5f * Time.deltaTime);
					actionCount -= 1.5f * Time.deltaTime;
					break;
				case 2:
					transform.Translate(0.0f, 0.0f, -1.5f * Time.deltaTime);
					actionCount -= 1.5f * Time.deltaTime;
					break;
				case 3:
					transform.Rotate(0.0f, 100.0f * Time.deltaTime, 0.0f);
					actionCount -=  100.0f * Time.deltaTime;
					break;
				case 4:
					transform.Rotate(0.0f, -100.0f * Time.deltaTime, 0.0f);
					actionCount -=  100.0f * Time.deltaTime;
					break;
				case 5:

				if (transform.position.y < -0.2f) {
					Camera.SetParent(null);
					transform.Translate(0.0f,  -0.5f * Time.deltaTime, 0.0f);

				} else {
					transform.Translate(0.0f,  -2.0f * Time.deltaTime, 0.0f);
					actionCount -= 2.0f * Time.deltaTime;
				}
						
					

					

					break;
			}
				
			if (actionCount < 0.0f) {

				if (action == 3 || action == 4)
					fixRotate();

				if ((action == 1 || action == 2 || action == 5) && !ObjectCollision(Bottom))
					fall();
				else {
					actionCount = 0.0f;
					action = 0;
				}
				
			}
				
		}

		if (jumpCount > 0.0f) {
			transform.Translate(0.0f,  2.5f * Time.deltaTime, 0.0f);
			jumpCount -= 2.5f * Time.deltaTime;
		}
	}

	public void Forward() {
		if (action == 0) {

			int tileState = ObjectCollisonForward();

			if (tileState < 2) {

				if (tileState == 1) {
					jump();
					Debug.Log("Jumping");
				}
					

				actionCount = 1.0f;
				action = 1;
			}
				
		}
	}

	public void Backward() {
		if (action == 0 && !ObjectCollision(BackSide)) {
			actionCount = 1.0f;
			action = 2;
		}
	}

	public void TurnLeft() {
		if (action == 0) {
			actionCount = 90.0f;
			action = 4;
		}
	}

	public void TurnRight() {
		if (action == 0) {
			actionCount = 90.0f;
			action = 3;
		}	
	}

	public void Push() {

	}

	public void Hit() {
		
	}

	void fixRotate() {
		if (transform.eulerAngles.y > 80 && transform.eulerAngles.y < 120)
			transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
		else if (transform.eulerAngles.y > 160 && transform.eulerAngles.y < 200)
			transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
		else if (transform.eulerAngles.y > 250 && transform.eulerAngles.y < 290)
			transform.eulerAngles = new Vector3(0.0f, 270.0f, 0.0f);
		else if (transform.eulerAngles.y > 340 || transform.eulerAngles.y < 20)
			transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);		
	}

	int ObjectCollisonForward() {
		foreach (var tile in LevelManager.Tiles)
			if (Vector3.Distance(FrontSide.position, tile.position) < 0.5f) {
				if (tile.childCount > 0)
					return 2;
				else
					return 1;
			}
				
		return 0;
	}

	bool ObjectCollision(Transform obj) {
		foreach (var tile in LevelManager.Tiles)
			if (Vector3.Distance(obj.position, tile.position) < 0.5f)
				return true;
		return false;
	}

	void jump() {
		jumpCount = 1.0f;
	}

	void fall() {
		action = 5;
		actionCount = 1.0f;
	}
}
