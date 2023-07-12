using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoInternetTextLoader : MonoBehaviour
{
    private void OnGUI()
    {
        SceneTextsLoader.LoadNoInternetTranslations();
    }
}
