//for test/development purposes, not implemented in game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    void Start()
    {
        // Switch to 800 x 600 windowed
        Screen.SetResolution(800, 600, false);
    }
}

