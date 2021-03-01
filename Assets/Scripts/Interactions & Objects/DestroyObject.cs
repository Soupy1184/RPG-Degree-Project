using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public void ObjectDestroy(){
    	StartCoroutine(DetroyCo());
    }

    IEnumerator DetroyCo(){
    	yield return new WaitForSeconds(.3f);
    	this.gameObject.SetActive(false);
    }
}
