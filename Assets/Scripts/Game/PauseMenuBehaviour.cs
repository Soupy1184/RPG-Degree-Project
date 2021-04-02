﻿//chris campbell - february 2021
//resource: Unity 5.x Game Development Blueprints by John P. Doran

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuBehaviour : MainMenuBehaviour
{
	public static bool isPaused;
	public GameObject pauseMenu;
	public GameObject optionsMenu;
    public GameObject deathMenu;
    public GameObject inventory;
    public GameObject quests;
    public GameObject equipment;
    public GameObject controlsMenu;
    public GameObject player;
    public int currentLevel;

    public bool infoIsActive = true;
    public Text infoText;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        inventory.SetActive(false);

        UpdateInfoHelp();
        UpdateVolumeLabel();

     }

    // Update is called once per frame
    void Update()
    {
        UpdateInfoHelp();
        //open options menu on key hit escape 
        if (Input.GetKeyUp("escape")){
            if(!optionsMenu.activeInHierarchy){
                isPaused = !isPaused;
                Time.timeScale = (isPaused) ? 0 : 1;
                pauseMenu.SetActive(isPaused);
            }
            else
            {
                OpenPauseMenu();
            }
        	
        }
        //open and close inventory hit I on keyboard
        else if(Input.GetKeyUp(KeyCode.I)){
            if (!inventory.activeInHierarchy){
                OpenInventory();
            }
            else{
                CloseInventory();
            }
        } 
        //open and close Quests hit Q on keyboard
        else if(Input.GetKeyUp(KeyCode.Q)){
            if (!quests.activeInHierarchy){
                OpenQuests();
            }
            else{
                CloseQuests();
            }
        } 
        //open and close Equipment hit E on keyboard
        else if(Input.GetKeyUp(KeyCode.E)){
            if (!equipment.activeInHierarchy){
                OpenEquipment();
            }
            else{
                CloseEquipment();
            }
        } 
        else if(player.isDead){//if player is dead, start death options menu
            isPaused = true;

            StartCoroutine("Death");
        }
    }

    public void ResumeGame(){
    	isPaused = false;
    	pauseMenu.SetActive(false);
    	Time.timeScale = 1;
    }
    
    public void SetVolume(float value){
    	AudioListener.volume = value;
    	UpdateVolumeLabel();
    }

    private void UpdateVolumeLabel(){
    	optionsMenu.transform.Find("Master Volume").GetComponent<UnityEngine.UI.Text>().text = "Master Volume - " + (AudioListener.volume * 100).ToString("f2") + "%";
    }

    //chris campbell - updated march 2021 week 8
    //this function separate from UpdateInfoHelp for onClick
    public void OnClickInfoUpdate(){
        infoIsActive = !infoIsActive;
    }

    //this function separate to be called per frame in update method
    private void UpdateInfoHelp(){
        if (infoIsActive){
            infoText.text = "Turn Off Info Help";
        }
        else{
            infoText.text = "Turn On Info Help";
        }
    }

    public void OnClickInventoryButton(){
        if (!inventory.activeInHierarchy){
            OpenInventory();
        }
        else{
            CloseInventory();
        }
    }

    public void OnClickQuests(){
        if (!quests.activeInHierarchy){
            OpenQuests();
        }
        else{
            CloseQuests();
        }
    }

    public void OnClickEquipment(){
        if (!equipment.activeInHierarchy){
            OpenEquipment();
        }
        else{
            CloseEquipment();
        }
    }

    public void OpenInventory(){
        isPaused = true;
        Time.timeScale = (isPaused) ? 0 : 1;
        inventory.SetActive(true);
        quests.SetActive(false);
        equipment.SetActive(false);
    }

    public void CloseInventory(){
        isPaused = false;
        inventory.SetActive(false);
    	Time.timeScale = 1;
    }

    public void OpenQuests(){
        isPaused = true;
        Time.timeScale = (isPaused) ? 0 : 1;
        quests.SetActive(true);
        inventory.SetActive(false);
        equipment.SetActive(false);
    }

    public void CloseQuests(){
        isPaused = false;
        quests.SetActive(false);
    	Time.timeScale = 1;
    }

    public void OpenEquipment(){
        isPaused = true;
        Time.timeScale = (isPaused) ? 0 : 1;
        quests.SetActive(false);
        inventory.SetActive(false);
        equipment.SetActive(true);
        
    }

    public void CloseEquipment(){
        isPaused = false;
        equipment.SetActive(false);
    	Time.timeScale = 1;
    }

    public void OpenOptions(){
    	optionsMenu.SetActive(true);
    	pauseMenu.SetActive(false);
    }

    public void OpenPauseMenu(){
    	optionsMenu.SetActive(false);
    	pauseMenu.SetActive(true);
    }

    //chris campbell
    //updated - march 21
    public void OpenControlsMenu(){
        controlsMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void CloseControlsMenu(){
        controlsMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    IEnumerator Death(){
        yield return new WaitForSeconds(2.5f);
        deathMenu.SetActive(isPaused);
        Time.timeScale = (isPaused) ? 0 : 1;
    }
}
