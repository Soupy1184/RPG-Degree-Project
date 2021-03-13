using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : LootDropObject
{
    public void ObjectDestroy(){
    	StartCoroutine(DetroyCo());
    }

    IEnumerator DetroyCo(){
    	yield return new WaitForSeconds(.3f);
    	this.gameObject.SetActive(false);
        Instantiate(lootDrop, transform.position, Quaternion.identity);
    }
}
