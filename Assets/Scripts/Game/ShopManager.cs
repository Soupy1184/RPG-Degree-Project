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
    public bool playerInRange;
    void Update() {
        if (Input.GetKeyDown(KeyCode.F) && playerInRange){
        	menu.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
    	if (other.CompareTag("Player")){
    		playerInRange = true;
    	}
    }

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
    }

    public void closeShop(){
        menu.SetActive(false);
    }
}
