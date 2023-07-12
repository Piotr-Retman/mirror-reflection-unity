using UnityEngine;
using UnityEngine.UI;

public class StatsLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        UpdateActualPointsValue();
        UpdateLevelValue();
        UpdatePathValue();
    }

    void Update()
    {
        //TODO: Make animation of counts (refresh each sec)
    }

    private void UpdatePathValue()
    {
        Text winText = GameObject.Find("PathValue")
         .GetComponent<Text>();
        winText.text = CurrentLevelData.path.ToString();
    }

    private void UpdateLevelValue()
    {
        Text winText = GameObject.Find("LevelValue")
         .GetComponent<Text>();
        winText.text = CurrentLevelData.maximumLvlReachedByPlayer.ToString();
    }

    private void UpdateActualPointsValue()
    {
        Text winText = GameObject.Find("PointsValue")
          .GetComponent<Text>();
        winText.text = CurrentLevelData.loadedPoints.ToString();
    }
}
