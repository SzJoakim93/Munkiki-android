using System;
using UnityEngine;

public class Slippery : MonoBehaviour
{
    [SerializeField] CubeManager CubeManager;
    Vector3 direction = new Vector3(0, 0, 0);
    Transform collidedObjUnder = null;
    
    void Update()
    {
        if (direction.x != 0 || direction.y != 0 || direction.z != 0)
        {
            transform.Translate(direction);

            if ((Mathf.RoundToInt(transform.position.x * 5) % (Global.SIZE_MULTIPLER * 5)) == 0 && (Mathf.RoundToInt(transform.position.z * 5) % (Global.SIZE_MULTIPLER * 5)) == 0)
            {
                Vector3 underSide = new Vector3(transform.position.x, transform.position.y - Global.SIZE_MULTIPLER, transform.position.z);
                if (!CubeManager.ObjectCollision(underSide))
                {
                    direction = new Vector3(0, 0, 0);
                    CubeManager.invokeCubeFall(transform);
                }
                else
                {
                    collidedObjUnder = CubeManager.CollidedObj;
                    transform.SetParent(collidedObjUnder);
                }

                if (CubeManager.IsCollidedByDirection(transform.position, direction.x, direction.y, direction.z))
                {
                    direction = new Vector3(0, 0, 0);
                    transform.position = new Vector3(collidedObjUnder.position.x, collidedObjUnder.position.y + Global.SIZE_MULTIPLER, collidedObjUnder.position.z);
                }
            }
            
        }
    }

    public void SetDirection(float x, float y, float z)
    {
        direction = new Vector3(x, y, z);
    }
}