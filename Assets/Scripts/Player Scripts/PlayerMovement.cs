using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]

    public float speed = 3f;
    private Rigidbody playerRb;
    private Vector2 moveInput;
    public float jumpForce = 2f;


    private void Awake()
    {
        //playerRb = GetComponent<Rigidbody>();
        playerRb = GetComponentInChildren<Rigidbody>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        // Rotation du joueur pour qu'il regarde dans la direction où il marche
        Vector3 playerRotation = new Vector3(moveInput.x, 0, moveInput.y);
        playerRb.gameObject.transform.rotation = Quaternion.Slerp(playerRb.gameObject.transform.rotation, Quaternion.LookRotation(playerRotation), 0.15f);

        // Déplacement du joueur
        Vector3 playerVelocity = new Vector3(moveInput.x * speed, playerRb.velocity.y, moveInput.y * speed);
        playerRb.velocity = transform.TransformDirection(playerVelocity);
    }

    public void OnMovement(InputValue value)
    {
        // On récupère la valeur du mouvement qu'on stock dans un Vector2
        moveInput = value.Get<Vector2>();
    }

    public void OnJump()
    {
        // Quand on reçoit l'info de jump, on saute
        Vector3 playerJump = new Vector3(moveInput.x * speed, jumpForce * 5, moveInput.y * speed);
        playerRb.velocity = transform.TransformDirection(playerJump);
    }
}