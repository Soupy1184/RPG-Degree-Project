using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenuBehaviour : MonoBehaviour
{
	public GameSaveManager gameSave;

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
