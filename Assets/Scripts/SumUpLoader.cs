using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SumUpLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (CurrentLevelData.isLevelEnd && CurrentLevelData.levelWon)
        {
            SceneManager.LoadScene(8);
        }
    }
}
