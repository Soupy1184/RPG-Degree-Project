/*
    written by: Afieqha Mieza
    date: March 2021
    signaling concept resource: https://www.youtube.com/watch?v=BLfNP4Sc_iA
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class SignalSender : ScriptableObject
{
    //list of signal listeners
    public List<SignalListener> listeners = new List<SignalListener>();

    // to raise the signals
    // loop through all signal listenerrs that we have
    public void Raise(){
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnSignalRaised();
        }
    }

    public void RegisterListener(SignalListener listener){
        listeners.Add(listener);
    }

    public void DeRegisterListener(SignalListener listener){
        listeners.Remove(listener);
    }
}


