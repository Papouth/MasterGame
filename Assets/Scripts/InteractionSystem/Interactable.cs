using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected string interactText;


    public virtual bool Interact()
    {
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
        
    }
}