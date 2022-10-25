using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Interactable
{
    public GameObject cube;
    public Transform spawnPos;


    public override bool Interact()
    {
        //Debug.Log("Comment puis-je vous aider ?");
        //return true;
        base.Interact();
        Instantiate(cube, spawnPos.position, spawnPos.rotation);

        return true;
    }

    public override void TextInfo()
    {
        base.TextInfo();
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        //Action sur UI
    }
}