//chris campbell - april 3, 2021
//resource: https://www.youtube.com/watch?v=CPKAgyp8cno
//for testing purposes, not implemented

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static PersistentManagerScript Instance {get; private set; }

    public int value;
    public string scene;

    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
}
