//chris campbell - march 19, 2021
//this script sets the quest to the scriptable object quest

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

    //called everytime the quest menu is opened
    private void OnEnable(){
        SetQuestUI();
    }

    public void SetQuestUI(){
        //if there is an active quest set the UI
        if(quest.isActive){
            title.text = quest.title;
            description.text = quest.description;
            experienceReward.text = "Exeperience: \n" + quest.experienceReward;
            goldReward.text = "Reward: \n" + quest.goldReward;
        }
        else{ //set to default if no active quest
            title.text = "";
            description.text = "";
            experienceReward.text = "Exeperience: \n";
            goldReward.text = "Reward: \n";
        }
    }
}
