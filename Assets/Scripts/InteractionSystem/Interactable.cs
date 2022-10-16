using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected string interactText;


    public virtual bool Interact(Interactor interactor)
    {
        return true;
    }

    public virtual void TextInfo()
    {
        Debug.Log(interactText);
    }
}