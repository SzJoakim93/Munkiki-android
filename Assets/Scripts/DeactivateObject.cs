using UnityEngine;
using System.Collections;

public class DeactivateObject : MonoBehaviour {

    // Use this for initialization
    void Start ()
    {

    }
    
    // Update is called once per frame
    void Update ()
    {
    
    }

    public void Deactivate()
    {
        gameObject.SetActive (false);
    }
}
