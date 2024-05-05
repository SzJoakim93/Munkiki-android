using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] CameraStates cameraStates;

    [SerializeField] GameObject Intro;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject Win;
    [SerializeField] GameObject LevelButtonPack;
    [SerializeField] Sprite ActiveStar;
    [SerializeField] Sprite ActiveKey;
    [SerializeField] Text WinText;
    [SerializeField] LanguageManager LanguageManager;
    GameObject actualMenu;
    Button[] levelButtons;
    Image[][] starsAndKey;
    const int BASIC_LEVEL_COUNT = 25;
    const int BONUS_LEVEL_COUNT = 5;

    // Start is called before the first frame update
    void Start()
    {
        levelButtons = LevelButtonPack.GetComponentsInChildren<Button>(true);
        for (int i = 1; i < /*PlayerPrefs.GetInt("UnlockedLevels", 1)*/30; i++)
        {
            levelButtons[i].interactable = true;
        }

        starsAndKey = new Image[levelButtons.Length][];
        Global.magicKeys = 0;
        for (int i = 0; i < levelButtons.Length; i++)
        {
            starsAndKey[i] = levelButtons[i].GetComponentsInChildren<Image>(true);
            for (int j = 1; j < PlayerPrefs.GetInt($"LevelStars{i+1}", 0)+1; j++)
            {
                starsAndKey[i][j].sprite = ActiveStar;
            }

            if (PlayerPrefs.GetInt($"LevelMagicKey{i+1}", 0) == 1)
            {
                if (starsAndKey[i].Length > 4)
                {
                    starsAndKey[i][4].sprite = ActiveKey;
                }

                levelButtons[BASIC_LEVEL_COUNT + Global.magicKeys - 1].interactable = true;
                Global.magicKeys++;
            }
        }

        switch (Global.startingMenu)
        {
            case 0:
                actualMenu = Intro;
                break;
            case 1:
                Intro.SetActive(false);
                actualMenu = MainMenu;
                //cameraStates.SetPosition(1);
                break;
            case 2:
                Intro.SetActive(false);
                actualMenu = Win;
                //cameraStates.SetPosition(1);
                break;
        }

        actualMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.startingMenu == 2 && WinText.text == "")
        {
            if (Global.level >= BASIC_LEVEL_COUNT + BONUS_LEVEL_COUNT)
            {
                WinText.text = LanguageManager.GetTextByValue("WinBonus");
            }
            else
            {
                WinText.text = LanguageManager.GetTextByValue("Win1");
                if(Global.magicKeys >= BONUS_LEVEL_COUNT)
                {
                    WinText.text += "\n" + LanguageManager.GetTextByValue("Win3");
                }
                else
                {
                    WinText.text += "\n" + LanguageManager.GetTextByValue("Win2");
                }
            }
        }

        if (Global.startingMenu != 0)
        {
            Global.startingMenu = 0;
        }
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
