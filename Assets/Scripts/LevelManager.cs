using UnityEngine;
using System.Collections.Generic;
using System.Globalization;

public class LevelManager : MonoBehaviour {

	[SerializeField] Transform [] cubes;
	[SerializeField] Transform [] pickups;
	public List<Transform> Tiles;
	public List<Transform> MusicalNotes;

	// Use this for initialization
	void Start () {
		Load();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (var note in MusicalNotes)
			note.Rotate(0.0f, 30.0f * Time.deltaTime, 0.0f);
	
	}

	void Load()
	{
		TextAsset level_path = Resources.Load<TextAsset>("Levels/L" + Global.level.ToString());
        string[] lines = level_path.text.Replace("\r\n", "\n") .Split('\n');
		Transform newObject = null;
		Transform prevObject = null;

		foreach(var line in lines)
        {
			if (line == "")
			{
				continue;
			}

			string [] obj_params = line.Split(' ');

			if (obj_params[0] == "+")
			{
				addChild(ref prevObject, obj_params[1], obj_params.Length > 2 ? obj_params[2] : null );
			}
			else
			{
				newObject =
						Instantiate<Transform>(cubes[int.Parse(obj_params[3])], new Vector3(float.Parse(obj_params[0], CultureInfo.InvariantCulture)*Global.SIZE_MULTIPLER,
							float.Parse(obj_params[1], CultureInfo.InvariantCulture)*Global.SIZE_MULTIPLER,
							float.Parse(obj_params[2], CultureInfo.InvariantCulture)*Global.SIZE_MULTIPLER), Quaternion.identity);
				newObject.gameObject.SetActive(true);
				Tiles.Add(newObject);

				if (obj_params.Length > 4)
				{
					addInObject(prevObject, obj_params[4]);
				}
				
				prevObject = newObject;
			}

			
		}
	}

	void addChild(ref Transform parentObject, string item, string inItem)
	{
		Transform newObject = null;
		if (item == "*" || item == "%")
		{
			newObject = pickups[getIndexFromPickupMarkup(item)];
		}
		else
		{
			newObject = cubes[int.Parse(item)];
		}

		Transform instantiatedObj = Instantiate<Transform>(newObject, parentObject.position, Quaternion.identity);
		instantiatedObj.gameObject.SetActive(true);
		instantiatedObj.SetParent(parentObject);
		instantiatedObj.Translate(0.0f, Global.SIZE_MULTIPLER, 0.0f);

		if (item == "*" || item == "%")
		{
			MusicalNotes.Add(instantiatedObj);
		}
		else
		{
			Tiles.Add(instantiatedObj);
		}

		if (inItem != null)
		{
			addInObject(instantiatedObj, inItem);
		}

		parentObject = instantiatedObj;
	}

	void addInObject(Transform parentObject, string itemType)
	{
		Transform newMusicalNote = Instantiate<Transform>(pickups[getIndexFromPickupMarkup(itemType)], parentObject.position, Quaternion.identity);
		newMusicalNote.gameObject.SetActive(true);
		newMusicalNote.SetParent(parentObject);
		MusicalNotes.Add(newMusicalNote);
	}

	int getIndexFromPickupMarkup(string pickupMarkup)
	{
		switch (pickupMarkup)
		{
			case "*":
				return 0;
			case "%":
				return 1;
			default:
				return -1;
		}
	}
}
