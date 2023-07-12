using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

class LevelLoader
{

    public LevelDefinition LoadLevel(int levelNo, int pathNo)
    {
        LevelDefinition ld = null;
        try
        {
            var filePath = String.Format("Levels/{0}_{1}", pathNo,levelNo);
            TextAsset jsonLevelsFile = Resources.Load<TextAsset>(filePath);
            var jsonObject = JsonUtility.FromJson<LevelDefinitionBE>(jsonLevelsFile.text);
            ld = CreateLevelDefinitionBasedOnBe(jsonObject);
            CurrentLevelData.actualLevel = levelNo;
            //ld = createDummyLevel();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Data);
        }
        return ld;
    }

    private LevelDefinition createDummyLevel()
    {
        LevelDefinition ld = new LevelDefinition
        {
            levelNo = 1,
            path = 1,
            answer = ParseAnswerOrQuestion("1,8;1,6;1,7;2,8;2,7;3,8;4,8;5,8;5,7;6,8;6,7;6,6;7,8;7,7;8,8;5,6;5,5;4,7;4,6;3,7;2,6;1,5;1,4;"),
            question = ParseAnswerOrQuestion("8,1;6,1;7,1;8,2;7,2;8,3;8,4;8,5;7,5;8,6;7,6;6,6;8,7;7,7;8,8;6,5;5,5;7,4;6,4;7,3;6,2;5,1;4,1;"),
            chances = 23
        };
        return ld;
    }

    private LevelDefinition CreateLevelDefinitionBasedOnBe(LevelDefinitionBE ldBe)
    {
        LevelDefinition ld = new LevelDefinition
        {
            levelNo = ldBe.levelNo,
            path = ldBe.path,
            answer = ParseAnswerOrQuestion(ldBe.answer),
            question = ParseAnswerOrQuestion(ldBe.question),
            chances = ldBe.chances
        };
        return ld;
    }

    private List<FieldCoords> ParseAnswerOrQuestion(string listAsString)
    {
        List<FieldCoords> coords = new List<FieldCoords>();
        var splittedList = listAsString.Split(';');
        for (var i = 0; i < splittedList.Length; i++)
        {
            if (splittedList[i].Length > 0)
            {
                var splittedPoint = splittedList[i].Split(',');
                var coordX = splittedPoint[0];
                var coordY = splittedPoint[1];
                FieldCoords fieldCoords = new FieldCoords(int.Parse(coordX), int.Parse(coordY));
                coords.Add(fieldCoords);
            }
        }
        return coords;
    }
}

