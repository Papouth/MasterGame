using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Interactable
{
    public GameObject cube;
    public Transform spawnPos;
    public GameObject topCube;

    public void Awake()
    {
        topCube.SetActive(false);
    }

    public override bool Interact()
    {
        //Debug.Log("Comment puis-je vous aider ?");
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
        if (other.CompareTag("Player"))
            topCube.SetActive(true);

        //Action sur UI
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            topCube.SetActive(false);
    }

    public override void GoToHand(GameObject hands, PlayerInput playerInput)
    {

    }
}