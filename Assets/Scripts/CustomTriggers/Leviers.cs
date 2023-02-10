using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Leviers : CustomsTriggers
{
    public UnityEvent leverEvent;
    private bool doorOpen;
    [SerializeField] private Animation theDoorAnim;
    [SerializeField] private GameObject doorToDestroy;
    private Animator animLever;


    private void Start()
    {
        animLever = GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (!doorOpen) leverEvent.Invoke();

        return;
    }

    public void OpenDoor()
    {
        // On ouvre la porte qui correspond
        doorOpen = true;

        if (theDoorAnim != null)
        {
            theDoorAnim.Play();

            animLever.SetBool("leverOn", true);
        }
        else { Destroy(doorToDestroy); }
    }
}