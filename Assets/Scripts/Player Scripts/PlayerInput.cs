using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Vector2 moveInput;
    private bool canJump;
    private bool crouching;
    private bool canInteract;
    private bool canChangeTempo;
    private bool canDestroy;

    public Vector2 MoveInput => moveInput;
    public bool CanJump
    {
        get { return canJump; }
        set { canJump = value; }
    }
    public bool Crouching => crouching;
    public bool CanInteract
    {
        get { return canInteract; }
        set { canInteract = value; }
    }

    public bool ChangeTempo
    {
        get { return canChangeTempo; }
        set { canChangeTempo = value; }
    }

    public bool CanDestroy
    {
        get { return canDestroy; }
        set { canDestroy = value; }
    }
 

    public void OnMovement(InputValue value)
    {
        // On r�cup�re la valeur du mouvement qu'on stock dans un Vector2
        moveInput = value.Get<Vector2>();
    }

    public void OnJump()
    {
        //R�cup�ration de l'input
        canJump = true;
    }

    public void OnCrouch()
    {
        if (crouching == true) crouching = false;
        else crouching = true;
    }

    public void OnInteract()
    {
        canInteract = true;
    }

    public void OnTempo()
    {
        canChangeTempo = true;
    }

    public void OnBreak()
    {
        canDestroy = true;
        Invoke("BreakTimer", 0.1f);
    }

    private void BreakTimer()
    {
        canDestroy = false;
    }
}