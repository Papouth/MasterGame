using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
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
        Debug.Log(onInteractText);
        return true;
    }

    public virtual void TextInfo()
    {
        Debug.Log(interactText);
    }

    /// <summary
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TextInfo();
        }
    }
}