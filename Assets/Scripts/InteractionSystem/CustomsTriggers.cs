using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractable
{
    public bool Interact(PlayerInteractor playerInteractor);
}

public abstract class CustomsTriggers : MonoBehaviour, IInteractable
{
    [Header("Interactable Info")]
    [SerializeField] protected string interactText;
    [SerializeField] protected string onInteractText;

    [SerializeField] protected float weight;
    [SerializeField] protected Rigidbody rb;



    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null) return;

        weight = rb.mass;
    }

    public virtual bool Interact(PlayerInteractor playerInteractor)
    {
        //Debug.Log(onInteractText);
        return true;
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
}