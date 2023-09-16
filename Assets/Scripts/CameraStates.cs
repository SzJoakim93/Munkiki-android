using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStates : MonoBehaviour
{
    [SerializeField] Transform Camera;
    Tuple<Vector3, Vector3>[] positions;
    Tuple<Vector3, Vector3> dif;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        positions = new Tuple<Vector3, Vector3>[]
        {
            new Tuple<Vector3, Vector3>
            (
                Camera.position,
                Camera.eulerAngles
            ),
            new Tuple<Vector3, Vector3>
            (
                new Vector3(-2.69f, 4.92f, -12.25f),
                new Vector3(24.0f, 12.3f, 5.07f)
            )
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(positions[index].Item1, Camera.position) > 0.1f)
        {
            //Camera.Translate(dif.Item1);
            Camera.position = new Vector3(Camera.position.x + dif.Item1.x, 
                Camera.position.y + dif.Item1.y,
                Camera.position.z + dif.Item1.z);
            Camera.Rotate(dif.Item2);
        }
    }

    public void SetPosition(int i)
    {
        dif = new Tuple<Vector3, Vector3>
        (
            (positions[i].Item1 - positions[index].Item1) / 200.0f,
            (positions[i].Item2 - positions[index].Item2) / 200.0f
        );

        index = i;
    }
}
