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

    void Start()
    {
        if(needText){
           	StartCoroutine(placeNameCo());
        }
    }

    private IEnumerator placeNameCo(){
    	text.SetActive(true);
    	placeText.text = placeName;
    	yield return new WaitForSeconds(4f);
    	text.SetActive(false);
    }
}
