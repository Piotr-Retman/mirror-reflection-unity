using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RunLevel : MonoBehaviour
{
    private ArrayList definitions = new ArrayList();
    private bool isInternetNotAvailable = false;

    void Start()
    {
        if(CurrentLevelData.path >= CurrentLevelData.maximumPath && CurrentLevelData.nextLevel >= CurrentLevelData.maximumPath)
        {
            SetDefaultPathValue(CurrentLevelData.maximumPath);
            UpdateAndEnableOrDisableLevelChoose(true);
            FilesJobs.SaveGameStateUntilNewLevels();
        }
        else
        {
            SetDefaultPathValue(CurrentLevelData.path);
            UpdateAndEnableOrDisableLevelChoose();
        }
        
    }

    private void UpdateAndEnableOrDisableLevelChoose(bool enableAll = false)
    {
        int NEXTLEVEL = CurrentLevelData.maximumLvlReachedByPlayer;
        for (int lvl = 1; lvl <= 15; lvl++)
        {
            int buttonName = lvl;
            Button button = GameObject.Find("Level" + buttonName)
               .GetComponent<Button>();
            if (enableAll)
            {
                button.interactable = true;
            }
            else
            {
                if (lvl <= NEXTLEVEL || lvl == 1)
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
            }
        }

        if (enableAll)
        {
            TurnOnNextPathButton(true);
            TurnOnTrophyButton(false);
        }
        else
        {
            if (NEXTLEVEL >= 16)
            {
                TurnOnTrophyButton(true);
            }
            else
            {
                TurnOnNextPathButton(false);
                TurnOnTrophyButton(false);
            }
        }
    }

    private void TurnOnNextPathButton(bool state)
    {
        Button nextPathButton = GameObject.Find("NextPath")
              .GetComponent<Button>();
        nextPathButton.interactable = state;
    }

    private void TurnOnTrophyButton(bool state)
    {
        Button nextPathButton = GameObject.Find("RewardTrophy")
              .GetComponent<Button>();
        nextPathButton.interactable = state;
    }

    private void SetDefaultPathValue(int newPath)
    {
        Text pathValue = GameObject.Find("PathValue")
              .GetComponent<Text>();
        pathValue.text = newPath.ToString();
    }

    public void ClickToGoToLevel(int lvlNumber)
    {
        RunLevelUtil runLevelUtil = new RunLevelUtil();
        runLevelUtil.RunLevel(lvlNumber);
    }

    private LevelDefinition GenerateLevelDefinition(int lvlNo, int path)
    {

        // Load from backend
        LevelLoader levelLoader = new LevelLoader();
        LevelDefinition ld = levelLoader.LoadLevel(lvlNo, path);
        ld.question = new List<FieldCoords>()
        {
            new FieldCoords(1, 8)
        };
        ld.answer = new List<FieldCoords>
        {
            new FieldCoords(8, path)
        };
        ld.chances = 1;
        ld.path = path;
        ld.levelNo = lvlNo;
        return ld;
    }

    public void ShowAnotherPath()
    {
        int path = CurrentLevelData.path;
        int newPath = path + 1;
        if (newPath >= CurrentLevelData.maximumPath + 1)
        {
            SceneManager.LoadScene(sceneName: "EndGame");
        }
        else{
            CurrentLevelData.path = newPath;
            definitions.Clear();
            UpdateCurrentData(newPath);
            UpdateAndEnableOrDisableLevelChoose();
            SetDefaultPathValue(newPath);
            CurrentLevelData.isRewardedAvaiable = false;
            GameObject.Find("BonusInfo").GetComponentInChildren<Text>().text = "";
        }
    }

    private void UpdateCurrentData(int newPathNo)
    {
        CurrentLevelData.nextLevel = 1;
        CurrentLevelData.maximumLvlReachedByPlayer = 1;
    }
}
