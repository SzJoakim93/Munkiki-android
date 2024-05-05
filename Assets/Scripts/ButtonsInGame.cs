using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsInGame : MonoBehaviour {

	[SerializeField] GameObject PausePanel;
    [SerializeField] GameObject SummaryPanel;
	[SerializeField] GameObject GameOverPanel;
    [SerializeField] AdManagerInterstitial AdManager;
    [SerializeField] MessageDialog MessageDialog;
    [SerializeField] LanguageManager LanguageManager;
    [SerializeField] Button NextLevelBtn;
    
    enum SelectedButton
    {
        QuitToMenu,
        NextLevel,
        RestartLevel
    }
    SelectedButton selectedButton;

    int tutorialIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        tutorialIndex = PlayerPrefs.GetInt("TutorialIndex", 1);
    }

    // Update is called once per frame
    void Update()
    {
        //Tutorials
        ShowTutorial(1, 1);
        ShowTutorial(1, 2);
        ShowTutorial(2, 3);
        ShowTutorial(3, 4);
        ShowTutorial(6, 5);
        ShowTutorial(10, 6);
        ShowTutorial(14, 7);
        ShowTutorial(15, 8);
    }

    public void ShowPauseMenu()
    {
        PausePanel.SetActive(true);
    }

    public void ShowSummaryMenu()
    {
        if ((int)Global.level == 25+Global.magicKeys)
        {
            NextLevelBtn.interactable = false;
        }
        SummaryPanel.SetActive(true);
    }

    public void ShowGameOverMenu()
    {
        GameOverPanel.SetActive(true);
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
    }

    public void Restart()
    {
        selectedButton = SelectedButton.RestartLevel;
        if (AdManager.ShowAd())
        {
            return;
        }
        
        SceneManager.LoadScene("InGame");
    }

    public void NextLevel()
    {
        selectedButton = SelectedButton.NextLevel;

        if ((int)Global.level < 25+Global.magicKeys)
        {
            Global.level++;
        }
        
        if (AdManager.ShowAd())
        {
            return;
        }
        
        SceneManager.LoadScene("InGame");
    }

    public void Quit()
    {
        selectedButton = SelectedButton.QuitToMenu;
        if (AdManager.ShowAd())
        {
            return;
        }

        SceneManager.LoadScene("Menu");
    }

    public void OnAdClosedEvent() {
        switch (selectedButton)
        {
            case SelectedButton.RestartLevel:
                SceneManager.LoadScene("InGame");
                break;
            case SelectedButton.NextLevel:
                SceneManager.LoadScene("InGame");
                break;
            case SelectedButton.QuitToMenu:
                SceneManager.LoadScene("Menu");
                break;
        }
    }

    void ShowTutorial(int level, int tutorialIndex)
    {
        if (Global.level == level && this.tutorialIndex == tutorialIndex && !MessageDialog.gameObject.activeInHierarchy && !SummaryPanel.activeInHierarchy)
        {
            MessageDialog.Popup(LanguageManager.GetTextByValue($"Tutorial{tutorialIndex}"));
            this.tutorialIndex++;
            PlayerPrefs.SetInt("TutorialIndex", this.tutorialIndex);
        }
    }
}
