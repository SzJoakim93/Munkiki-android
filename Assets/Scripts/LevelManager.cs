using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class LevelManager : MonoBehaviour {

    [SerializeField] Transform [] cubes;
    [SerializeField] Transform [] pickups;
    [SerializeField] Text musicalNoteTxt;
    public List<Transform> Tiles;
    public List<Transform> MusicalNotes;
    public int MusicalNotesCount
    {
        get { return MusicalNotes.Count(x => x.tag == "MusicalNote"); }
    }

    public readonly int[] optimalSteps = new int[] {
        3, 4, 8, 10, 18,
        16, 9, 17, 13, 18,
        8, 10, 18, 18, 25,
        30, 35, 18, 30, 38,
        24, 26, 50, 27, 21,
        41, 21, 17, 43, 22
    };
    // Use this for initialization
    void Start () {
        Load();
    }
    
    // Update is called once per frame
    void Update () {
        foreach (var note in MusicalNotes)
            note.Rotate(0.0f, 0.0f, 30.0f * Time.deltaTime);
    }

    public void PickMusicalNote(Transform note)
    {
        MusicalNotes.Remove(note);
        Destroy(note.gameObject);
        musicalNoteTxt.text = MusicalNotes.Count(x => x.tag == "MusicalNote").ToString();
    }

    void Load()
    {
        TextAsset level_path = Resources.Load<TextAsset>("Levels/L" + Global.level.ToString());
        string[] lines = level_path.text.Replace("\r\n", "\n").Split('\n');
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
                Transform objectToInstantiate;
                if (obj_params[3] == "*" || obj_params[3] == "%")
                {
                    objectToInstantiate = pickups[getIndexFromPickupMarkup(obj_params[3])];
                    newObject =
                        Instantiate<Transform>(objectToInstantiate, new Vector3(float.Parse(obj_params[0], CultureInfo.InvariantCulture)*Global.SIZE_MULTIPLER,
                            float.Parse(obj_params[1], CultureInfo.InvariantCulture)*Global.SIZE_MULTIPLER,
                            float.Parse(obj_params[2], CultureInfo.InvariantCulture)*Global.SIZE_MULTIPLER), Quaternion.identity);
                    newObject.eulerAngles = new Vector3(270.0f, 0.0f, 0.0f);
                    MusicalNotes.Add(newObject);
                }
                else
                {
                    objectToInstantiate = cubes[int.Parse(obj_params[3])];
                    newObject =
                        Instantiate<Transform>(objectToInstantiate, new Vector3(float.Parse(obj_params[0], CultureInfo.InvariantCulture)*Global.SIZE_MULTIPLER,
                            float.Parse(obj_params[1], CultureInfo.InvariantCulture)*Global.SIZE_MULTIPLER,
                            float.Parse(obj_params[2], CultureInfo.InvariantCulture)*Global.SIZE_MULTIPLER), Quaternion.identity);
                    Tiles.Add(newObject);
                }

                newObject.gameObject.SetActive(true);

                if (obj_params.Length > 4)
                {
                    CubeMovement newObjectMoving = newObject.GetComponent<CubeMovement>();

                    newObjectMoving.Nodes.Add(new Vector3(float.Parse(obj_params[0], CultureInfo.InvariantCulture)*Global.SIZE_MULTIPLER,
                            float.Parse(obj_params[1], CultureInfo.InvariantCulture)*Global.SIZE_MULTIPLER,
                            float.Parse(obj_params[2], CultureInfo.InvariantCulture)*Global.SIZE_MULTIPLER));
                    for (int i = 4; i < obj_params.Length; i += 3)
                    {
                        newObjectMoving.Nodes.Add(new Vector3(float.Parse(obj_params[i], CultureInfo.InvariantCulture)*Global.SIZE_MULTIPLER,
                            float.Parse(obj_params[i+1], CultureInfo.InvariantCulture)*Global.SIZE_MULTIPLER,
                            float.Parse(obj_params[i+2], CultureInfo.InvariantCulture)*Global.SIZE_MULTIPLER));
                    }
                }
                
                prevObject = newObject;
            }

            musicalNoteTxt.text = MusicalNotes.Count(x => x.tag == "MusicalNote").ToString();
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
            instantiatedObj.eulerAngles = new Vector3(270.0f, 0.0f, 0.0f);
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
        newMusicalNote.eulerAngles = new Vector3(270.0f, 0.0f, 0.0f);
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
