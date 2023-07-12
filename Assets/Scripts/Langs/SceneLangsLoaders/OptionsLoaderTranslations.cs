using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsLoaderTranslations : MonoBehaviour
{
    private void OnGUI()
    {
        SceneTextsLoader.LoadOptionsTranslations();
    }
}
