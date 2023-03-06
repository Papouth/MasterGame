using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Leviers : CustomsTriggers
{
    public UnityEvent leverEvent;
    private bool boolCheck;

    [Header("Si c'est une porte à ouvrir")] 
    [SerializeField] private Animation theDoorAnim;
    [SerializeField] private GameObject doorToDestroy;

    [Header("Si c'est une plateforme à activer")]
    [Tooltip("Le script de la plateforme que l'on souhaite activer")]
    [SerializeField] private Plateforme plateformeScript;


    private Animator animLever;


    private void Start()
    {
        //animLever = GetComponent<Animator>();
        //if (plateformeScript != null) plateformeScript = GetComponent<Plateforme>();
    }

    public override void Interact()
    {
        if (!boolCheck) leverEvent.Invoke();

        return;
    }

    public void OpenDoor()
    {
        // On ouvre la porte qui correspond
        boolCheck = true;

        if (theDoorAnim != null)
        {
            theDoorAnim.Play();

            animLever.SetBool("leverOn", true);
        }
        else { Destroy(doorToDestroy); }
    }

    public void ActivatePlatform()
    {
        Debug.Log("bool platform");
        // On active la plateforme qui correspond
        boolCheck = true;

        plateformeScript.isEnable = true;
    }
}