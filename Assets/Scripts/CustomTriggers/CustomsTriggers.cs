using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractable
{
    public void Interact();
}

public abstract class CustomsTriggers : MonoBehaviour, IInteractable
{
    protected float weight;
    protected Rigidbody rb;

    [HideInInspector] public PlayerInteractorDistance playerInteractorDistance;


    public virtual void Awake()
    {
        PlayerInteractor player = PlayerInteractor.playerInteractorInstance;

        playerInteractorDistance = player.gameObject.GetComponent<PlayerInteractorDistance>();

        rb = GetComponent<Rigidbody>();

        if (rb == null) return;

        weight = rb.mass;
    }

    public virtual void Interact()
    {
        //Debug.Log(onInteractText);
        return;
    }

    public virtual void TextInfo()
    {
        // Debug.Log(interactText);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TextInfo();
        }
    }

    /// <summary>
    /// Called when is visible by a camera
    /// Override if we don't want them to be in the interaction
    /// </summary>
    public virtual void OnBecameVisible()
    {
        if (playerInteractorDistance != null)
            playerInteractorDistance.AddList(this); 
    }

    /// <summary>
    /// Called when is invisble  by a camera
    /// Override if we don't want them to be in the interaction
    /// </summary>
    public virtual void OnBecameInvisible()
    {
        if (playerInteractorDistance != null)
            playerInteractorDistance.RemoveList(this);
    }
}