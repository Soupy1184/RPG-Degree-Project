using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
	public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;
   	public GameObject player;
    public Vector3 initialPosition;

    void Start() {
        if(needText && player.transform.position == initialPosition){
           	StartCoroutine(placeNameCo());
        }
    }

    //shows text 
    private IEnumerator placeNameCo(){
    	text.SetActive(true);
    	placeText.text = placeName;
    	yield return new WaitForSeconds(3f);
    	text.SetActive(false);
    }
}
