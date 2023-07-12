using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitOptions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var filesJobs = new FilesJobs();
        CreateSaveAndOptionsFile();
        filesJobs.LoadOptions();
        SceneManager.LoadScene("AssetProject");
        CurrentLevelData.maximumPath = 15; // TODO: EACH TIME THIS OPTION SHOULD BE UPDATED!!!
    }

    private void CreateSaveAndOptionsFile()
    {
        var dataPath = Path.Combine(Application.persistentDataPath, "save-file.json");
        if (!System.IO.File.Exists(dataPath))
        {
            //if it doesn't, create
            var jsonAsString = "{\"actualPath\":1,\"actualLevel\":1,\"actualPoints\":0,\"nextLevel\":1}";
            using (StreamWriter sw = File.CreateText(dataPath))
            {
                sw.WriteLine(jsonAsString);
            }
        }

        var optionsPath = Path.Combine(Application.persistentDataPath, "options.json");
        if (!System.IO.File.Exists(optionsPath))
        {
            //if it doesn't, create it
            var jsonAsString = "{\"playMusic\":true,\"infiniteChances\":false,\"countTime\":true,\"googlePlayGames\":false,\"soundsOn\":true,\"musicVolume\":0}";
            using (StreamWriter sw = File.CreateText(optionsPath))
            {
                sw.WriteLine(jsonAsString);
            }
        }
    }
}
