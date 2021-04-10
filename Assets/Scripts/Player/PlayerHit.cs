//chris campbell - february 2021
//resource: https://www.youtube.com/watch?v=EjeteWtaIEM&list=PL4vbr3u7UKWp0iM1WIfRjCDTI03u43Zfu&index=15
//this script manages the objects in the world that can be hit by the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        //if player hits a n object with a breakable tag - pots, barrels, etc.
    	if(other.CompareTag("Breakable")){
    		other.GetComponent<DestroyObject>().ObjectDestroy();
    	}
    }
}
