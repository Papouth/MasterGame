using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    /*
    public LayerMask layerCanClimb;
    public float rangeMaxClimb = 1f;

    public float forceClimb = 1.5f;

    [Header("Component")]
    public RaycastCheck[] raycastClimbsUp;
    public RaycastCheck[] raycastClimbsDown;
    private PlayerMovement playerMovement;
    */


    [Header("Climb Settings")]
    public bool canClimb; // Si le joueur peut grimper
    public bool askToClimb; // Le joueur appuie sur la touche pour grimper
    public bool canGrip; // Si le joueur peut s'accrocher
    private bool climbing; // Le joueur est en train de grimper
    public int raycastGood;

    public RaycastCheck[] raycastsClimb;



    #region OLD SYSTEM
    /*
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        foreach (RaycastCheck raycastCheck in raycastClimbsUp)
        {
            raycastCheck.layer = layerCanClimb;
            raycastCheck.rangeMax = rangeMaxClimb;
            raycastCheck.directionRaycast = transform.forward;
        }
        foreach (RaycastCheck raycastCheck in raycastClimbsDown)
        {
            raycastCheck.layer = layerCanClimb;
            raycastCheck.rangeMax = rangeMaxClimb;
            raycastCheck.directionRaycast = transform.forward;
        }

        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (CheckCanClimb())
        {
            Climb();
        }
    }


    /// <summary>
    /// Check si le joueur peut climb une surface
    /// </summary>
    /// <returns></returns>
    private bool CheckCanClimb()
    {
        //Check raycast

        int raycastUpGood = 0;
        int raycastDownGood = 0;

        foreach (RaycastCheck raycast in raycastClimbsUp)
        {
            // On repositionne le raycast lorsque le joueur se d�place
            raycast.directionRaycast = transform.forward;
            if (raycast.RaycastTest()) raycastUpGood++;
        }

        foreach (RaycastCheck raycast in raycastClimbsDown)
        {
            // On repositionne le raycast lorsque le joueur se d�place
            raycast.directionRaycast = transform.forward;
            if (raycast.RaycastTest()) raycastDownGood++;
        }


        if (raycastDownGood > 0) //Si en bas ca touche le mur
        {
            if (raycastUpGood > 0) //La je touche le mur en haut
                return false;
            else    //La je touche pas le mur en haut
            {
                //Debug.Log("Good here");
                return true;
            }
        }
        else //La je touche pas le mur en bas
            return false;

    }

    /// <summary>
    /// Le joueur se fait propulser vers le haut lorsqu'il tente de grimper un mur
    /// </summary>
    private void Climb()
    {
        if(playerMovement == null) return;

        Vector3 forceToAdd = (Vector3.forward + Vector3.up) * forceClimb;
    }
    */
    #endregion

    private void Awake()
    {
        askToClimb = true;
        //canClimb = true;
        foreach (RaycastCheck raycastCheck in raycastsClimb)
        {
            raycastCheck.directionRaycast = transform.forward;
        }
    }

    private void Update()
    {
        ClimbChecker();
    }

    /// <summary>
    /// Détermine si je peux ou non grimper
    /// </summary>
    public bool ClimbChecker()
    {
        // canClimb -> true / false

        raycastGood = 0;
        foreach (RaycastCheck raycast in raycastsClimb)
        {
            // On repositionne le raycast lorsque le joueur se déplace
            raycast.directionRaycast = transform.forward;
            if (raycast.RaycastTest()) raycastGood++;
        }

        if (raycastGood > 0) return true;
        else return false;
    }


    /// <summary>
    /// On grimpe la structure
    /// </summary>
    public void Climb()
    {
        if (raycastGood == 1)
        {
            canClimb = true;

            // Je peux enjamber
            Debug.Log("J'enjambe !");

            // On réalise l'animation
            // On déplace le personnage à l'endroit où il doit se trouver après avoir grimpé
        }
        else if (raycastGood == 2)
        {
            canClimb = true;

            // Je peux grimper
            Debug.Log("je grimpe !");

            // On réalise l'animation
            // On déplace le personnage à l'endroit où il doit se trouver après avoir grimpé
        }
        else if (raycastGood == 3)
        {
            canClimb = true;

            // Je m'accroche avant de grimper
            Debug.Log("Je m'accroche avant de grimper !");

            // On réalise l'animation
            // On déplace le personnage à l'endroit où il doit se trouver après avoir grimpé
        }
        else if (raycastGood == 4)
        {
            canClimb = false;

            // Trop haut, donc il ne se passe rien
            Debug.Log("C'est trop haut pour moi !");
        }
        else
        {
            Debug.Log("impossible de grimper");
            canClimb = false;
        }
    }


    private void OnDrawGizmos()
    {
        foreach (RaycastCheck raycast in raycastsClimb)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(raycast.transform.position, raycast.transform.position + transform.forward);
        }
    }
}