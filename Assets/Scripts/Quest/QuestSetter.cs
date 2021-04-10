//chris campbell - march 18, 2021
//resource: https://www.youtube.com/watch?v=e7VEe_qW4oE
//this script manages setting the quest scriptable object

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestSetter : MonoBehaviour
{
    public Quest quest;

    public PlayerController player;

    public string questTitle;
    public string questDescrition;
    public int questReward;
    public int questExperience;

    public void SetQuest(){
        quest.isActive = true;
        quest.title = questTitle;
        quest.description = questDescrition;
        quest.goldReward = questReward;
        quest.experienceReward = questExperience;
    }
}