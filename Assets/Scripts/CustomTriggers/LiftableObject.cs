using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftableObject : CustomsTriggers
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Interact()
    {
        //Debug.Log("cube");
        //GoToHand(playerInteractor.hands, playerInteractor.playerInput);
        return;
    }
    
    /*
    public void GoToHand(GameObject hands, PlayerInput playerInput)
    {
        Debug.Log("gotohand");

        if (playerInput.CanInteract && hands.transform.childCount == 0)
        {

            gameObject.transform.SetParent(hands.transform, false);
            gameObject.transform.position = hands.transform.position;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;

        }
        else if (playerInput.CanInteract && hands.transform.childCount > 0)
        {
            gameObject.transform.SetParent(null);
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    */
}