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
    public bool playerInRange;


    void Update() {
        if (Input.GetKeyDown(KeyCode.F) && playerInRange){
            if (!menu.activeInHierarchy){
                menu.SetActive(true);
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
