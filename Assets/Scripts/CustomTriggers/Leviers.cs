using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Leviers : CustomsTriggers
{
    public UnityEvent leverEvent;
    private bool doorOpen;
    public Animation theDoorAnim;


    public override void Interact()
    {
        //Debug.Log("Levier");

        if (!doorOpen) leverEvent.Invoke();

        return;
    }

    public void OpenDoor()
    {
        // On ouvre la porte qui correspond
        doorOpen = true;
        theDoorAnim.Play();
    }

}