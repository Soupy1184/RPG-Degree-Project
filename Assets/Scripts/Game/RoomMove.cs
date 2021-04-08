/*
    written by: Afieqha Mieza
    date: January 2021
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    // take position vector of change of position
    public Vector3 playerChange;
    // reference to camera
    private CameraFollow cam;
    
    // Start is called before the first frame update
    void Start()
    {
        // get main camera
        cam = Camera.main.GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // when player's position collide with the door 
    // move the player to next room
    // at a position (based on playerChange)
    private void OnTriggerEnter2D (Collider2D other){
        if (other.CompareTag("Player"))
        {
            other.transform.position += playerChange;
        }
    }
}
