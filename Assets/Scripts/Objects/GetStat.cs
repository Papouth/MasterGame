using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GetStat : Interactable
{
    public UnityEvent statEvent;
    private bool statState;
    public Animation statAnim;


    public override bool Interact(PlayerInteractor playerInteractor)
    {
        //Debug.Log(onInteractText);

        if (!statState) statEvent.Invoke();

        return true;
    }

    public void AnimSuperForce()
    {
        // On joue l'animation de r�cup�ration de la super force
        statState = true;
        //statAnim.Play();
    }

}