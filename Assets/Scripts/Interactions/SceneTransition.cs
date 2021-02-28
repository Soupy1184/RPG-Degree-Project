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
    }
    

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger){
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
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while(!asyncOperation.isDone){
            yield return null;
        }
    }
}
