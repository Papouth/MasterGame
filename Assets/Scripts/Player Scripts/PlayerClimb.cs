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
    public bool isClimbing; // Si le joueur grimpe
    public bool canGrip; // Si le joueur peut s'accrocher
    public int raycastGood;

    public RaycastCheck[] raycastsClimb;

    [Header("Climb Anim")]
    public bool jumpAnim;
    public bool climbAnim;
    public bool gripAnim;



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
        //Debug.Log("je demande de grimper");

        if (raycastsClimb[0].RaycastTest() && !raycastsClimb[1].RaycastTest()) // Si mon premier raycast est bon, mais aussi que le second est faux alors on peut enjamber
        {
            isClimbing = true;

            // On réalise l'animation
            jumpAnim = true;


            // Je peux enjamber
            Debug.Log("J'enjambe !");

            // On déplace le personnage à l'endroit où il doit se trouver après avoir grimpé
        }
        else if (raycastsClimb[0].RaycastTest() && raycastsClimb[1].RaycastTest() && !raycastsClimb[2].RaycastTest())
        {
            isClimbing = true;

            // On réalise l'animation
            climbAnim = true;

            // Je peux grimper
            Debug.Log("je grimpe !");

            // On déplace le personnage à l'endroit où il doit se trouver après avoir grimpé
        }
        else if (raycastsClimb[0].RaycastTest() && raycastsClimb[1].RaycastTest() && raycastsClimb[2].RaycastTest() && !raycastsClimb[3].RaycastTest())
        {
            isClimbing = true;

            // On réalise l'animation
            gripAnim = true;

            // Je m'accroche avant de grimper
            Debug.Log("Je m'accroche avant de grimper !");


            // On déplace le personnage à l'endroit où il doit se trouver après avoir grimpé
        }
        else if (raycastsClimb[3].RaycastTest()) // Du moment que le dernier touche, c'est impossible pour le joueur de franchir l'obstacle
        {
            isClimbing = false;

            // Reset des booléens
            jumpAnim = false;
            climbAnim = false;
            gripAnim = false;

            // Trop haut, donc il ne se passe rien
            Debug.Log("C'est trop haut pour moi !");
        }
        else
        {
            // Reset des booléens
            jumpAnim = false;
            climbAnim = false;
            gripAnim = false;


            Debug.Log("impossible de grimper");
            isClimbing = false;
        }
    }

    /*
    private void OnDrawGizmos()
    {
        foreach (RaycastCheck raycast in raycastsClimb)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(raycast.transform.position, raycast.transform.position + transform.forward);
        }
    }
    */
}