using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    [Header("Player Movement")]
    public float moveSpeed = 3f;
    private Vector3 directionInput;
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
    public float airDrag = .5f;
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


    [Header("Player Component")]
    public Camera cam;
    private CharacterController cc;
    private PlayerInput playerInput;

    #endregion

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

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


        Locomotion();
        Jump();

        if (OnSteepSlope())
        {
            Debug.Log("Here");
            SteepSlopeMovement();
        }

        Crouching();
    }

    #region PlayerMove

    /*
    /// <summary>
    /// Gère le player movement
    /// </summary>
    private void PlayerMove()
    {
        // Rotation du joueur pour qu'il regarde dans la direction où il marche
        Vector3 playerRotation = new Vector3(playerInput.MoveInput.x, 0, playerInput.MoveInput.y);

        if (playerRotation != Vector3.zero)
            transform.rotation = Quaternion.Slerp(
               transform.rotation, Quaternion.LookRotation(playerRotation), 0.15f);


    
        float targetAngle = Mathf.Atan2(playerInput.MoveInput.x, playerInput.MoveInput.y) * Mathf.Rad2Deg +
                Camera.main.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref yVel, .3f);

        var direction = playerInput.MoveInput;
        if (playerInput.MoveInput.magnitude > 0)
        {
            direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        cc.velocity = direction.normalized * (speed * Time.deltaTime);


        // Déplacement du joueur
        playerVelocity = new Vector3(playerInput.MoveInput.x * speed, cc.velocity.y, playerInput.MoveInput.y * speed);
        playerRb.velocity = playerVelocity;
    }*/

    /// <summary>
    /// Gere le deplacement du personnage avec le character controller
    /// </summary>
    void Locomotion()
    {
        if (!playerInput) return;

        directionInput.Set(playerInput.MoveInput.x, 0, playerInput.MoveInput.y);

        if (directionInput.magnitude >= 0.1f)
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
        if (!isGrounded()) return false;

        float indicRaycastCheck = 0;
        foreach (RaycastCheck raycast in raycastsGrounds)
        {
            if (Physics.Raycast(raycast.transform.position, Vector3.down, out slopeHit, raycastLenghtCheck, layersGround))
            {
                indicRaycastCheck++;
            }
        }

        if(indicRaycastCheck > 0)
        {
            float slopeAngle = Vector3.Angle(slopeHit.normal, Vector3.up);
            if (slopeAngle > cc.slopeLimit) return true;
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
    private void Jump()
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
                ySpeed = jumpForce;
                coyoteTime = coyoteTimer;
            }
        }
        movement.y = ySpeed * Time.deltaTime;

        cc.Move(movement);
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


        if (cc.height != desiredHeight && CanStandUp())
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