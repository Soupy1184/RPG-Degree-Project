/*
    written by: Afieqha Mieza
    date: January 2021
    signaling concept resource: https://www.youtube.com/watch?v=BLfNP4Sc_iA
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    // reference to a signal
    public SignalSender signal;
    public UnityEvent signalEvent;

    // invoke event when signal is raised
    public void OnSignalRaised(){
        signalEvent.Invoke();
    }

    // registering signal
    private void OnEnable(){
        signal.RegisterListener(this);
    }

    // disable signal
    private void OnDisable(){
        signal.DeRegisterListener(this);
    }
}
