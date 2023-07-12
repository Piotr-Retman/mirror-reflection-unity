using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class SceneTextsLoader
{
    private static bool isSceneNameActual(string sceneName)
    {
        return SceneManager.GetActiveScene().name.Equals(sceneName);
    }

    public static void LoadWelcomeViewTranslations()
    {
        if (isSceneNameActual("AssetProject"))
        {
            if (CurrentLanguageData.LANGUAGE_DICTIONARY == null)
            {
                var filesJobs = new FilesJobs();
                filesJobs.LoadLanguage();
            }

            GameObject.Find("PlayButton").GetComponentInChildren<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["PLAY"];
            GameObject.Find("Options").GetComponentInChildren<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["OPTIONS"];
            GameObject.Find("GameTitle").GetComponent<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["MIRROR_REFLECTION"] + " v1.0";
            GameObject.Find("Stats").GetComponentInChildren<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["PLAYER_STATS"];
            GameObject.Find("GoToGoogleStats").GetComponentInChildren<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["GOOGLE_STATS"];
            GameObject.Find("ExitButton").GetComponentInChildren<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["EXIT"];
        }
    }

    public static void LoadPlaygroundTranslations()
    {
        if (isSceneNameActual("Playground"))
        {
            GameObject.Find("ChancesLabel").GetComponent<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["CHANCES"];
            GameObject.Find("PointsLabel").GetComponent<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["POINTS"];
            GameObject.Find("TimeLabel").GetComponent<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["TIME"];
        }
    }

    public static void LoadOptionsTranslations()
    {
        if (isSceneNameActual("Options"))
        {
            GameObject.Find("OptionsHeader").GetComponent<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["OPTIONS"];
            GameObject.Find("MusicOnToggle").GetComponentInChildren<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["MUSIC_ON"];
            GameObject.Find("TimingToggle").GetComponentInChildren<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["COUNT_TIME"];
            GameObject.Find("InfiniteChances").GetComponentInChildren<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["INFINITE_CHANCES"];
            GameObject.Find("SoundOn").GetComponentInChildren<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["SOUNDS_ON"];
            GameObject.Find("ResetGameStats").GetComponentInChildren<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["RESET_GAME_STATS"];
            GameObject.Find("GooglePlayGamesOn").GetComponentInChildren<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["GOOGLE_PLAY_GAMES_ON"];
        }
    }

    public static void LoadSumUpTranslations()
    {
        if (isSceneNameActual("LevelSumup"))
        {
            GameObject.Find("SumUpHeader").GetComponent<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["SUM_UP_HEADER"];
            GameObject.Find("PathLabelSumUp").GetComponent<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["PATH"];
            GameObject.Find("PointsLabelSumUp").GetComponent<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["POINTS"];
            GameObject.Find("SumLabelSumUp").GetComponent<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["SUM"];
            GameObject.Find("AllLabelSumUp").GetComponent<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["ALL"];
        }
    }

    public static void LoadChooseLevelTranslations()
    {
        if (isSceneNameActual("ChooseLvlScene"))
        {
            GameObject.Find("PathLabel").GetComponent<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["PATH"];
        }
    }

    public static void LoadNoInternetTranslations()
    {
        if (isSceneNameActual("NoInternetConnection"))
        {
            GameObject.Find("NoInternetLabel").GetComponent<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["NO_INTERNET_CONNECTION"];
        }
    }

    public static void LoadStatsTranslations()
    {
        if (isSceneNameActual("Stats"))
        {
            GameObject.Find("PathLabel").GetComponent<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["PATH"];
            GameObject.Find("PointsLabel").GetComponent<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["POINTS"];
            GameObject.Find("Header").GetComponent<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["PLAYER_STATS"];
        }
    }

    public static void LoadEndGameTranslations()
    {
        if (isSceneNameActual("EndGame"))
        {
            GameObject.Find("YouWonGame").GetComponent<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["CONGRATS"];
            GameObject.Find("NewGame").GetComponentInChildren<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["NEW_GAME"];
            GameObject.Find("WaitUntilNewLevels").GetComponentInChildren<Text>().text = CurrentLanguageData.LANGUAGE_DICTIONARY["WAIT_UNTIL_LEVELS"];
        }
    }
}
