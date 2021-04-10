//chris campbell - february 2021
//resource: https://www.youtube.com/watch?v=JcEJtEWjiZU&list=PL4vbr3u7UKWp0iM1WIfRjCDTI03u43Zfu&index=31
//this script manages the transistion between scenes

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    //starts the fade in - creates and destroys the panel with animation
    private void Awake() {
        if (fadeInPanel != null){
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1); 
        }
        Debug.Log("Entered Scene");
    }
    
    //when player enters scene transition collider
    public void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger){
            //set player position
            playerStorage.initialValue = playerPosition;
            StartCoroutine(FadeCo());
        } 
    }

    //start the fade out 
    public IEnumerator FadeCo() {
        if (fadeOutPanel != null){
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);       
        }
        yield return new WaitForSeconds (fadeWait);
        //load new scene while fade animations are happening
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        Debug.Log("Exiting Scene");
        while(!asyncOperation.isDone){
            yield return null;
        }

    }
}
