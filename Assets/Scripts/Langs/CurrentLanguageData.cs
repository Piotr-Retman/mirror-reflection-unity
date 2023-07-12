using System.Collections.Generic;
using UnityEngine;

class CurrentLanguageData
{
    public static Dictionary<string, string> LANGUAGE_DICTIONARY;

    public static void DisplayDictionaryEntries()
    {
        foreach(var item in LANGUAGE_DICTIONARY)
        {
            Debug.Log("{K}" + item.Key + "{V}"+item.Value);
        }
    }
}
