using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointAnimator : MonoBehaviour {

    [SerializeField] Text GainPoints;
    //[SerializeField] Text globalPoints;
    //[SerializeField] PopupText NoDeadText;
    [SerializeField] AudioSource pointCountSound;
    [SerializeField] Image [] Stars;
    [SerializeField] GameObject [] Sparks;
    [SerializeField] Sprite ActiveStar;
    //[SerializeField] LanguageManager LanguageManager;
    int maxPoints;
    int currentPoints;
    int currentRate;
    int [] trashold = new int[] {300, 600, 900};

    // Use this for initialization
    void Start () {
        currentPoints = 0;
        currentRate = 0;
        //pointCountSound.volume = Settings.SoundVolume;
    }
    
    // Update is called once per frame
    void Update () {
        if (currentPoints < maxPoints && currentPoints < 1000)
        {
            currentPoints += (int)(Time.deltaTime*750);
            if (currentPoints >= maxPoints)
            {
                currentPoints -= maxPoints - currentPoints;
                pointCountSound.Stop();
            }

            GainPoints.text = (currentPoints/10).ToString();

            for (int i = 0; i < trashold.Length; i++)
            {
                if (currentPoints > trashold[i] && i == currentRate)
                {
                    activateStar(i);
                    currentRate++;
                    if (currentRate > PlayerPrefs.GetInt($"LevelStars{Global.level}", 0))
                    {
                        PlayerPrefs.SetInt($"LevelStars{Global.level}", currentRate);
                    }
                }
            }   
        }
    }

    public void StartAnimation(int _maxPoints) {
        /*gainTitle = language_Manager.GetTextByValue("GainTitle");
        allPointsTitle = language_Manager.GetTextByValue("AllPointsTitle");
        global_points.text = allPointsTitle + "\n" + Global.global_points.ToString();*/
        maxPoints = _maxPoints*10;
        pointCountSound.Play();
    }

    void activateStar(int i) {
        Stars[i].sprite = ActiveStar;
        Sparks[i].SetActive(true);
    }
}
