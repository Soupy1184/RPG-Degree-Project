using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChangeMax;
    public Vector2 cameraChangeMin;
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

    private void OnTriggerEnter2D (Collider2D other){
        if (other.CompareTag("Player"))
        {
            cam.minPosition.x = cameraChangeMin.x;
            cam.minPosition.y = cameraChangeMin.y;
            cam.maxPosition.x = cameraChangeMax.x;
            cam.maxPosition.y = cameraChangeMax.y;
            other.transform.position += playerChange;
        }
    }
}
