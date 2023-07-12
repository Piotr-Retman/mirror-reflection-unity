using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerPanelActionScript : MonoBehaviour
{
    private List<FieldCoords> answers = new List<FieldCoords>();

    LevelDefinition ld = null;
    PointCounter pc = new PointCounter();
    ArrayList pa = PlayerAnswers.playerAnswers;
    ArrayList paFields = PlayerAnswers.playerAnswersFields;
    int countedPoints = 0;
    int lvlNo = 0;
    bool lvlWon = false;
    PlayMusicEffect musicEffect = new PlayMusicEffect();

    bool helperWon = false;

    private void HideShowWinText(string text, bool enabled)
    {
        Text winText = GameObject.Find("WinText")
             .GetComponent<Text>();
        winText.text = text;
        winText.enabled = enabled;
    }

    void Update()
    {
        if (CurrentUserOptions.countTime && CurrentUserOptions.infiniteChances && !helperWon)
        {
            helperWon = isLevelWonWhenInfiniteChances();

            if (helperWon)
            {
                CountPointsWhenInfiniteChances();
                FinalizeLevel();
            }
            else
            {
                if (CurrentLevelData.secondsLeft == 0)
                {
                    FinalizeLevel();
                }
            }
        }
        else if (CurrentUserOptions.countTime && !helperWon)
        {
            if (CurrentLevelData.secondsLeft == 0)
            {
                FinalizeLevel();
            }
        }
    }

    void Start()
    {
        PlayerAnswers.ClearPlayerAnswers();
        ld = LevelData.LevelDefinition;
        lvlNo = CurrentLevelData.nextLevel;
        CurrentLevelData.isLevelEnd = false;
        HideShowWinText("", false);
        HelperNextLevel.DisableOrEnableNextLvlBtn(false);
        answers = ld.answer;
    }

    public void Answer(int coordX, int coordY)
    {
        if (CurrentUserOptions.infiniteChances)
        {
            AnswerWhenInfiniteChances(coordX, coordY);
        }
        else
        {
            AnswerWhenNotInfiniteChances(coordX, coordY);
        }
    }

    private void AnswerWhenInfiniteChances(int coordX, int coordY)
    {
        answers = ld.answer;
        if (!CurrentLevelData.isLevelEnd)
        {
            var fieldCoordActual = new FieldCoords(coordX, coordY);
            var used = isFieldUsed(fieldCoordActual);

            if (used)
            {
                ChangeColorOfButtonTo(coordX, coordY, Color.red);
                var index = -1;

                for (int i = 0; i < pa.Count; i++)
                {
                    var fieldCoordLocal = pa[i] as FieldCoords;
                    if (fieldCoordLocal.getCoordX().Equals(coordX) &&
                    fieldCoordLocal.getCoordY().Equals(coordY))
                    {
                        index = i;
                    }

                };

                if (index > -1)
                {
                    pa.RemoveAt(index);
                }
            }
            else
            {
                ChangeColorOfButtonTo(coordX, coordY, Color.yellow);
                pa.Add(fieldCoordActual);
            }

            var won = isLevelWonWhenInfiniteChances();

            if (won)
            {
                CountPointsWhenInfiniteChances();
                FinalizeLevel();
            }
        }
    }

    private void CountPointsWhenInfiniteChances()
    {
        countedPoints = pc.CountPoints(countedPoints, pa.Count);
        UpdateVisibleLevelPoints();

        CurrentLevelData.currentPoints = countedPoints;
    }

    private bool isLevelWonWhenInfiniteChances()
    {
        bool won = false;

        List<Boolean> foundFields = new List<Boolean>();

        if (answers.Count.Equals(pa.Count))
        {
            for (var i = 0; i <= answers.Count; i++)
            {
                var answerCoords = answers[i];
                FieldCoords found = null;

                for (int j = 0; j < pa.Count; j++)
                {
                    FieldCoords fieldChosen = null;
                    if (paFields.Count > 0)
                    {
                        fieldChosen = paFields[j] as FieldCoords;
                    }

                    if(fieldChosen == null)
                    {
                        fieldChosen = pa[j] as FieldCoords;
                    }

                    if (fieldChosen.getCoordX().Equals(answerCoords.getCoordX()) &&
                    fieldChosen.getCoordY().Equals(answerCoords.getCoordY()))
                    {
                        found = fieldChosen;
                        break;
                    }
                }

                if (found != null)
                {
                    foundFields.Add(true);
                    if (foundFields.Count.Equals(answers.Count))
                    {
                        won = true;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
        return won;
    }

    private bool isFieldUsed(FieldCoords fieldCoords)
    {
        bool used = false;
        if (pa.Count > 0)
        {
            for (int usedFieldIndex = 0; usedFieldIndex < pa.Count; usedFieldIndex++)
            {
                var fieldUsed = pa[usedFieldIndex] as FieldCoords;
                if (fieldUsed.getCoordX().Equals(fieldCoords.getCoordX()) &&
                    fieldUsed.getCoordY().Equals(fieldCoords.getCoordY()))
                {
                    used = true;
                    break;
                }
            }
        }
        return used;
    }

    private void AnswerWhenNotInfiniteChances(int coordX, int coordY)
    {
        answers = ld.answer;
        if (!CurrentLevelData.isLevelEnd)
        {
            ChangeColorOfButtonTo(coordX, coordY, Color.yellow);
            if (CurrentLevelData.currentChances > 0)
            {
                var chances = CurrentLevelData.currentChances - 1;
                CurrentLevelData.currentChances = chances;
                UpdateVisibleLevelChances();

                if (pa.Count != answers.Count)
                {
                    foreach (FieldCoords coords in answers)
                    {
                        if (coords.getCoordX().Equals(coordX) && coords.getCoordY().Equals(coordY) && isNotAlreadySet(coordX, coordY))
                        {
                            pa.Add(true);
                            paFields.Add(coords);
                            countedPoints = pc.CountPoints(countedPoints, pa.Count);
                            UpdateVisibleLevelPoints();

                            CurrentLevelData.currentPoints = countedPoints;
                        }
                    }
                    if (CurrentLevelData.currentChances == 0)
                    {
                        FinalizeLevel();
                    }
                }
                else
                {
                    FinalizeLevel();
                }
            }
            else
            {
                FinalizeLevel();
            }
        }
    }

    private bool isNotAlreadySet(int coordX, int coordY)
    {
        bool isNotAlreadySet = true;
        foreach(FieldCoords answer in paFields)
        {
            if(answer.getCoordX().Equals(coordX) && answer.getCoordY().Equals(coordY))
            {
                isNotAlreadySet = false;
                break;
            }
        }
        return isNotAlreadySet;
    }

    private void InterpretSolution()
    {
        if (pa.Count.Equals(answers.Count) && isLevelWonWhenInfiniteChances())
        {
            HideShowWinText(CurrentLanguageData.LANGUAGE_DICTIONARY["YOU_WON"], true);
            lvlWon = true;
            CurrentLevelData.levelWon = true;
            musicEffect.PlayMusic("level_won");
        }
        else
        {
            HideShowWinText(CurrentLanguageData.LANGUAGE_DICTIONARY["YOU_LOSE"], true);
            lvlWon = false;
            CurrentLevelData.currentPoints = 0;
            CurrentLevelData.levelWon = false;
            musicEffect.PlayMusic("level_failed");
        }
        BlockRestartButton();
    }

    private void BlockRestartButton()
    {
        Button restartButton = GameObject.Find("RestartLevel").GetComponent<Button>();
        restartButton.interactable = false;
    }

    private void FinalizeLevel()
    {
        CurrentLevelData.isLevelEnd = true;
        InterpretSolution();
        if (CurrentLevelData.nextLevel + 1 - lvlNo == 1 || CurrentLevelData.nextLevel + 1 - lvlNo == 0)
        {
            if (lvlWon)
            {
                UpdateVisibleLevelPoints();
                if (CurrentLevelData.actualLevel == CurrentLevelData.maximumLvlReachedByPlayer)
                {
                    CurrentLevelData.nextLevel = CurrentLevelData.actualLevel + 1;
                    CurrentLevelData.maximumLvlReachedByPlayer = CurrentLevelData.nextLevel;
                    CurrentLevelData.currentPoints = pc.CountBonus(countedPoints);
                }
                else
                {
                    CurrentLevelData.currentPoints = 0;
                }
            }
            Debug.Log(CurrentLevelData.nextLevel);
            Debug.Log("Max Lvl"+CurrentLevelData.maximumLvlReachedByPlayer);
            HelperNextLevel.DisableOrEnableNextLvlBtn(true);
            Debug.Log("Level can be finalized!");
        }
        else
        {
            HelperNextLevel.DisableOrEnableNextLvlBtn(true);
        }
        
    }

    private void UpdateVisibleLevelChances()
    {
        Text levelPointsValueLabel = GameObject.Find("ChancesValue").GetComponent<Text>();
        if (CurrentUserOptions.infiniteChances)
        {
            levelPointsValueLabel.text = "∞";
        }
        else
        {
            levelPointsValueLabel.text = CurrentLevelData.currentChances.ToString();
        }
    }

    private void UpdateVisibleLevelPoints()
    {
        Text levelPointsValueLabel = GameObject.Find("PointsValue")
         .GetComponent<Text>();
        levelPointsValueLabel.text = countedPoints.ToString();
    }

    private void ChangeColorOfButtonTo(int coordX, int coordY, Color color)
    {
        Button button = GameObject.Find("Button_A_" + coordX + "_" + coordY)
         .GetComponent<Button>();
        button.image.color = color;
    }

    public void SendAnswer(string coordinates)
    {
        if (!CurrentLevelData.isLevelEnd)
        {
            char delimitter = ',';
            string[] values = coordinates.Split(delimitter);
            Answer(int.Parse(values[0]), int.Parse(values[1]));
        }
    }
}
