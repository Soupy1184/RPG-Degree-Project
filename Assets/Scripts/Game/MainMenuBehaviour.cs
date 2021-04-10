//chris campbell - january 2021
//resource: Unity 5.x Game Development Blueprints by John P. Doran
//this script manages the main title screen of the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
