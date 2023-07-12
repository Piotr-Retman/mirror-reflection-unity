using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class RewardButtonBehaviour : MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("BonusInfo").GetComponentInChildren<Text>().text = "";
    }

    public void Reward()
    {
        CurrentLevelData.isSpecialRewardEnable = true;
        CurrentLevelData.isRewardedAvaiable = true;
        TurnOffTrophyButton();
    }

    private void TurnOffTrophyButton()
    {
        Button rewardTrophy = GameObject.Find("RewardTrophy")
              .GetComponent<Button>();
        rewardTrophy.interactable = false;

        Button nextPathButton = GameObject.Find("NextPath")
             .GetComponent<Button>();
        nextPathButton.interactable = true;
    }
}
