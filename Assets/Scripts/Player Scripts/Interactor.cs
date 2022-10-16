using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interactor : MonoBehaviour
{
    public Transform interactionPoint;
    public float radius;
    public LayerMask interactableLayer;
    public int interactableCount;

    private Collider[] colliders = new Collider[5];
    private Interactable interactable;
    private bool canInteract;


    private void Update()
    {
        Detector();
    }

    public void Detector()
    {
        interactableCount = Physics.OverlapSphereNonAlloc(interactionPoint.position, radius, colliders, interactableLayer);


        // On Interagis
        if(interactableCount > 0)
        {
            interactable = colliders[0].GetComponent<Interactable>();

            interactable.TextInfo();

            if(interactable != null && canInteract)
            {
                interactable.Interact(this);
            }
        }
        else if (interactableCount == 0)
        {
            canInteract = false;
        }
    }

    public void OnInteract()
    {
        canInteract = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(interactionPoint.position, radius);
    }
}