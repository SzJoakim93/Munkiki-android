using UnityEngine;
using System.Collections;
using System;

public class Munkiki : MonoBehaviour {
    [SerializeField] Transform FrontSide;
    [SerializeField] Transform BackSide;
    [SerializeField] Transform Bottom;
    [SerializeField] Transform UpperFront;
    [SerializeField] Transform UppestFront;
    [SerializeField] Transform LowerFront;
    [SerializeField] Transform LowestFront;
    [SerializeField] Transform FarFront;
    [SerializeField] Transform FarUpperFront;
    [SerializeField] Transform FarLowerFront;
    [SerializeField] Transform FarLowestFront;
    [SerializeField] Transform FarVeryLowestFront;
    [SerializeField] GameObject MusicalNote;
    [SerializeField] Transform Camera;
    [SerializeField] LevelManager LevelManager;
    [SerializeField] ParticleSystem Spark;
    [SerializeField] Animator animator;
    [SerializeField] ButtonsInGame ButtonsInGame;
    [SerializeField] PointAnimator PointAnimator;
    [SerializeField] AudioSource pickup;
    [SerializeField] AudioSource win;
    [SerializeField] AudioSource PushSnd;
    [SerializeField] AudioSource HitBox;
    [SerializeField] CubeManager CubeManager;
    Vector3 pushDir;

    float actionCount = 0.0f;
    float jumpCount = 0.0f;
    int action = 0;
    int steps = 0;

    // Use this for initialization
    void Start () {

    }
    
