using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : CustomsTriggers
{
    #region Variables
    //public GameObject cube;
    //public Transform spawnPos;
    //public GameObject topCube;

    public Dialogue dialogues;
    private DialogueManager manager;
    private Transform player;
    #endregion


    #region Built In Methods
    public override void Awake()
    {
        if(DialogueManager.InstanceDialogue)
        manager = DialogueManager.InstanceDialogue;
        else Debug.LogError("Pas de dialogue manager ?");
        
        //topCube.SetActive(false);

        if (PlayerInteractor.playerInteractorInstance.GetComponent<PlayerInteractorDistance>() != null)
            playerInteractorDistance = PlayerInteractor.playerInteractorInstance.GetComponent<PlayerInteractorDistance>();

    }

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }
    #endregion

    private void LookAtPlayer()
    {
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    public override void Interact()
    {
        Debug.Log("Comment puis-je vous aider ?");
        base.Interact();
        //Instantiate(cube, spawnPos.position, spawnPos.rotation);

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
            // Peut être remplacer par un point d'interrogation par exemple
            //topCube.SetActive(true);

            LookAtPlayer();
            manager.StartDialogue(dialogues);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) LookAtPlayer();
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Peut être remplacer par un point d'interrogation par exemple
            //topCube.SetActive(false);

            manager.EndDialogue();
        }
    }
}