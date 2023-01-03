using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : CustomsTriggers
{
    public bool isDestroyable;

    [Header("Player Component")]
    private PlayerInput playerInput;
    private PlayerStats playerStats;
    private Animator anim;
    private bool playerCheck;

    public override void Awake()
    {
        if (gameObject.CompareTag("Destroyable")) isDestroyable = true;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BreakObject();
        }
    }

    /// <summary
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInput = other.GetComponent<PlayerInput>();
            playerStats = other.GetComponent<PlayerStats>();
            anim = other.GetComponent<Animator>();
        }
    }

    #region Destroy Objects
    public bool BreakObject()
    {
        if (playerInput.CanDestroy && playerStats.haveSuperForce && isDestroyable && !playerInput.CanTelekinesy)
        {
            Debug.Log("here");

            // Animation du joueur
            anim.SetTrigger("TrDestroy");

            Invoke("BreakThis", 1.2f);

            playerInput.CanDestroy = false;

            return true;
        }

        return false;
    }

    public void BreakThis()
    {
        Destroy(gameObject);
    }
    #endregion
}