using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class HelperNextLevel
 {
    public static void DisableOrEnableNextLvlBtn(bool state)
    {
        Button button = GameObject.Find("NextLevel")
               .GetComponent<Button>();
        button.interactable = state;

        Button closePlayground = GameObject.Find("ClosePlayground")
               .GetComponent<Button>();
        closePlayground.interactable = !state;
    }
}
