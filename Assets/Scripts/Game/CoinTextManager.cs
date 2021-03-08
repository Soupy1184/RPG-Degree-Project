using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinTextManager : MonoBehaviour
{
    public Inventory playerInventory;
    public Text coinText;

    void Start() {
        coinText.text = "x " + playerInventory.coins;
    }

    public void UpdateCoinCount() {
        coinText.text = "x " + playerInventory.coins;
    } 
}
