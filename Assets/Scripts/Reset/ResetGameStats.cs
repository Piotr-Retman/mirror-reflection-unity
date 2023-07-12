using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGameStats : MonoBehaviour
{
    public void ResetGame()
    {
        FilesJobs.ResetGameState();
    }

    public void SaveStatusUntilNewLevels()
    {
        FilesJobs.SaveGameStateUntilNewLevels();
    }
}
