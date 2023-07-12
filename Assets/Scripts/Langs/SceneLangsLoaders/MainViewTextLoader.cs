using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainViewTextLoader : MonoBehaviour
{
    private void OnGUI()
    {
        SceneTextsLoader.LoadWelcomeViewTranslations();
    }
}
