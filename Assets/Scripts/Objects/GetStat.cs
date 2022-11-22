using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GetStat : Interactable
{
    public UnityEvent statEvent;
    private bool statState;
    public Animation statAnim;


    public override bool Interact()
    {
        //Debug.Log(onInteractText);

        if (!statState) statEvent.Invoke();

        return true;
    }

    public void AnimSuperForce()
    {
        // On joue l'animation de récupération de la super force
        statState = true;
        //statAnim.Play();
    }

    public override void GoToHand(GameObject hands, PlayerInput playerInput)
    {

    }
}