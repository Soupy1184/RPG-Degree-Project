using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MainMenuBehaviour
{
	public static bool isPaused;
	public GameObject pauseMenu;
	public GameObject optionsMenu;
    public GameObject deathMenu;
    public GameObject inventory;
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        inventory.SetActive(false);

        UpdateQualityLabel();
        UpdateVolumeLabel();
    }

    // Update is called once per frame
    void Update()
    {
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
        //open and close inventory
        else if(Input.GetKeyUp(KeyCode.I)){
            if (!inventory.activeInHierarchy){
                isPaused = !isPaused;
                Time.timeScale = (isPaused) ? 0 : 1;
                inventory.SetActive(isPaused);
            }
            else{
                CloseInventory();
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


    public void IncreaseQuality(){
    	QualitySettings.IncreaseLevel();
    	UpdateQualityLabel();
    }
    public void DecreaseQuality(){
    	QualitySettings.DecreaseLevel();
    	UpdateQualityLabel();
    }

    public void SetVolume(float value){
    	AudioListener.volume = value;
    	UpdateVolumeLabel();
    }

    private void UpdateQualityLabel(){
    	int currentQuality = QualitySettings.GetQualityLevel();
    	string qualityName = QualitySettings.names[currentQuality];

    	optionsMenu.transform.Find("Quality Level").GetComponent<UnityEngine.UI.Text>().text = "Quality Level - " + qualityName;
    }

    private void UpdateVolumeLabel(){
    	optionsMenu.transform.Find("Master Volume").GetComponent<UnityEngine.UI.Text>().text = "Master Volume - " + (AudioListener.volume * 100).ToString("f2") + "%";
    }

    public void CloseInventory(){
        isPaused = false;
        inventory.SetActive(false);
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

    IEnumerator Death(){
        yield return new WaitForSeconds(2.5f);
        deathMenu.SetActive(isPaused);
        Time.timeScale = (isPaused) ? 0 : 1;
    }
}
