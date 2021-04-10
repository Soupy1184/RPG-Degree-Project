using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Text value;
    void Start()
    {
        value.text = PersistentManagerScript.Instance.value.ToString();
    }

    public void GoToFirstScene(){
        SceneManager.LoadScene("first");
    }

    public void GoToSecondScene(){
        SceneManager.LoadScene("second");
    }
}
