using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Variables")]

    public float speed = 3f;
    private Rigidbody playerRb;
    private Vector2 moveInput;


    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        Vector3 playerVelocity = new Vector3(moveInput.x * speed, playerRb.velocity.y, moveInput.y * speed);
        playerRb.velocity = transform.TransformDirection(playerVelocity);
    }

    public void OnMovement(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}