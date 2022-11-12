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
    private Vector3 climbPos = Vector3.zero;

    [Header("Climb Raycast Settings")]
    public RaycastCheck[] raycastsClimb;
    public int raycastGood;

    [Header("Player Component")]
    private PlayerMovement playerMovement;


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
        playerMovement = GetComponent<PlayerMovement>();

        foreach (RaycastCheck raycastCheck in raycastsClimb)
        {
            raycastCheck.directionRaycast = transform.forward;
        }
    }

    private void Update()
    {
        if (!playerMovement.isGrounded()) ClimbChecker();

        if (isClimbing)
        {
            transform.position = climbPos;
        }
    }


    /// <summary>
    /// Détermine si je peux ou non grimper
    /// </summary>
    public void ClimbChecker()
    {
        raycastGood = 0;
        foreach (RaycastCheck raycast in raycastsClimb)
        {
            // On repositionne le raycast lorsque le joueur se déplace
            raycast.directionRaycast = transform.forward;

            if (raycast.RaycastTest()) raycastGood++;
        }

        Climb();
    }


    /// <summary>
    /// On grimpe la structure
    /// </summary>
    public void Climb()
    {
        if (raycastsClimb[0].RaycastTest() && !raycastsClimb[1].RaycastTest() && !raycastsClimb[2].RaycastTest()) // Si mon raycast d'enjambement est bon ET que les autres sont faux
        {
            isClimbing = true;


            // On réalise l'animation
            playerMovement.animator.applyRootMotion = true;
            playerMovement.animator.SetTrigger("TrFastJumpAnim");


            // On empêche l'autre anim de pouvoir se lancer
            playerMovement.animator.SetBool("FastJumpCheck", true); 


            StartCoroutine(ClimbEndFastJump());


            Debug.Log("J'enjambe !");

            // On déplace le personnage à l'endroit où il doit se trouver après avoir grimpé si besoin
        }
        else if (raycastsClimb[1].RaycastTest() && !raycastsClimb[2].RaycastTest()) // Si mon raycast de grimpHit est bon et que celui du dessus ne l'est pas
        {
            isClimbing = true;


            // On réalise l'animation
            playerMovement.animator.applyRootMotion = true;
            playerMovement.animator.SetTrigger("TrClimbAnim");


            // On empêche l'autre anim de pouvoir se lancer
            playerMovement.animator.SetBool("ClimbCheck", true);


            StartCoroutine(ClimbEnd());


            Debug.Log("je grimpe !");

            // On déplace le personnage à l'endroit où il doit se trouver après avoir grimpé si besoin
        }
        else 
        {
            Debug.Log("impossible de grimper");
            //isClimbing = false;
        }
    }

    private IEnumerator ClimbEndFastJump()
    {
        yield return new WaitForSeconds(1.05f);

        isClimbing = false;

        // Reset des booléens
        playerMovement.animator.applyRootMotion = false;


        playerMovement.animator.ResetTrigger("TrFastJumpAnim");


        playerMovement.animator.SetBool("FastJumpCheck", false);
    }

    private IEnumerator ClimbEnd()
    {
        yield return new WaitForSeconds(1.2f);

        isClimbing = false;

        // Reset des booléens
        playerMovement.animator.applyRootMotion = false;


        playerMovement.animator.ResetTrigger("TrClimbAnim");


        playerMovement.animator.SetBool("ClimbCheck", false);
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