using UnityEngine;

public class Settings
{
    public static bool MusicEnabled = true;
    public static float MusicVolume = 0.5f;
    public static bool SoundEnabled = true;
    public static float SoundVolume = 0.5f;
    public static string Language = "ENG";
    public static string [] languageIds = new string[]
    {
        "ENG", "HUN"
    };

    public static void Load()
    {
        SoundVolume = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        Language = PlayerPrefs.GetString("Language", Application.systemLanguage == SystemLanguage.Hungarian ? "HUN" : "ENG");
    }

    public static void Save()
    {
        PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.SetString("Language", Language);
    }

    public static int GetLanguageId()
    {
        for (int i = 0; i < languageIds.Length; i++)
        {
            if (languageIds[i] == Language)
            {
                return i;
            }
        }

        return -1;
    }

}