    // Update is called once per frame
    void Update () {
        if (actionCount > 0.0f) {

            switch (action) {
                case 1: //step forward
                    transform.Translate(0.0f, 0.0f, 1.5f * Global.SIZE_MULTIPLER * Time.deltaTime);
                    actionCount -= 1.5f * Global.SIZE_MULTIPLER * Time.deltaTime;
                    break;
                case 2: //step backward
                    transform.Translate(0.0f, 0.0f, -1.5f * Global.SIZE_MULTIPLER * Time.deltaTime);
                    actionCount -= 1.5f * Global.SIZE_MULTIPLER * Time.deltaTime;
                    break;
                case 3: //turn right
                    transform.Rotate(0.0f, 100.0f * Time.deltaTime, 0.0f);
                    actionCount -=  100.0f * Time.deltaTime;
                    break;
                case 4: //turn left
                    transform.Rotate(0.0f, -100.0f * Time.deltaTime, 0.0f);
                    actionCount -=  100.0f * Time.deltaTime;
                    break;
                case 5: //fall

                    if (transform.position.y < 2.0f)
                    {
                        if (CubeManager.ObjectCollision(Bottom.position))
                        {
                            transform.position = new Vector3(
                                CubeManager.CollidedObj.position.x,
                                CubeManager.CollidedObj.position.y + 4.875f,
                                CubeManager.CollidedObj.position.z);
                            animator.SetTrigger("Standing");
                        }
                        else
                        {
                            Camera.SetParent(null);
                            transform.Translate(0.0f,  -0.5f * Global.SIZE_MULTIPLER * Time.deltaTime, 0.0f);
                            animator.SetTrigger("WaterFalling");

                             if (transform.position.y < 6.0f)
                             {
                                ButtonsInGame.ShowGameOverMenu();
                             }
                        }
                    }
                    else
                    {
                        transform.Translate(0.0f,  -2.0f * Global.SIZE_MULTIPLER * Time.deltaTime, 0.0f);
                        actionCount -= 2.0f * Global.SIZE_MULTIPLER * Time.deltaTime;
                    }

                    break;
                case 6: //cross jump
                    transform.Translate(0.0f, 0.0f, 3.0f * Global.SIZE_MULTIPLER * Time.deltaTime);
                    actionCount -= 1.5f * Global.SIZE_MULTIPLER * Time.deltaTime;
                    break;
                case 7: //pushing
                    transform.Translate(0.0f, 0.0f, 1.0f * Global.SIZE_MULTIPLER * Time.deltaTime);

                    if (Mathf.RoundToInt(transform.eulerAngles.y) == 0)
                        pushDir = new Vector3(0.0f, 0.0f, 1.0f * Global.SIZE_MULTIPLER * Time.deltaTime);
                    else if (Mathf.RoundToInt(transform.eulerAngles.y) == 270)
                        pushDir = new Vector3(-1.0f * Global.SIZE_MULTIPLER * Time.deltaTime, 0.0f, 0.0f);
                    else if (Mathf.RoundToInt(transform.eulerAngles.y) == 180)
                        pushDir = new Vector3(0.0f, 0.0f, -1.0f * Global.SIZE_MULTIPLER * Time.deltaTime);
                    else if (Mathf.RoundToInt(transform.eulerAngles.y) == 90)
                        pushDir = new Vector3(1.0f * Global.SIZE_MULTIPLER * Time.deltaTime, 0.0f, 0.0f);

                    CubeManager.PushableObj.Translate(pushDir.x, pushDir.y, pushDir.z);

                    actionCount -= 1.0f * Global.SIZE_MULTIPLER * Time.deltaTime;
                    break;
                case 8: //jump
                    transform.Translate(0.0f, 0.0f, 1.5f * Global.SIZE_MULTIPLER * Time.deltaTime);
                    actionCount -= 1.5f * Global.SIZE_MULTIPLER * Time.deltaTime;

                    if (jumpCount > 0.0f) { //jump
                        transform.Translate(0.0f,  2.5f * Global.SIZE_MULTIPLER * Time.deltaTime, 0.0f);
                        jumpCount -= 2.5f * Global.SIZE_MULTIPLER * Time.deltaTime;
                    }

                    break;
                case 9: //spring jump near

                    transform.Translate(0.0f, 0.0f, 1.5f * Global.SIZE_MULTIPLER * Time.deltaTime);
                    actionCount -= 1.5f * Global.SIZE_MULTIPLER * Time.deltaTime;

                    if (jumpCount > 0.0f) { //jump
                        transform.Translate(0.0f,  5.0f * Global.SIZE_MULTIPLER * Time.deltaTime, 0.0f);
                        jumpCount -= 5.0f * Global.SIZE_MULTIPLER * Time.deltaTime;
                    }

                    break;
                case 10: //spring jump far
                    transform.Translate(0.0f, 0.0f, 3.0f * Global.SIZE_MULTIPLER * Time.deltaTime);
                    actionCount -= 1.5f * Global.SIZE_MULTIPLER * Time.deltaTime;

                    if (jumpCount > 0.0f) { //jump
                        transform.Translate(0.0f,  2.5f * Global.SIZE_MULTIPLER * Time.deltaTime, 0.0f);
                        jumpCount -= 2.5f * Global.SIZE_MULTIPLER * Time.deltaTime;
                    }
                    
                    break;
            }
 
            if (actionCount < 0.0f)
            {
                if (action == 3 || action == 4) //after rotate
                {
                    fixRotate();
                    animator.SetTrigger("Standing");
                }

                if (action == 7)
                {
                    CubeManager.PushableObj.position = new Vector3(
                        Mathf.Round(CubeManager.PushableObj.position.x), CubeManager.PushableObj.position.y, Mathf.Round(CubeManager.PushableObj.position.z));

                    if (!CubeManager.ObjectCollision(LowerFront.position))
                    {
                        CubeManager.invokeCubeFall(CubeManager.PushableObj);
                    }
                    else if (CubeManager.PushableObj.tag.ToString() == "Slippery")
                    {
                        CubeManager.PushableObj.GetComponent<Slippery>().SetDirection(pushDir.x, pushDir.y, pushDir.z);
                    }
                    else
                    {
                        CubeManager.PushableObj.SetParent(CubeManager.CollidedObj);
                    }
                }

                if (action == 1 || action == 2 || action == 5 || action == 6 || action == 7 || action == 8 || action == 9) //after moving
                {
                    if (!CubeManager.ObjectCollision(Bottom.position)) //nothing under munkiki
                    {
                        if (action != 5)
                        {
                            animator.SetTrigger("Falling");
                        }
                        fall();
                    }
                    else
                    {
                        fixPosition();
                        animator.SetTrigger("Standing");
                        actionCount = 0.0f;
                        action = 0;
                    }
                }
                else
                {
                    actionCount = 0.0f;
                    action = 0;
                    animator.SetTrigger("Standing");
                }
            }
        }

        if (actionCount == 0.0f || transform.parent != null)
        {
            foreach (var note in LevelManager.MusicalNotes)
            {
                if (Vector3.Distance(transform.position, note.position) < 1.0f)
                {
                    LevelManager.PickMusicalNote(note);
                    if (LevelManager.MusicalNotesCount <= 0 || note.tag.ToString() == "MagicKey")
                    {
                        if (note.tag.ToString() == "MagicKey")
                        {
                            PlayerPrefs.SetInt($"LevelMagicKey{Global.level}", 1);
                        }
                        if (Global.level == PlayerPrefs.GetInt("UnlockedLevels", 1) && Global.level < 25)
                        {
                            PlayerPrefs.SetInt("UnlockedLevels", Global.level+1);
                        }
                        if (Global.level == 25)
                        {
                            Global.startingMenu = 2;
                        }
                        animator.SetTrigger("AllMusicalNotesCollected");
                        StartCoroutine(ActivateOwnMusicalNote());
                        StartCoroutine(ShowSummaryMenu());
                        win.Play();
                    }
                    else
                    {
                        pickup.Play();
                    }
                    break;
                }
            }
        }

        if (isInCube())
        {
            transform.Translate(0.0f, Global.SIZE_MULTIPLER, 0.0f);
        }

        CubeManager.FallAction();
        
    }

