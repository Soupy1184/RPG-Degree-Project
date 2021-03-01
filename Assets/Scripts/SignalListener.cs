using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public SignalSender signal;
    public UnityEvent signalEvent;

    // called when signal is raised
    public void OnSignalRaised(){
        signalEvent.Invoke();
    }

    //go to signal and register
    private void OnEnable(){
        signal.RegisterListener(this);
    }

    private void OnDisable(){
        signal.DeRegisterListener(this);
    }
}
