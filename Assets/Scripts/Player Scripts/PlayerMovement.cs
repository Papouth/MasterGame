using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    #region Variables

    [Header("Player Movement")]
    public float speed = 3f;
    private Vector3 playerVelocity;


    [Header("Player Jump")]
    public float jumpForce = 2f;
    public float airDrag = .5f;
    private Vector3 playerJump;

    private float coyoteTime = 0;
    [SerializeField] private float coyoteTimer = .5f;


    [Header("Player Ground")]
    public RaycastCheck[] raycastsGrounds;
    public LayerMask layersGround;
    public float rangeMaxGrounded;


    [Header("Crouch")]
    public float crouchSpeed = .3f;
    public float standHeight = 2.0f;
    public float crouchHeight = 1.0f;

    [Header("Player Component")]
    private Rigidbody playerRb;
    public Transform playerMesh;
    public CapsuleCollider capsuleCollider;

    private PlayerInput playerInput;

    #endregion

    private void Awake()
    {
        playerRb = GetComponentInChildren<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        playerInput = GetComponent<PlayerInput>();

        foreach (RaycastCheck raycast in raycastsGrounds)
        {
            raycast.layer = layersGround;
            raycast.rangeMax = rangeMaxGrounded;
            raycast.directionRaycast = Vector3.down;
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
        Crouching();
    }

    private void Update()
    {
        if (isGrounded() && playerInput.CanJump)
        {
            Jump();
        }
    }

    #region PlayerMove

    /// <summary>
    /// G�re le player movement
    /// </summary>
    private void PlayerMove()
    {
        // Rotation du joueur pour qu'il regarde dans la direction o� il marche
        Vector3 playerRotation = new Vector3(playerInput.MoveInput.x, 0, playerInput.MoveInput.y);

        if (playerRotation != Vector3.zero)
            playerRb.gameObject.transform.rotation = Quaternion.Slerp(playerRb.gameObject.transform.rotation, Quaternion.LookRotation(playerRotation), 0.15f);

        // D�placement du joueur
        playerVelocity = new Vector3(playerInput.MoveInput.x * speed, playerRb.velocity.y, playerInput.MoveInput.y * speed);
        playerRb.velocity = playerVelocity;
    }



    #endregion

    #region PlayerJump



    /// <summary>
    /// activation du jump
    /// </summary>
    private void Jump()
    {
        playerJump = new Vector3(0, jumpForce, 0);
        playerRb.velocity = playerJump;
        playerInput.CanJump = false;
        playerJump = Vector3.zero;
        coyoteTime = coyoteTimer;
    }

    /// <summary>
    /// Check si le joueur est au sol
    /// </summary>
    /// <returns></returns>
    private bool isGrounded()
    {
        int raycastGood = 0;
        foreach (RaycastCheck raycast in raycastsGrounds)
        {
            if (raycast.RaycastTest()) raycastGood++;
        }


        if (raycastGood > 0) //La je touche le sol
        {
            coyoteTime = 0;
            return true;
        }
        else  //La je touche plus le sol
        {
            coyoteTime += Time.deltaTime;

            if (coyoteTime >= coyoteTimer) //Temps écoulé
            {
                // On emp�che le joueur de re-sauter instantan�ment au contact du sol
                playerInput.CanJump = false;
                return false;
            }
            else return true; //Encore le temps de sauté


        }
    }

    #endregion


    #region PlayerCrouched

    /// <summary>
    /// Adjuste la taille du joueur selon l'input
    /// </summary>
    private void Crouching()
    {
        float desiredHeight = playerInput.Crouching ? crouchHeight : standHeight;

        if (capsuleCollider.height != desiredHeight)
        {
            AdjustHeight(desiredHeight);
        }
    }

    /// <summary>
    /// Adjuste la taille du joueur, la collider, le scale (animation)
    /// </summary>
    /// <param name="height"></param>
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