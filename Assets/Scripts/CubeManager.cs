using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] LevelManager LevelManager;
    [SerializeField] AudioSource FallSnd;
    public Transform PushableObj {
        set {
            this.pushableObj = value;
        }
        
        get {
            return this.pushableObj;
        }
    }

    public Transform CollidedObj {
        get {
            return this.collidedObj;
        }
    }

    Transform pushableObj = null;
    Transform collidedObj = null;
    Transform fallableObj = null;
    float cubeFall = 0.0f;

    public bool IsCollidedByDirection(Vector3 obj, float dirX, float dirY, float dirZ)
    {
        if (dirX > 0.0f)
        {
            Vector3 rightSide = new Vector3(obj.x + Global.SIZE_MULTIPLER, obj.y, obj.z);
            if (ObjectCollision(rightSide))
            {
                return true;
            }
        }
        else if (dirX < 0.0f)
        {
            Vector3 lefttSide = new Vector3(obj.x - Global.SIZE_MULTIPLER, obj.y, obj.z);
            if (ObjectCollision(lefttSide))
            {
                return true;
            }
        }

        if (dirY > 0.0f)
        {
            Vector3 upSide = new Vector3(obj.x, obj.y + Global.SIZE_MULTIPLER, obj.z);
            if (ObjectCollision(upSide))
            {
                return true;
            }
        }
        else if (dirY < 0.0f)
        {
            Vector3 downSide = new Vector3(obj.x, obj.y - Global.SIZE_MULTIPLER, obj.z);
            if (ObjectCollision(downSide))
            {
                return true;
            }
        }

        if (dirZ > 0.0f)
        {
            Vector3 forwardSide = new Vector3(obj.x, obj.y, obj.z + Global.SIZE_MULTIPLER);
            if (ObjectCollision(forwardSide))
            {
                return true;
            }
        }
        else if (dirZ < 0.0f)
        {
            Vector3 backwardSide = new Vector3(obj.x, obj.y, obj.z - Global.SIZE_MULTIPLER);
            if (ObjectCollision(backwardSide))
            {
                return true;
            }
        }

        return false;
    }

    public void FallAction() {
        if (cubeFall > 0.0f) { //cube falling
            fallableObj.Translate(0.0f, -1.0f * Global.SIZE_MULTIPLER * Time.deltaTime, 0.0f);
            cubeFall -= 1.0f * Global.SIZE_MULTIPLER * Time.deltaTime;

            if (cubeFall <= 0.0f)
            {
                Vector3 cubeBottomSide = new Vector3(fallableObj.position.x, fallableObj.position.y - (1.0f * Global.SIZE_MULTIPLER), fallableObj.position.z);

                if (fallableObj.position.y > 0.45f && !ObjectCollision(cubeBottomSide))
                    cubeFall = Global.SIZE_MULTIPLER;
                else
                {
                    FallSnd.Play();
                    fallableObj.SetParent(collidedObj);
                }
                    
            }
        }
    }

    public bool ObjectCollision(Vector3 obj) {
        foreach (var tile in LevelManager.Tiles)
        {
            if (Vector3.Distance(obj, tile.position) < 2.5)
            {
                collidedObj = tile;
                return true;
            }
        }
    
        collidedObj = null;
        return false;
    }

    public void invokeCubeFall(Transform obj) {
        cubeFall = Global.SIZE_MULTIPLER;
        fallableObj = obj;
    }
    
}