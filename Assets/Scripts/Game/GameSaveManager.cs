//chris campbell - february 2021
//resource: https://www.youtube.com/watch?v=F4oG4Yvic5k&list=PL4vbr3u7UKWp0iM1WIfRjCDTI03u43Zfu&index=68

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameSaveManager : MonoBehaviour
{
    public List<ScriptableObject> objects = new List<ScriptableObject>();

    private void OnEnable(){
        LoadScriptables();
    }

    private void OnDisable(){
        SaveScriptables();
    }   

    public void ResetScriptables(){
        //deletes all the saved scriptable objects data
        for(int i = 0; i < objects.Count; i++){
            if(File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i))){
                File.Delete(Application.persistentDataPath + string.Format("/{0}.dat", i));
            }
        }
    }
    
    public void SaveScriptables(){
        //loop through all the scriptables and save to a Json file
        for(int i = 0; i < objects.Count; i++){
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.dat", i));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(objects[i]);
            binary.Serialize(file, json);
            file.Close();
        }
    }

    public void LoadScriptables(){
        //loops through the json file and set all the scriptables to the values
        for(int i = 0; i < objects.Count; i++){
            if(File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i))){
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.dat", i), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), objects[i]);
                file.Close();
            }
        }
    }
}
