using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoFirstObjects : MonoBehaviour
{
    public GameObject sameFirst;
    // public GameObject self;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other){
    	if (other.CompareTag("Player")){
    		if(sameFirst.activeSelf == true){
                sameFirst.SetActive(false);
            }
    	}
    }
}
