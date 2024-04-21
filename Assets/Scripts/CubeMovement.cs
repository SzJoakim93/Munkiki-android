using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CubeMovement : MonoBehaviour {
    [SerializeField] CubeManager CubeManager;
    public List<Vector3> Nodes;
    int x = 1;
    bool forwardDirection = true;

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
        float dirX = transform.position.x > Nodes[x].x + 0.1f ? -0.5f * Global.SIZE_MULTIPLER * Time.deltaTime : 
                (transform.position.x < Nodes[x].x - 0.1f ? 0.5f * Global.SIZE_MULTIPLER * Time.deltaTime : 0.0f);
        float dirY = transform.position.y > Nodes[x].y + 0.1f ? -0.5f * Global.SIZE_MULTIPLER * Time.deltaTime : 
                (transform.position.y < Nodes[x].y - 0.1f ? 0.5f * Global.SIZE_MULTIPLER * Time.deltaTime : 0.0f);
        float dirZ = transform.position.z > Nodes[x].z + 0.1f ? -0.5f * Global.SIZE_MULTIPLER * Time.deltaTime : 
                (transform.position.z < Nodes[x].z - 0.1f ? 0.5f * Global.SIZE_MULTIPLER * Time.deltaTime : 0.0f);
        
        if (CubeManager.IsCollidedByDirection(transform.position, dirX, dirY, dirZ))
        {
            forwardDirection = !forwardDirection;
            x += forwardDirection ? 1 : -1;
        }
        
        transform.Translate(dirX, dirY, dirZ);

        if (Vector3.Distance(transform.position, Nodes[x]) < 0.1f)
        {
            transform.position = new Vector3(Nodes[x].x, Nodes[x].y, Nodes[x].z);

            if (x == Nodes.Count()-1)
            {
                forwardDirection = false;
            }
            else if (x == 0)
            {
                forwardDirection = true;
            }

            x += forwardDirection ? 1 : -1;
        }
    }
}
