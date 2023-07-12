using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartHandler : MonoBehaviour
{
    public void RestartLevel()
    {
        CurrentLevelData.isLevelShouldBeRestarted = true;
    }
}
