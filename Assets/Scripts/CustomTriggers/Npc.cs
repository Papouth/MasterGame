using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : CustomsTriggers
{
    public GameObject cube;
    public Transform spawnPos;
    public GameObject topCube;

    public Dialogue dialogues;
    private DialogueManager manager;

    public override void Awake()
    {
        if(DialogueManager.InstanceDialogue)
        manager = DialogueManager.InstanceDialogue;
        else Debug.LogError("Pas de dialogue manager ?");
        
        topCube.SetActive(false);
    }

    public override void Interact()
    {
        Debug.Log("Comment puis-je vous aider ?");
        base.Interact();
        Instantiate(cube, spawnPos.position, spawnPos.rotation);

        return;
    }

    public override void TextInfo()
    {
        //base.TextInfo();
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
        {
            topCube.SetActive(true);

            manager.StartDialogue(dialogues);
        }

        //Action sur UI
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            topCube.SetActive(false);

            manager.EndDialogue();
        }
    }
}