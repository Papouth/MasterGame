using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Interactable
{
    public override bool Interact()
    {
        Debug.Log("Comment puis-je vous aider ?");
        return true;
    }

    public override void TextInfo()
    {
        base.TextInfo();
    }

    public override void OnTriggerEnter(Collider other)
    {
        //Action sur UI
    }
}