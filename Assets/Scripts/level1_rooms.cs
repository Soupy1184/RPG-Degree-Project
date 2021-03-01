using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level1_rooms : MonoBehaviour
{
    // this script controls which virtual camera will be active
    public GameObject virtualCamera;

    // main camera checks which virtual camera to follow
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        // checks which vc is the player colliding with, 
        // and set that to true
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCamera.SetActive(true);
        }
    }

    // main camera checks which virtual camera to stop follow    
    public virtual void OnTriggerExit2D(Collider2D other)
    {
        // checks which vc is the player colliding with when exitting, 
        // and set that to false
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCamera.SetActive(false);
        }
    }
}
