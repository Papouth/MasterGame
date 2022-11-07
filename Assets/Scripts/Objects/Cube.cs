using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Interactable
{
    public override void Awake()
    {
        base.Awake();
    }

    public override bool Interact()
    {
        //Debug.Log("cube");

        return true;
    }

    public override void GoToHand(GameObject hands, PlayerInput playerInput)
    {
        base.GoToHand(hands, playerInput);
    }
}