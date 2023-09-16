using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] CameraStates cameraStates;

    [SerializeField] GameObject Intro;
    [SerializeField] GameObject MainMenu;
    GameObject actualMenu;

    // Start is called before the first frame update
    void Start()
    {
        actualMenu = Intro;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartClick()
    {
        cameraStates.SetPosition(1);
        ShowMenu(MainMenu);
    }

    public void BackToIntro()
    {
        cameraStates.SetPosition(0);
        ShowMenu(Intro);
    }

    public void StartLevelClick(int x)
    {
        Global.level = x;
        SceneManager.LoadScene("ingame");
    }

    public void ShowMenu(GameObject menu)
    {
        actualMenu.SetActive(false);
        menu.SetActive(true);
        actualMenu = menu;
    }
}
