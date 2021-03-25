using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
          StartCoroutine(DieAfterTime(1f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private IEnumerator DieAfterTime(float waitTime) {
          yield return new WaitForSeconds(waitTime);
          Debug.Log("Delete enemy now");
          Destroy(this.gameObject);

     }
}