    public void ForwardBtn()
    {
        StartCoroutine(Forward());
    }

    public void Backward() {
        if (transform.parent != null)
                transform.SetParent(null);
        if (action == 0 && !CubeManager.ObjectCollision(BackSide.position)) {
            actionCount = 1.0f * Global.SIZE_MULTIPLER;
            action = 2;
            animator.SetTrigger("RunningBack");
            steps++;
        }
    }

    public void TurnLeft() {
        if (action == 0) {
            actionCount = 90.0f;
            action = 4;
            animator.SetTrigger("TurnLeft");
        }
    }

    public void TurnRight() {
        if (action == 0) {
            actionCount = 90.0f;
            action = 3;
            animator.SetTrigger("TurnRight");
        }	
    }

    public void Push() {

        if (action == 0 && !CubeManager.ObjectCollision(UpperFront.position) && !CubeManager.ObjectCollision(FarFront.position))
            foreach (var tile in LevelManager.Tiles)
                if (Vector3.Distance(FrontSide.position, tile.position) < 1.0f) {
                    CubeManager.PushableObj = tile;
                    actionCount = 1.0f * Global.SIZE_MULTIPLER;
                    action = 7;
                    steps++;
                    animator.SetTrigger("Pushing");
                    PushSnd.Play();
                }
    }

    public void HitBtn()
    {
        StartCoroutine(Hit());
    }

    IEnumerator Hit()
    {
        animator.SetTrigger("Hitting");
        yield return new WaitForSeconds(0.8f);

        foreach (var tile in LevelManager.Tiles)
            if (Vector3.Distance(FrontSide.position, tile.position) < 0.5f && tile.tag == "Destroyable")
            {
                Transform [] children = tile.GetComponentsInChildren<Transform>();

                if (children.Length > 1)
                {
                    if (children[1].localPosition.y > 0.01f)
                    {
                        CubeManager.invokeCubeFall(children[1]);
                    }
                    children[1].SetParent(null);
                }

                LevelManager.Tiles.Remove(tile);
                Destroy(tile.gameObject);
                Spark.transform.position = transform.position;
                Spark.Play();
                HitBox.Play();
                break;
            }
    }

