using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChooseTextLoader : MonoBehaviour
{
    private void OnGUI()
    {
        SceneTextsLoader.LoadChooseLevelTranslations();
    }
}
