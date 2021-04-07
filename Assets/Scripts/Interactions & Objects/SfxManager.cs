using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public AudioSource playerSwing;
    public AudioSource openChest;
    public AudioSource coins;
    public AudioSource hurt;

    private static bool sfxManExists;
    // Start is called before the first frame update
    void Start()
    {
        if(!sfxManExists){
            sfxManExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }else{
            Destroy(gameObject); 
        }
    }
}

    
