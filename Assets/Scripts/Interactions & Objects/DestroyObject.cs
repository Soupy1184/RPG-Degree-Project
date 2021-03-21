//chris campbell - february 2021
//resource: https://www.youtube.com/watch?v=EjeteWtaIEM&list=PL4vbr3u7UKWp0iM1WIfRjCDTI03u43Zfu&index=15

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
