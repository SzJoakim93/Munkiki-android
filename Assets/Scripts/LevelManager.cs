using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	public List<Transform> Tiles;
	public List<Transform> MusicalNotes;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach (var note in MusicalNotes)
			note.Rotate(0.0f, 1.0f, 0.0f);
	
	}
}
