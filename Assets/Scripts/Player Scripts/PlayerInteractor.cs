using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInteractor : MonoBehaviour
{
    public static PlayerInteractor playerInteractorInstance;

    public Transform interactionPoint;
    public float radius;
    public LayerMask interactableLayer;
    public int interactableCount;

    [Header("Composant")]
    private Collider[] colliders = new Collider[5];
    public IInteractable interactable;

    [HideInInspector] public PlayerInput playerInput;
    public GameObject hands;

    private void Awake()
    {
        playerInteractorInstance = this;
        
        playerInput = this.GetComponent<PlayerInput>();
    }

    public virtual void Update()
    {
        if (playerInput.CanInteract)
        {
            Detector();
        }
    }

    /// <summary>
    /// Détecte la présence d'interactable autour du joueurs
    /// </summary>
    public virtual void Detector()
    {
        interactableCount = Physics.OverlapSphereNonAlloc(interactionPoint.position, radius, colliders, interactableLayer);

        // On Interagis
        if (interactableCount > 0)
        {
            interactable = NearestCollider(colliders);

            if (interactable != null) //Sécurité au cas ou
            {
                interactable.Interact();
            }
        }

        playerInput.CanInteract = false;
        interactable = null;
    }

    /// <summary>
    /// Récupére Le collider le plus proche du joueurs selon un radius et un layers spécifique
    /// </summary>
    /// <param name="cols"></param>
    /// <returns></returns>
    private IInteractable NearestCollider(Collider[] cols)
    {
        float nearestInteractable = 9999;
        Collider nearestCol = null;

        foreach (Collider col in cols)
        {
            if (col == null) continue;

            float currentDistance = Vector3.Distance(col.transform.position, transform.position);
            if (currentDistance <= nearestInteractable)
            {
                nearestInteractable = currentDistance;
                nearestCol = col;
            }
        }
        if (nearestCol.GetComponent(typeof(IInteractable)) != null)
        {
            return nearestCol.GetComponent<IInteractable>();
        }
        else return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(interactionPoint.position, radius);
    }
}