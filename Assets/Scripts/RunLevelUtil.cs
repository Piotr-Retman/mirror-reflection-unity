using UnityEngine.SceneManagement;

public class RunLevelUtil
{
    public void RunLevel(int lvlNumber)
    {
        int pathNo = CurrentLevelData.path;
        LevelLoader levelLoader = new LevelLoader();
        LevelDefinition ld = levelLoader.LoadLevel(lvlNumber, pathNo);
        if (ld == null)
        {
            SceneManager.LoadScene(sceneName: "NoInternetConnection");
        }
        else
        {
            if(ld.levelNo == -1 && ld.path == -1)
            {
                SceneManager.LoadScene(sceneName: "EndGame");
            }
            else
            {
                LevelData.LevelDefinition = ld;
                SceneManager.LoadScene(sceneName: "Playground");
            }
        }
    }
}

