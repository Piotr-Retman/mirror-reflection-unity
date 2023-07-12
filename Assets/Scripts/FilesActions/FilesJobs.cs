using System;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

class FilesJobs
{
    public static void SaveGameState()
    {
        SavedGame sg = CreateSaveGameObject();
        SaveGameFlow(sg);
    }

    public static long SaveGameStateExtraReward()
    {
        long points = 0;
        if (CurrentLevelData.isSpecialRewardEnable)
        {
            points = 1000 * CurrentLevelData.path;
            SavedGame sg = CreateSaveGameObject(points);
            SaveGameFlow(sg);
        }
        CurrentLevelData.isSpecialRewardEnable = false;
        return points;
    }

    internal static void SaveGameStateUntilNewLevels()
    {
        
        SavedGame sg = CreateSaveGameObject(0,true);
        sg.actualPath = CurrentLevelData.maximumPath; //TODO: TO CHANGE WHEN NEW LEVELS ARRIVE!
        sg.actualLevel = 15;
        sg.nextLevel = 16;
        SaveGameFlow(sg);
    }

    private static void SaveGameFlow(SavedGame sg)
    {
        var jsonToSave = JsonUtility.ToJson(sg);
        var jsonAsString = jsonToSave.ToString();
        var dataPath = Path.Combine(Application.persistentDataPath, "save-file.json");

        FileStream streamW = new FileStream(dataPath, FileMode.Truncate);
        var writer = new StreamWriter(streamW);
        writer.Write(jsonAsString);
        writer.Close();
        streamW.Close();

        GooglePlayGamesManager.SaveGeneralScore(sg.actualPoints);

        Debug.Log("Game Saved!");
    }

    private static SavedGame CreateSaveGameObject(long optionalReward = 0, bool optionalNotCountingPonts = false)
    {
        long pointsToSave = 0;

        if (optionalNotCountingPonts)
        {
            pointsToSave = CurrentLevelData.loadedPoints;
        }
        else
        {
            pointsToSave = CurrentLevelData.currentPoints + CurrentLevelData.loadedPoints + optionalReward;
        }

        Debug.LogWarning(
            "******** \n Data: " +
            "Current Level:" + CurrentLevelData.actualLevel +
            "Current Points:" + CurrentLevelData.currentPoints +
            "Loaded Points:" + CurrentLevelData.loadedPoints +
            "Optional Reward:" + optionalReward +
            "Points to save:"+ pointsToSave + 
            "\n ******");

        SavedGame sg;
        if (CurrentLevelData.nextLevel + 1 >= 16)
        {
            sg = new SavedGame
            {
                actualPath = CurrentLevelData.path + 1,
                actualPoints = pointsToSave,
                actualLevel = 1,
                nextLevel = 1
            };
        }
        else
        {
            sg = new SavedGame
            {
                actualPath = CurrentLevelData.path,
                actualPoints = pointsToSave,
                actualLevel = CurrentLevelData.levelWon ? CurrentLevelData.nextLevel : CurrentLevelData.maximumLvlReachedByPlayer,
                nextLevel = CurrentLevelData.levelWon && CurrentLevelData.maximumLvlReachedByPlayer == CurrentLevelData.actualLevel ? CurrentLevelData.nextLevel + 1 : CurrentLevelData.nextLevel
            };
        }

        CurrentLevelData.loadedPoints = pointsToSave;
        return sg;
    }

    public void LoadGame()
    {
        var dataPath = Path.Combine(Application.persistentDataPath, "save-file.json");

        StreamReader reader = new StreamReader(dataPath);
        string saveGameString = reader.ReadToEnd();

        var savedGame = JsonUtility.FromJson<SavedGame>(saveGameString);
        CurrentLevelData.currentPoints = savedGame.actualPoints;
        CurrentLevelData.loadedPoints = savedGame.actualPoints;
        CurrentLevelData.nextLevel = savedGame.nextLevel;
        CurrentLevelData.actualLevel = savedGame.actualLevel;
        CurrentLevelData.maximumLvlReachedByPlayer = savedGame.nextLevel;
        CurrentLevelData.path = savedGame.actualPath;
    }

