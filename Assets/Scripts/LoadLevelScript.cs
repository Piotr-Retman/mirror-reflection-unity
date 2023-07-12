using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevelScript : MonoBehaviour
{
    private List<FieldCoords> question = new List<FieldCoords>();
    private List<FieldCoords> answer = new List<FieldCoords>();

    private ArrayList correctAnswers = new ArrayList();
    LevelDefinition ld = LevelData.LevelDefinition;
    private int chances = 0;
    private int path = 0;
    private float time;
    private int minutes;

    private List<FieldCoords> answerPlaygroundFields = new List<FieldCoords>();
    // Start is called before the first frame update
    void Start()
    {
        answerPlaygroundFields.Clear();
        FillAnswerPlaygroundFields();
        StartOrRestartLevel();
    }

    private void FillAnswerPlaygroundFields()
    {
        int[] coords = new int[8];
        coords.SetValue(1, 0);
        coords.SetValue(2, 1);
        coords.SetValue(3, 2);
        coords.SetValue(4, 3);
        coords.SetValue(5, 4);
        coords.SetValue(6, 5);
        coords.SetValue(7, 6);
        coords.SetValue(8, 7);

        for (int i = 0; i < 8; i++)
        {
            int coordX = coords[i];
            foreach (int coordY in coords)
            {
                var fieldCoord = new FieldCoords(coordX, coordY);
                answerPlaygroundFields.Add(fieldCoord);
            }
        }
    }

    private void StartOrRestartLevel()
    {
        PlayerAnswers.ClearPlayerAnswers();
        CurrentLevelData.isLevelShouldBeRestarted = false;
        question = ld.question;
        answer = ld.answer;
        int lvlNo = ld.levelNo;
        path = ld.path;
        CurrentLevelData.currentChances = ld.chances;
        CurrentLevelData.levelChances = ld.chances;
        CurrentLevelData.isLevelEnd = false;
        UpdateTimeStartup();
        chances = ld.chances;
        for (int startIndex = 0; startIndex < question.Count; startIndex++)
        {
            FieldCoords coord = (FieldCoords)question[startIndex];
            int cordX = coord.getCoordX();
            int cordY = coord.getCoordY();

            Button button = GameObject.Find("Button_Q_" + cordX + "_" + cordY)
                .GetComponent<Button>();
            button.image.color = Color.yellow;
        }
        HelperNextLevel.DisableOrEnableNextLvlBtn(false);
        SetDefaultChancesValue();
        SetActualTime();
    }

    private void UpdateTimeStartup()
    {
        if (CurrentUserOptions.countTime)
        {
            decimal secondsToWin = CountHowMuchTimeShouldLvlBe();
            if (secondsToWin < 10)
            {
                CurrentLevelData.time = "00:00:0" + secondsToWin;
            }
            else
            {
                CurrentLevelData.time = "00:00:" + secondsToWin;
            }
            CurrentLevelData.secondsLeft = secondsToWin;
            time = float.Parse(secondsToWin.ToString());
            minutes = 0;
        }
        else
        {
            CurrentLevelData.time = "00:00:00";
            CurrentLevelData.secondsLeft = 0;
            time = 0.0f;
            minutes = 0;
        }
    }

    private decimal CountHowMuchTimeShouldLvlBe()
    {
        int chances = CurrentLevelData.levelChances;
        var msToWin = 500 * chances + 1500 * chances;
        decimal secondsToWin = msToWin / 1000;
        return Math.Round(secondsToWin);
    }

    private void SetDefaultChancesValue()
    {
        Text chancesText = GameObject.Find("ChancesValue")
              .GetComponent<Text>();
        if (CurrentUserOptions.infiniteChances)
        {
            chancesText.text = "\u221e";
        }
        else
        {
            chancesText.text = chances.ToString();
        }

    }

    private void SetActualTime()
    {
        Text time = GameObject.Find("Time")
              .GetComponent<Text>();
        time.text = CurrentLevelData.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentLevelData.isLevelShouldBeRestarted)
        {
            if (!CurrentLevelData.isLevelEnd)
            {
                RestartLevel();
            }
        }
        else
        {
            if (CurrentUserOptions.countTime)
            {
                if (!CurrentLevelData.isLevelEnd)
                {
                    time -= Time.deltaTime;
                    var timeAsSeconds = Math.Round(time);
                    if (time > 0)
                    {
                        if (timeAsSeconds == 60)
                        {
                            CurrentLevelData.time = "00:01:00";
                        }
                        else if (timeAsSeconds > 60)
                        {
                            var seconds = timeAsSeconds - 60;
                            if (seconds > 10)
                            {
                                CurrentLevelData.time = "00:01:" + seconds;
                            }
                            else
                            {
                                CurrentLevelData.time = "00:01:0" + seconds;
                            }
                        }
                        else
                        {
                            if (timeAsSeconds > 10)
                            {
                                CurrentLevelData.time = "00:00:" + timeAsSeconds;
                            }
                            else
                            {
                                CurrentLevelData.time = "00:00:0" + timeAsSeconds;
                            }

                        }


                        SetActualTime();
                        CurrentLevelData.secondsLeft = (int)timeAsSeconds;
                    }
                    else
                    {
                        CurrentLevelData.time = "00:00:00";
                        SetActualTime();
                        CurrentLevelData.levelWon = false;
                        CurrentLevelData.secondsLeft = (int)timeAsSeconds;
                    }
                }
            }
        }
    }

    private void RestartLevel()
    {
        ClearAnswerPlayground();
        ClearPointsValue();
        StartOrRestartLevel();
    }

    private void ClearPointsValue()
    {
        Text levelPointsValueLabel = GameObject.Find("PointsValue")
         .GetComponent<Text>();
        levelPointsValueLabel.text = "0";
    }

    private void ClearAnswerPlayground()
    {
        PlayerAnswers.playerAnswers.Clear();
        for (int startIndex = 0; startIndex < answerPlaygroundFields.Count; startIndex++)
        {
            FieldCoords coord = (FieldCoords)answerPlaygroundFields[startIndex];
            int cordX = coord.getCoordX();
            int cordY = coord.getCoordY();

            Button button = GameObject.Find("Button_A_" + cordX + "_" + cordY)
                .GetComponent<Button>();
            button.image.color = Color.red;
        }
    }
}
