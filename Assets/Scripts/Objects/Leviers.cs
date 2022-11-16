using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Leviers : Interactable
{
    public UnityEvent leverEvent;
    private bool doorOpen;
    public Animation theDoorAnim;


    public override bool Interact()
    {
        //Debug.Log("Levier");

        if (!doorOpen) leverEvent.Invoke();

        return true;
    }

    public void OpenDoor()
    {
        // On ouvre la porte qui correspond
        doorOpen = true;
        theDoorAnim.Play();
    }

    public override void GoToHand(GameObject hands, PlayerInput playerInput)
    {

    }
}