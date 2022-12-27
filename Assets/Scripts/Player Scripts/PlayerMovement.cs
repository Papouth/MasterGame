using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    [Header("Player Movement")]
    public float moveSpeed = 3f;
    public Vector3 directionInput;
    private Vector3 movement;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity = 0.1f;

    [Header("Player Ground")]
    public RaycastCheck[] raycastsGrounds;
    public LayerMask layersGround;
    public float rangeMaxGrounded;
    public float stepGround = -.1f;

    [Header("SteepSlopeCheck")]
    public float raycastLenghtCheck;
    RaycastHit slopeHit;

    [Header("Player Jump")]
    public float jumpForce = 2f;
    private float ySpeed = 0;
    private float coyoteTime = 0;
    [SerializeField] private float coyoteTimer = .5f;

    [Header("Crouch")]
    public float crouchSpeed = .3f;
    public float standHeight = 2.0f;
    public float crouchHeight = 1.0f;
    public RaycastCheck[] raycastCanStandUp;
    public LayerMask layersCanStandUp;
    public float rangeMaxStandUp = 1.05f;

    [Header("Player Capacity")]

    [Header("Player Component")]
    public Camera cam;
    private CharacterController cc;
    private PlayerInput playerInput;
    public Animator animator;
    private PlayerNewClimbSystem playerNewClimbSystem;

    #endregion

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        playerNewClimbSystem = GetComponent<PlayerNewClimbSystem>();

        foreach (RaycastCheck raycast in raycastsGrounds)
        {
            raycast.layer = layersGround;
            raycast.rangeMax = rangeMaxGrounded;
            raycast.directionRaycast = Vector3.down;
        }

        foreach (RaycastCheck raycast in raycastCanStandUp)
        {
            raycast.layer = layersCanStandUp;
            raycast.rangeMax = rangeMaxStandUp;
            raycast.directionRaycast = Vector3.up;
        }
    }

    private void Update()
    {
        Jump();

        Locomotion();

        if (OnSteepSlope()) SteepSlopeMovement();

        Crouching();

        SetAnimator();
    }

    #region PlayerMove

    /// <summary>
    /// Gere le deplacement du personnage avec le character controller
    /// </summary>
    public void Locomotion()
    {
        if (!playerInput) return;

        directionInput.Set(playerInput.MoveInput.x, 0, playerInput.MoveInput.y);
        //Debug.Log(directionInput.x + directionInput.z);

        if (directionInput.magnitude >= 0.1f && !playerNewClimbSystem.isClimbing)
        {
            float targetAngle = Mathf.Atan2(directionInput.x, directionInput.z) * Mathf.Rad2Deg +
                cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
                ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            directionInput = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        movement = directionInput.normalized * (moveSpeed * Time.deltaTime);
    }

    private bool OnSteepSlope()
    {
        float indicRaycastCheck = 0;
        foreach (RaycastCheck raycast in raycastsGrounds)
        {
            if (Physics.Raycast(raycast.transform.position, Vector3.down, out slopeHit, raycastLenghtCheck, layersGround))
            {
                Debug.DrawLine(raycast.transform.position, Vector3.down * raycastLenghtCheck + raycast.transform.position, Color.blue);
                float slopeAngle = Vector3.Angle(slopeHit.normal, Vector3.up);

                if (slopeAngle > cc.slopeLimit)

                    indicRaycastCheck++;
            }
        }

        if (indicRaycastCheck > 0)
        {
            return true;
        }
        return false;
    }

    private void SteepSlopeMovement()
    {
        Vector3 slopeDirection = Vector3.up - slopeHit.normal * Vector3.Dot(Vector3.up, slopeHit.normal);
        float slideSpeed = moveSpeed + Time.deltaTime;

        movement = slopeDirection * -slideSpeed;
        movement.y = movement.y - slopeHit.point.y;

        cc.Move(movement * Time.deltaTime);
    }

    #endregion

    #region PlayerJump

    /// <summary>
    /// activation du jump
    /// </summary>
    public void Jump()
    {
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (isGrounded())
        {
            //Fonction Check Step Slope ground Return bool
            //=> Fonction Check step slope void 


            if (ySpeed <= stepGround)
            {
                ySpeed = -0.2f;
            }

            if (playerInput.CanJump)
            {
                // Animator Jump
                animator.SetTrigger("TrJump");

                ySpeed = jumpForce;

                coyoteTime = coyoteTimer;
            }

        }

        movement.y = ySpeed * Time.deltaTime;

        cc.Move(movement);
        animator.ResetTrigger("TrJump");
    }

    /// <summary>
    /// Check si le joueur est au sol
    /// </summary>
    /// <returns></returns>
    public bool isGrounded()
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
                // On empeche le joueur de re-sauter instantanement au contact du sol
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

        if (playerInput.Crouching)
            animator.SetBool("Crouch", true);

        else if (!playerInput.Crouching)
            animator.SetBool("Crouch", false);


        if (cc.height != desiredHeight && CanStandUp()/* && !playerNewClimbSystem.climbStateSwitcher*/)
        {
            AdjustHeight(desiredHeight);
        }
    }

    private bool CanStandUp()
    {
        if (cc.height == standHeight)
        {
            // Sinon on autorise a s'accroupir
            return true;
        }
        else
        {
            // On check si le joueur est accroupis
            int raycastGood = 0;


            foreach (RaycastCheck raycast in raycastCanStandUp)
                if (raycast.RaycastTest()) raycastGood++;


            if (raycastGood > 0) return false;
            else return true;
        }
    }

    /// <summary>
    /// Adjuste la taille du joueur, la collider, le scale (animation)
    /// </summary>
    /// <param name="height"></param>
    private void AdjustHeight(float height)
    {
        float center = height / 2;


        cc.height = Mathf.Lerp(cc.height, height, crouchSpeed);
        cc.center = Vector3.Lerp(cc.center, new Vector3(0, center, 0), crouchSpeed);
    }

    #endregion

    private void SetAnimator()
    {
        animator.SetFloat("Movement", directionInput.magnitude);
    }

    private void OnDrawGizmos()
    {
        foreach (RaycastCheck raycast in raycastsGrounds)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(raycast.transform.position, raycast.transform.position + Vector3.down * rangeMaxGrounded);
        }

        foreach (RaycastCheck raycast in raycastCanStandUp)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(raycast.transform.position, raycast.transform.position + Vector3.up * rangeMaxStandUp);
        }
    }
}