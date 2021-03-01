using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    // take position vector of change of position
    public Vector3 playerChange;

    private CameraFollow cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // when player's position collide with the door 
    // move the player to next room
    // at a position
    private void OnTriggerEnter2D (Collider2D other){
        if (other.CompareTag("Player"))
        {
            other.transform.position += playerChange;
        }
    }
}
