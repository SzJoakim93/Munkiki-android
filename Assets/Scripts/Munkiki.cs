using UnityEngine;
using System.Collections;

public class Munkiki : MonoBehaviour {
	public Transform FrontSide;
	public Transform BackSide;
	public Transform Bottom;
	public Transform UpperFront;
	public Transform LowerFront;
	public Transform FarFront;
	public Transform FarUpperFront;
	public Transform FarLowerFront;
	public Transform Camera;
	public LevelManager LevelManager;
	public Spark Spark;
	CubeManager cubeManager;

	float actionCount = 0.0f;
	float jumpCount = 0.0f;
	int action = 0;

	// Use this for initialization
	void Start () {
		cubeManager = new CubeManager(LevelManager);
	}
	
	// Update is called once per frame
	void Update () {
		if (actionCount > 0.0f) {

			switch (action) {
				case 1: //step forward
					transform.Translate(0.0f, 0.0f, 1.5f * Time.deltaTime);
					actionCount -= 1.5f * Time.deltaTime;
					break;
				case 2: //step backward
					transform.Translate(0.0f, 0.0f, -1.5f * Time.deltaTime);
					actionCount -= 1.5f * Time.deltaTime;
					break;
				case 3: //turn right
					transform.Rotate(0.0f, 100.0f * Time.deltaTime, 0.0f);
					actionCount -=  100.0f * Time.deltaTime;
					break;
				case 4: //turn left
					transform.Rotate(0.0f, -100.0f * Time.deltaTime, 0.0f);
					actionCount -=  100.0f * Time.deltaTime;
					break;
				case 5: //fall

					if (transform.position.y < -0.2f) {
						Camera.SetParent(null);
						transform.Translate(0.0f,  -0.5f * Time.deltaTime, 0.0f);

					} else {
						transform.Translate(0.0f,  -2.0f * Time.deltaTime, 0.0f);
						actionCount -= 2.0f * Time.deltaTime;
					}

					break;
				case 6: //cross jump
					transform.Translate(0.0f, 0.0f, 3.0f * Time.deltaTime);
					actionCount -= 1.5f * Time.deltaTime;
					break;
				case 7: //pushing
					transform.Translate(0.0f, 0.0f, 1.0f * Time.deltaTime);

					if (Mathf.RoundToInt(transform.eulerAngles.y) == 0)
						cubeManager.PushableObj.Translate(0.0f, 0.0f, 1.0f * Time.deltaTime);
					else if (Mathf.RoundToInt(transform.eulerAngles.y) == 270)
						cubeManager.PushableObj.Translate(-1.0f * Time.deltaTime, 0.0f, 0.0f);
					else if (Mathf.RoundToInt(transform.eulerAngles.y) == 180)
						cubeManager.PushableObj.Translate(0.0f, 0.0f, -1.0f * Time.deltaTime);
					else if (Mathf.RoundToInt(transform.eulerAngles.y) == 90)
						cubeManager.PushableObj.Translate(1.0f * Time.deltaTime, 0.0f, 0.0f);

					actionCount -= 1.0f * Time.deltaTime;
					break;
			}
				
			if (actionCount < 0.0f) {

				if (action == 3 || action == 4)
					fixRotate();

				if ((action == 1 || action == 2 || action == 5) && !cubeManager.ObjectCollision(Bottom.position))
					fall();
				else {
					if (action == 7) {
						cubeManager.PushableObj.position = new Vector3(
							Mathf.Round(cubeManager.PushableObj.position.x), cubeManager.PushableObj.position.y, Mathf.Round(cubeManager.PushableObj.position.z));

						if (!cubeManager.ObjectCollision(LowerFront.position))
							cubeManager.invokeCubeFall(cubeManager.PushableObj);
						else
							cubeManager.PushableObj.SetParent(cubeManager.CollidedObj);
					}
	
					actionCount = 0.0f;
					action = 0;
				}
				
				foreach (var note in LevelManager.MusicalNotes) {
					if (Vector3.Distance(transform.position, note.position) < 0.4f) {
						LevelManager.MusicalNotes.Remove(note);
						Destroy(note.gameObject);
						break;
					}
				}
			}
		}

		if (jumpCount > 0.0f) { //jump
			transform.Translate(0.0f,  2.5f * Time.deltaTime, 0.0f);
			jumpCount -= 2.5f * Time.deltaTime;
		}

		cubeManager.FallAction();
		
	}

	public void Forward() {
		if (action == 0) {

			int tileState = ObjectCollisonForward();

			if (tileState < 2) {

				if (tileState == 1)
					jump();
				else if (isCrossJump()) {
					actionCount = 1.0f;
					action = 6;
				}
				else {
					actionCount = 1.0f;
					action = 1;
				}	
			}
				
		}
	}

	public void Backward() {
		if (action == 0 && !cubeManager.ObjectCollision(BackSide.position)) {
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

		if (!cubeManager.ObjectCollision(UpperFront.position) && !cubeManager.ObjectCollision(FarFront.position))
			foreach (var tile in LevelManager.Tiles)
				if (Vector3.Distance(FrontSide.position, tile.position) < 0.5f) {
					cubeManager.PushableObj = tile;
					actionCount = 1.0f;
					action = 7;
				}

	}

	public void Hit() {
		foreach (var tile in LevelManager.Tiles)
			if (Vector3.Distance(FrontSide.position, tile.position) < 0.5f && tile.tag == "Destroyable") {
				Transform [] children = tile.GetComponentsInChildren<Transform>();

				if (children.Length > 1) {
					cubeManager.invokeCubeFall(children[1]);
					children[1].SetParent(null);
				}

				Destroy(tile.gameObject);
				LevelManager.Tiles.Remove(tile);
				Spark.gameObject.SetActive(true);
				Spark.SetSparks(transform);
				break;
			}
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
		if (cubeManager.ObjectCollision(FrontSide.position)) {
			if (cubeManager.ObjectCollision(UpperFront.position))
				return 2;
			else
				return 1;
		}
				
		return 0;
	}


	bool isCrossJump() {

		return !cubeManager.ObjectCollision(LowerFront.position) && !cubeManager.ObjectCollision(FarFront.position) && cubeManager.ObjectCollision(FarLowerFront.position);
	}

	void jump() {
		this.action = 1;
		jumpCount = 1.0f;
		actionCount = 1.0f;
	}

	void fall() {
		action = 5;
		actionCount = 1.0f;
	}
}
