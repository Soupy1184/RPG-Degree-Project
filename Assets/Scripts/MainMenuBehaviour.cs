using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
	public void LoadLevel(string levelName){
		SceneManager.LoadScene(levelName);
	}
    public void QuitGame(){
    	#if UNITY_EDITOR
    		UnityEditor.EditorApplication.isPlaying = false;
    	#else
    		Application.Quit();
    	#endif
    }
}