    IEnumerator Forward()
    {
        if (action == 0)
        {
            int tileState = ObjectCollisonForward();

            if (tileState < 2)
            { //no cube on the UpperFront

                if (isCrossJump()) //cross jumping
                {
                    animator.SetTrigger("Jumping");
                    yield return new WaitForSeconds(0.3f);
                    actionCount = 1.0f * Global.SIZE_MULTIPLER;
                    action = 6;
                } else if (isSpring() && isSpringFar())
                {
                    animator.SetTrigger("Jumping");
                    yield return new WaitForSeconds(0.3f);
                    jumpCount = 1.0f * Global.SIZE_MULTIPLER;
                    actionCount = 1.0f * Global.SIZE_MULTIPLER;
                    action = 10;
                    
                }
                else //jumping
                {
                    if (tileState == 1) //cube on the front
                    {
                        animator.SetTrigger("Jumping");
                        yield return new WaitForSeconds(0.3f);
                        jumpCount = 1.0f * Global.SIZE_MULTIPLER;
                    }
                    else
                    {
                        animator.SetTrigger("Running");
                    }
                        
                    actionCount = 1.0f * Global.SIZE_MULTIPLER;
                    action = 8;
                }
                steps++;
            }
            else if (isSpring() && isSpringNear())
            { //cube on the UpperFront
                animator.SetTrigger("Jumping");
                yield return new WaitForSeconds(0.3f);
                jumpCount = 2.0f * Global.SIZE_MULTIPLER;
                actionCount = 1.0f * Global.SIZE_MULTIPLER;
                action = 9;
                steps++;
            }

            if (transform.parent != null)
                    transform.SetParent(null);
        }
    }

    IEnumerator ActivateOwnMusicalNote()
    {
        yield return new WaitForSeconds(1.0f);
        MusicalNote.SetActive(true);
    }

    IEnumerator ShowSummaryMenu()
    {
        yield return new WaitForSeconds(2.5f);
        ButtonsInGame.ShowSummaryMenu();
        Debug.Log((int)(Math.Pow((float)LevelManager.optimalSteps[Global.level-1]/steps, 2)*100));
        PointAnimator.StartAnimation((int)(Math.Pow((float)LevelManager.optimalSteps[Global.level-1]/steps, 2)*100));
    }

    void fixRotate()
    {
        if (transform.eulerAngles.y > 80 && transform.eulerAngles.y < 120)
            transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
        else if (transform.eulerAngles.y > 160 && transform.eulerAngles.y < 200)
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        else if (transform.eulerAngles.y > 250 && transform.eulerAngles.y < 290)
            transform.eulerAngles = new Vector3(0.0f, 270.0f, 0.0f);
        else if (transform.eulerAngles.y > 340 || transform.eulerAngles.y < 20)
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void fixPosition()
    {
        transform.position = new Vector3(
            CubeManager.CollidedObj.position.x,
            CubeManager.CollidedObj.position.y + 4.875f,
            CubeManager.CollidedObj.position.z);

        if (CubeManager.CollidedObj.tag == "Moving" || CubeManager.CollidedObj.tag == "Spring")
            transform.SetParent(CubeManager.CollidedObj);
    }

    int ObjectCollisonForward()
    {
        if (CubeManager.ObjectCollision(FrontSide.position)) {
            if (CubeManager.ObjectCollision(UpperFront.position))
                return 2;
            else
                return 1;
        }
                
        return 0;
    }


    bool isCrossJump()
    {

        return !CubeManager.ObjectCollision(LowestFront.position) &&
            !CubeManager.ObjectCollision(LowerFront.position) &&
            !CubeManager.ObjectCollision(FarFront.position) &&
            (CubeManager.ObjectCollision(FarLowerFront.position) ||
                CubeManager.ObjectCollision(FarLowestFront.position) ||
                CubeManager.ObjectCollision(FarVeryLowestFront.position));
    }

    bool isSpring()
    {
        if (CubeManager.ObjectCollision(Bottom.position))
            return CubeManager.CollidedObj.tag == "Spring";
        return false;
    }

    bool isSpringNear()
    {
        return !CubeManager.ObjectCollision(UppestFront.position);
    }

    bool isSpringFar()
    {
        return CubeManager.ObjectCollision(FarFront.position) && !CubeManager.ObjectCollision(FarUpperFront.position);
    }

    bool isInCube()
    {
        return CubeManager.ObjectCollision(transform.position);
    }

    void fall()
    {
        action = 5;
        actionCount = 1.0f * Global.SIZE_MULTIPLER;
    }
}
