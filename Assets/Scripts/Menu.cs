using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] CameraStates cameraStates;

    [SerializeField] GameObject Intro;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject LevelButtonPack;
    [SerializeField] Sprite ActiveStar;
    [SerializeField] Sprite ActiveKey;
    GameObject actualMenu;
    Button[] levelButtons;
    Image[][] starsAndKey;
    const int BASIC_LEVEL_COUNT = 25;
    const int BONUS_LEVEL_COUNT = 5;

    // Start is called before the first frame update
    void Start()
    {
        actualMenu = Intro;

        levelButtons = LevelButtonPack.GetComponentsInChildren<Button>(true);
        for (int i = 1; i < PlayerPrefs.GetInt("UnlockedLevels", 1); i++)
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
                starsAndKey[i][4].sprite = ActiveKey;
                levelButtons[BASIC_LEVEL_COUNT + Global.magicKeys].interactable = true;
                Global.magicKeys++;
            }
        }

        /*Global.LoadState();
        StarTxt.text = Global.TotalStars.ToString();
        TotalScores.text = Global.TotalScores.ToString();*/
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
