using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractorDistance : PlayerInteractor
{
    public List<CustomsTriggers> objectsOnCamera = new List<CustomsTriggers>();
    private GameObject[] gameObjects = new GameObject[5];

    //private float securityDelay = 0.01f;
    //private float securityDelayTimer = 0;


    public override void Update()
    {
        base.Update();
    }

    public override void Detector()
    {
        interactable = NearestCollider(objectsOnCamera);

        if (interactable != null) //Sécurité au cas ou
        {
            Debug.Log("Here + " + interactable);
            interactable.Interact();
        }
        playerInput.CanInteract = false;
        interactable = null;
    }

    private IInteractable NearestCollider(List<CustomsTriggers> customsTriggers)
    {
        if (customsTriggers.Count == 0) return null;

        float nearestInteractable = 9999;
        CustomsTriggers nearestCol = null;

        foreach (CustomsTriggers trigger in customsTriggers)
        {
            if (trigger == null) continue;

            float currentDistance = Vector3.Distance(trigger.transform.position, transform.position);

            if (currentDistance <= nearestInteractable)
            {
                nearestInteractable = currentDistance;
                nearestCol = trigger;
            }
        }
        if (nearestCol.GetComponent(typeof(IInteractable)) != null)
        {
            return nearestCol.GetComponent<IInteractable>();
        }
        else return null;
    }

    /// <summary>
    /// Add a trigger on the list of the can interact with players
    /// </summary>
    /// <param name="objectsOnCamera.Add(triggers"> the trigger to add </param>
    public virtual void AddList(CustomsTriggers triggers)
    {
        if (!objectsOnCamera.Contains(triggers)) objectsOnCamera.Add(triggers);
    }

    /// <summary>
    /// Remove  a trigger on the list of the can interact with players
    /// </summary>
    /// <param name="objectsOnCamera.Add(triggers"> the trigger to remove </param>
    public virtual void RemoveList(CustomsTriggers triggers)
    {
        if (objectsOnCamera.Contains(triggers)) objectsOnCamera.Remove(triggers);
    }
}
