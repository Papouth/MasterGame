using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    [Header("Climb Settings")]
    public bool isClimbing; // Si le joueur grimpe
    private Vector3 climbPos = Vector3.zero;

    [Header("Climb Raycast Settings")]
    public RaycastCheck[] raycastsClimb;
    public int raycastGood;

    [Header("Player Component")]
    private PlayerMovement playerMovement;
    private PlayerInteractor playerInteractor;


    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerInteractor = GetComponent<PlayerInteractor>();

        foreach (RaycastCheck raycastCheck in raycastsClimb)
        {
            raycastCheck.directionRaycast = transform.forward;
        }
    }

    private void Update()
    {
        if (!playerMovement.isGrounded() && playerInteractor.hands.transform.childCount == 0) ClimbChecker();

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


            //Debug.Log("J'enjambe !");

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


            //Debug.Log("je grimpe !");

            // On déplace le personnage à l'endroit où il doit se trouver après avoir grimpé si besoin
        }
        else 
        {
            //Debug.Log("impossible de grimper");
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