using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    public void NextLevelClicked()
    {
        CurrentLevelData.isLevelEnd = false;
        PlayerAnswers.ClearPlayerAnswers();
        SceneManager.LoadScene(sceneName: "ChooseLvlScene");
    }
}
