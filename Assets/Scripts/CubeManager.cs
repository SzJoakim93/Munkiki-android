using UnityEngine;

public class CubeManager
{ 
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
    LevelManager levelManager;

    public CubeManager(LevelManager _levelManager) {
        this.levelManager = _levelManager;
    }

    public void FallAction() {
        if (cubeFall > 0.0f) { //cube falling
			fallableObj.Translate(0.0f, -1.0f * Global.SIZE_MULTIPLER * Time.deltaTime, 0.0f);
			cubeFall -= 1.0f * Global.SIZE_MULTIPLER * Time.deltaTime;

			if (cubeFall <= 0.0f) {
				Vector3 cubeBottomSide = new Vector3(fallableObj.position.x, fallableObj.position.y - (1.0f * Global.SIZE_MULTIPLER), fallableObj.position.z);

				if (fallableObj.position.y > 0.45f && !ObjectCollision(cubeBottomSide))
					cubeFall = Global.SIZE_MULTIPLER;
				else
					fallableObj.SetParent(collidedObj);
					
			}
		}
    }

    public bool ObjectCollision(Vector3 obj) {
		foreach (var tile in levelManager.Tiles)
			if (Vector3.Distance(obj, tile.position) < 1.0f) {
				collidedObj = tile;
				return true;
			}
				
		collidedObj = null;
		return false;
	}

    public void invokeCubeFall(Transform obj) {
		cubeFall = Global.SIZE_MULTIPLER;
		fallableObj = obj;
	}
    
}