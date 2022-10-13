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
    private Vector3 _playerJump;

    [Header("Player Ground")]
    public RaycastGrounded[] raycastsGrounds;
    public LayerMask layersGround;
    public float rangeMaxGrounded;

    [Header("Crouch")]
    public float crouchSpeed = .3f;
    public float standHeight = 2.0f;
    public float crouchHeight = 1.0f;

    private bool crouching;
    private bool canCrouch;


    [Header("Player Component")]
    private Rigidbody playerRb;

    public Transform playerMesh;
    public CapsuleCollider collider;


    private void Awake()
    {
        //playerRb = GetComponent<Rigidbody>();
        playerRb = GetComponentInChildren<Rigidbody>();

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
        //Recuperation de l'input
        canJump = true;

    }

    /// <summary>
    /// activation du jump
    /// </summary>
    private void Jump()
    {
        _playerJump = new Vector3(0, jumpForce, 0);
        playerRb.velocity = _playerJump;
        canJump = false;
        _playerJump = Vector3.zero;

    }



    private bool isGrounded()
    {
        int a = 0;
        foreach (RaycastGrounded raycast in raycastsGrounds)
        {
            if (raycast.RaycastTest()) a++;
        }

        if (a == raycastsGrounds.Length) return true;
        else return false;

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
    
        if(collider.height != desiredHeight)
        {
            AdjustHeight(desiredHeight);

        }
    }

    private void AdjustHeight(float height)
    {
        float center = height / 2;
        

        collider.height = Mathf.Lerp(collider.height, height
            , crouchSpeed);
        collider.center = Vector3.Lerp(collider.center, new Vector3(0, center, 0), crouchSpeed);

        playerMesh.localScale = new Vector3(
            playerMesh.localScale.x,
            Mathf.Lerp(playerMesh.localScale.y, height / 2, crouchSpeed),
            playerMesh.localScale.z);


        playerMesh.localPosition = Vector3.Lerp(
            collider.center,
            new Vector3(
                playerMesh.localPosition.x,
                center/2,
                playerMesh.localPosition.z),
            crouchSpeed /2);

    }

    #endregion
}