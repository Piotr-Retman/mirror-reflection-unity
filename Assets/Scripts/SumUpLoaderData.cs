using UnityEngine;
using UnityEngine.UI;

public class SumUpLoaderData: MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("LevelValueSumUp").GetComponentInChildren<Text>().text = SumUpLevelData.levelNumber;
        GameObject.Find("PathValueSumUp").GetComponentInChildren<Text>().text = SumUpLevelData.pathNumber;
        GameObject.Find("PointsValueSumUp").GetComponentInChildren<Text>().text = SumUpLevelData.currentLevelPoints;
        GameObject.Find("BonusValueSumUp").GetComponentInChildren<Text>().text = "+" + SumUpLevelData.bonusValue;
        // STATUS
        GameObject.Find("SumValueSumUp").GetComponentInChildren<Text>().text = SumUpLevelData.sumBonusAndLevelPoints;
        GameObject.Find("AllValueSumUp").GetComponentInChildren<Text>().text = SumUpLevelData.allPoints;
    }
}
