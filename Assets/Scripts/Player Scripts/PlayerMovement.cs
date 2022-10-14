using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    public float speed = 3f;
    private Vector2 moveInput;
    private Vector3 playerVelocity;


    [Header("Player Jump")]
    public float jumpForce = 2f;
    private bool canJump;
    public float airDrag = .5f;
    private Vector3 playerJump;


    [Header("Player Ground")]
    public RaycastGrounded[] raycastsGrounds;
    public LayerMask layersGround;
    public float rangeMaxGrounded;


    [Header("Crouch")]
    public float crouchSpeed = .3f;
    public float standHeight = 2.0f;
    public float crouchHeight = 1.0f;
    private bool crouching;


    [Header("Player Component")]
    private Rigidbody playerRb;
    public Transform playerMesh;
    public CapsuleCollider capsuleCollider;



    private void Awake()
    {
        playerRb = GetComponentInChildren<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        foreach (RaycastGrounded raycast in raycastsGrounds)
        {
            raycast.layer = layersGround;
            raycast.rangeMaxGrounded = rangeMaxGrounded;
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();

        if (isGrounded() && canJump)
        {
            Jump();
        }

        Crounching();
    }

    private void Update()
    {

    }

    #region PlayerMove

    /// <summary>
    /// Gère le player movement
    /// </summary>
    private void PlayerMove()
    {
        // Rotation du joueur pour qu'il regarde dans la direction où il marche
        Vector3 playerRotation = new Vector3(moveInput.x, 0, moveInput.y);

        if (playerRotation != Vector3.zero)
            playerRb.gameObject.transform.rotation = Quaternion.Slerp(playerRb.gameObject.transform.rotation, Quaternion.LookRotation(playerRotation), 0.15f);

        // Déplacement du joueur
        playerVelocity = new Vector3(moveInput.x * speed, playerRb.velocity.y, moveInput.y * speed);
        playerRb.velocity = playerVelocity;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void OnMovement(InputValue value)
    {
        // On récupère la valeur du mouvement qu'on stock dans un Vector2
        moveInput = value.Get<Vector2>();
    }

    #endregion

    #region PlayerJump

    /// <summary>
    /// Imput Du jump
    /// </summary>
    public void OnJump()
    {
        //Récupèration de l'input
        canJump = true;
    }

    /// <summary>
    /// activation du jump
    /// </summary>
    private void Jump()
    {
        playerJump = new Vector3(0, jumpForce, 0);
        playerRb.velocity = playerJump;
        canJump = false;
        playerJump = Vector3.zero;
    }

    private bool isGrounded()
    {
        int a = 0;
        foreach (RaycastGrounded raycast in raycastsGrounds)
        {
            if (raycast.RaycastTest()) a++;
        }

        // Modifier en a =< 1 ?
        if (a == raycastsGrounds.Length)
        { 
            return true; 
        }
        else
        {
            // On empêche le joueur de resauter instantanément au contact du sol ?
            canJump = false;
            return false;
        }
    }

    #endregion


    #region PlayerCrouched

    public void OnCrouch()
    {
        if (crouching == true) crouching = false;
        else crouching = true;
    }

    private void Crounching()
    {
        float desiredHeight = crouching ? crouchHeight : standHeight;

        if (capsuleCollider.height != desiredHeight)
        {
            AdjustHeight(desiredHeight);
        }
    }

    private void AdjustHeight(float height)
    {
        float center = height / 2;


        capsuleCollider.height = Mathf.Lerp(capsuleCollider.height, height, crouchSpeed);
        capsuleCollider.center = Vector3.Lerp(capsuleCollider.center, new Vector3(0, center, 0), crouchSpeed);

        playerMesh.localScale = new Vector3(playerMesh.localScale.x, Mathf.Lerp(playerMesh.localScale.y, height / 2, crouchSpeed), playerMesh.localScale.z);

        playerMesh.localPosition = Vector3.Lerp(capsuleCollider.center, new Vector3(playerMesh.localPosition.x, center / 2, playerMesh.localPosition.z), crouchSpeed / 2);
    }

    #endregion
}