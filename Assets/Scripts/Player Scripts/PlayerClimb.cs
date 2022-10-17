using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    public LayerMask layerCanClimb;
    public float rangeMaxClimb = 1f;

    public float forceClimb = 1.5f;

    public RaycastCheck[] raycastClimbsUp;
    public RaycastCheck[] raycastClimbsDown;


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
            // On repositionne le raycast lorsque le joueur se déplace
            raycast.directionRaycast = transform.forward;
            if (raycast.RaycastTest()) raycastUpGood++;
        }

        foreach (RaycastCheck raycast in raycastClimbsDown)
        {
            // On repositionne le raycast lorsque le joueur se déplace
            raycast.directionRaycast = transform.forward;
            if (raycast.RaycastTest()) raycastDownGood++;
        }


        if (raycastDownGood > 0) //Si en bas ca touche le mur
        {
            if (raycastUpGood > 0) //La je touche le mur en haut
                return false;
            else    //La je touche pas le mur en haut
            {
                Debug.Log("Good here");
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
        Rigidbody rb = GetComponent<Rigidbody>();

        Vector3 forceToAdd = (Vector3.forward + Vector3.up) * forceClimb;

        rb.AddForce(forceToAdd, ForceMode.Impulse);
        Debug.Log("Here lets go");
    }
}