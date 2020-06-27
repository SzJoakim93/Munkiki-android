using UnityEngine;
using System.Collections;

public class ButtonsInGame : MonoBehaviour {

	public GameObject PausePanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InvokePause() {
		PausePanel.SetActive(true);
	}

	public void ResumeGame() {
		PausePanel.SetActive(false);
	}
}
