//chris campbell - march 19, 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestMenuManager : MonoBehaviour
{
    public Quest quest;

    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI experienceReward;
    public TextMeshProUGUI goldReward;
    private void OnEnable(){
        SetQuestUI();
    }

    public void SetQuestUI(){
        if(quest.isActive){
            title.text = quest.title;
            description.text = quest.description;
            experienceReward.text = "Exeperience: \n" + quest.experienceReward;
            goldReward.text = "Reward: \n" + quest.goldReward;
        }
        else{
            title.text = "";
            description.text = "";
            experienceReward.text = "Exeperience: \n";
            goldReward.text = "Reward: \n";
        }
    }
}
