//chris campbell - january 2021
//resource: https://www.youtube.com/watch?v=NwsUxJ3kDR4&list=PL4vbr3u7UKWp0iM1WIfRjCDTI03u43Zfu&index=7
//this script is for making the camera follow the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    void Start(){
        //set camera position
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	if (transform.position != target.position){
        	Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            //set position of camera to position of player
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            //make camera motion smooth
        	transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
    	}
    }
}
