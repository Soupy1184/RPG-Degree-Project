using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ObjectDestroy(){
    	StartCoroutine(DetroyCo());
    }

    IEnumerator DetroyCo(){
    	yield return new WaitForSeconds(.3f);
    	this.gameObject.SetActive(false);
    }
}