    public void LoadLanguage()
    {
        if (CurrentLanguageData.LANGUAGE_DICTIONARY == null)
        {
            CurrentLanguageData.LANGUAGE_DICTIONARY = new System.Collections.Generic.Dictionary<string, string>();
        }

        //This outputs what language your system is in

        string locale = GetLocaleCode();
        TextAsset textFile = Resources.Load<TextAsset>("Translations/" + locale);
        string textAsString = textFile.text;
        char[] separatorSemicolon = ";".ToCharArray();
        char[] separator = ":".ToCharArray();
        string[] splittedDictionary = textAsString.Split(separatorSemicolon);

        for (int i = 0; i < splittedDictionary.Length; i++)
        {
            if (splittedDictionary[i].Length > 0)
            {
                string[] keyAndValue = splittedDictionary[i].Split(separator);
                CurrentLanguageData.LANGUAGE_DICTIONARY.Add(keyAndValue[0], keyAndValue[1]);
            }
        };

        Debug.Log("Lang done");
    }

    private static string GetLocaleCode()
    {
        // Default locale
        string localeVal = "en_US";

#if UNITY_ANDROID
        using (AndroidJavaClass cls = new AndroidJavaClass("java.util.Locale"))
        {
            if (cls != null)
            {
                using (AndroidJavaObject locale = cls.CallStatic<AndroidJavaObject>("getDefault"))
                {
                    if (locale != null)
                    {
                        localeVal = locale.Call<string>("getLanguage") + "_" + locale.Call<string>("getCountry");
                        Debug.Log("Android lang: " + localeVal);
                    }
                    else
                    {
                        Debug.Log("locale null");
                    }
                }
            }
            else
            {
                Debug.Log("cls null");
            }
        }
#endif
        Debug.Log("Locale: " + localeVal);
        return (localeVal);
    }


    public void LoadOptions()
    {
        var path = Path.Combine(Application.persistentDataPath, "options.json");

        StreamReader reader = new StreamReader(path);
        string optionsString = reader.ReadToEnd();

        //TextAsset text = Resources.Load<TextAsset>("options");
        //Debug.Log("Options json:" + text);
        var options = JsonUtility.FromJson<UserOptions>(optionsString);
        CurrentUserOptions.countTime = options.countTime;
        CurrentUserOptions.infiniteChances = options.infiniteChances;
        CurrentUserOptions.musicVolume = options.musicVolume;
        CurrentUserOptions.playMusic = options.playMusic;
        CurrentUserOptions.soundsOn = options.soundsOn;
        CurrentUserOptions.googlePlayGames = options.googlePlayGames;

        PointsCoefficient.UpdateCoefficient();

    }

    public void SaveOptions()
    {
        UserOptions uo = CreateOptionsGameObject();
        var jsonToSave = JsonUtility.ToJson(uo);
        var jsonAsString = jsonToSave.ToString();
        Debug.Log("JSON to save" + jsonAsString);
        var dataPath = Path.Combine(Application.persistentDataPath, "options.json");
        FileStream streamW = new FileStream(dataPath, FileMode.Truncate);
        var writer = new StreamWriter(streamW);
        writer.Write(jsonAsString);
        writer.Close();
        streamW.Close();
        Debug.Log("Options Saved!");
    }

    public UserOptions CreateOptionsGameObject()
    {
        UserOptions options = new UserOptions
        {
            countTime = CurrentUserOptions.countTime,
            infiniteChances = CurrentUserOptions.infiniteChances,
            musicVolume = CurrentUserOptions.musicVolume,
            playMusic = CurrentUserOptions.playMusic,
            soundsOn = CurrentUserOptions.soundsOn,
            googlePlayGames = CurrentUserOptions.googlePlayGames
        };
        return options;
    }

    public static void ResetGameState()
    {
        SavedGame sg = new SavedGame
        {
            actualPath = 1,
            actualPoints = 0,
            actualLevel = 1,
            nextLevel = 1
        };

        SaveGameFlow(sg);
    }
}

