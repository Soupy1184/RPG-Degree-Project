// Zachary Moorman

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
     public Quest quest;
     public Inventory inventory;
     private bool playerInRange;
     private bool opened;
     public SignalSender coin;
     public Animator chestAnimator;

    // Start is called before the first frame update
    void Start()
    {
          opened = false;
    }

    // Update is called once per frame
    void Update()
    {
          if (playerInRange && opened == false && quest.isActive) {
               if (Input.GetKeyDown(KeyCode.F)) {
                    if (quest.title == "The Trials of the Hero") {
                         opened = true;
                         inventory.coins += quest.goldReward;
                         coin.Raise();
                         chestAnimator.SetTrigger("open");

                         quest.title = "Demo Completed!";
                         quest.description = "You have successfully completed the trial, and have completed the game in its current state. Congratulations!";
                         quest.experienceReward = 0;
                         quest.goldReward = 0;
                    }
               }
          }
     }

     private void OnTriggerEnter2D(Collider2D other) {
          if (other.CompareTag("Player") && !other.isTrigger) {
               playerInRange = true;
          }
     }

     private void OnTriggerExit2D(Collider2D other) {
          if (other.CompareTag("Player") && !other.isTrigger) {
               playerInRange = false;
          }
     }
}
