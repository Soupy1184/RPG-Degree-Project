//chris campbell - march 2021
//manages the interaction for the shop

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject shopMenu;
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogObject;
    [SerializeField] Text dialogText;
    public string objectText;
	public string dialog;
	public bool playerInRange;

    void Update() {
        //manage dialog box
        if (Input.GetKeyDown(KeyCode.F) && playerInRange){
            if (!menu.activeInHierarchy && !shopMenu.activeInHierarchy && !dialogBox.activeInHierarchy){
                dialogBox.SetActive(true);
                menu.SetActive(true);
                dialogText.text = dialog;
        		dialogObject.text = objectText;
            }
        }
    }

    //player enters collider
    private void OnTriggerEnter2D(Collider2D other){
    	if (other.CompareTag("Player")){
    		playerInRange = true;
    	}
    }

    //if players runs away with no actions
    private void OnTriggerExit2D(Collider2D other){
    	if (other.CompareTag("Player")){
            menu.SetActive(false);
            menu.SetActive(false);
            shopMenu.SetActive(false);
    		playerInRange = false;
    	}
    }
    

    public void openShop(){
        menu.SetActive(false);
        shopMenu.SetActive(true);
        dialogBox.SetActive(false);
    }

    public void closeShop(){
        menu.SetActive(false);
        dialogBox.SetActive(false);
    }
}
