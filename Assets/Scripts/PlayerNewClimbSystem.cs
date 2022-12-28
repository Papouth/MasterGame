using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNewClimbSystem : MonoBehaviour
{
    #region Variables
    [Header("IK")]
    public LayerMask climbLayer;
    [SerializeField] private bool leftHandIK;
    [SerializeField] private bool rightHandIK;

    [Header("Position de la main")]
    [SerializeField] private float leftXOffset;
    [SerializeField] private float rightXOffset;
    [SerializeField] private float yAxisHandsOffset;
    [SerializeField] private float zAxisHandsOffset;
    [SerializeField] private float zAxisOffset;

    [SerializeField] private Vector3 rightPosOffset;
    [SerializeField] private Vector3 leftPosOffset;

    public Vector3 leftHandPos;
    public Vector3 rightHandPos;

    [Header("Rotation de la main")]
    public Quaternion leftHandRot;
    public Quaternion rightHandRot;


    [Header("ClimbStates")]
    public bool isClimbing;
    //public bool climbStateSwitcher;


    [Header("Freeze Position")]
    private Vector3 freezePos = new Vector3(0f, 0f, 0f);
    [SerializeField] private bool frozen;
    private Transform playerTransform;


    [Header("Player Component")]
    private Animator anim;

    private CharacterController cc;

    #endregion

    #region Built In Methods
    private void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        playerTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        HandClimbCheck();
    }

    private void Update()
    {
        OnClimb();

        ShowRay();

        //ClimbState();

        //ClimbMovement();
    }

    private void LateUpdate()
    {
        if (frozen)
        {
            transform.position = new Vector3(transform.position.x, freezePos.y, transform.position.z);
        }
    }
    #endregion


    #region IK Function
    private void HandClimbCheck()
    {
        RaycastHit ZPosHit;

        // ZRayCheck
        if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0f, zAxisOffset, 0f)), transform.forward + transform.TransformDirection(Vector3.zero), out ZPosHit, 0.5f))
        {
            RaycastHit LeftHit;
            RaycastHit RightHit;

            // LeftHandIKCheck
            if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(leftXOffset, yAxisHandsOffset, zAxisHandsOffset)), -transform.up + transform.TransformDirection(new Vector3(-0.5f, 0f, 0f)), out LeftHit, 1f, climbLayer))
            {
                leftHandIK = true;

                leftHandPos = new Vector3(LeftHit.point.x, LeftHit.point.y, ZPosHit.point.z) - transform.TransformDirection(leftPosOffset);

                leftHandRot = Quaternion.LookRotation(transform.forward, LeftHit.normal);
            }
            else
            {
                //leftHandIK = false;
            }

            // RightHandIKCheck
            if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(rightXOffset, yAxisHandsOffset, zAxisHandsOffset)), -transform.up + transform.TransformDirection(new Vector3(0.5f, 0f, 0f)), out RightHit, 1f, climbLayer))
            {
                rightHandIK = true;

                rightHandPos = new Vector3(RightHit.point.x, RightHit.point.y, ZPosHit.point.z) - transform.TransformDirection(rightPosOffset);

                rightHandRot = Quaternion.LookRotation(transform.forward, RightHit.normal);
            }
            else
            {
                //rightHandIK = false;
            }

            if (leftHandIK && rightHandIK)
            {
                isClimbing = true;
            }
        }
        else
        {
            leftHandIK = false;
            rightHandIK = false;

            isClimbing = false;
        }
    }
    #endregion

    private void ColliderModifier()
    {
        // Capsule Collider du joueur ajustement
        cc.height = Mathf.Lerp(cc.height, 1.3f, 0.3f);
        cc.center = Vector3.Lerp(cc.center, new Vector3(0, 0.6f, 0), 0.3f);
    }

    private void OnClimb()
    {
        // Ici on va venir freeze le joueur sur l'axe Y et On va venir empecher la rotation sur cet axe lorsqu'il est en position d'IK
        if (isClimbing)
        {
            playerTransform.position = transform.position;
            freezePos.y = playerTransform.position.y;

            frozen = true;

            // On joue l'animation d'Idle de climb
            anim.SetBool("ClimbBool", true);

            ColliderModifier();
        }
        else
        {
            frozen = false;

            // On arrête l'animation d'Idle de climb
            anim.SetBool("ClimbBool", false);
        }
    }

    /// <summary>
    /// Gère les animations
    /// </summary>
    private void ClimbState()
    {
        /*
        if (leftHandIK || rightHandIK)
        {
            // On bloque la rotation du joueur quand il est accroché
            climbStateSwitcher = true;

            // On peut donc jouer l'animation d'idle de climb
            anim.applyRootMotion = true;
            anim.SetBool("ClimbBool", true);

            // On freeze le joueur
            //FreezePlayer();

            // Capsule Collider du joueur ajustement
            cc.height = Mathf.Lerp(cc.height, 1.25f, 0.3f);
            cc.center = Vector3.Lerp(cc.center, new Vector3(0, 1.3f, 0), 0.3f);


            Debug.Log("On est en IdleClimb");
            // Si une des deux mains est en IK, alors ça veut dire que l'on est en train de grimper
        }

        if (!climbStateSwitcher)
        {
            Debug.Log("ClimbingState en faux car plus de contact avec l'IK");
        }
        */

        if (!leftHandIK && !rightHandIK)
        {
            Invoke("ClimbingStateSecurity", 0.2f);
        }
        /*
        // 1) Le joueur à escaladé la structure
        if (climbStateSwitcher && playerMovement.directionInput.z == 1 && crossState)
        {
            crossState = false;
            groundState = false;
            Debug.Log("On escalade");

            // Fin de l'animation d'Idle Climb
            anim.SetBool("ClimbBool", false);


            // On joue l'animation d'escalade
            anim.SetTrigger("TrClimbCross");


            // On reitre le apply root motion
            StartCoroutine(ClimbStop());
        }

        
        // 2) Le joueur à décider de descendre de la structure
        if (climbStateSwitcher && playerMovement.directionInput.z == -1 && groundState)
        {
            groundState = false;
            crossState = false;
            Debug.Log("On redescend");

            // Fin de l'animation d'Idle Climb
            anim.SetBool("ClimbBool", false);


            // On joue l'animation de drop
            anim.SetTrigger("TrClimbDropGround");


            // On reitre le apply root motion
            StartCoroutine(ClimbStop());
        }
        */
    }

    private void ClimbingStateSecurity()
    {
        //climbStateSwitcher = false;
    }

    /// <summary>
    /// Déplacement en état de climb via l'apply root motion
    /// </summary>
    private void ClimbMovement()
    {
        /*
        if (climbStateSwitcher)
        {
            // Déplacements latéraux
            anim.SetFloat("ClimbMove", playerMovement.directionInput.x);
        }
        */
    }

    /// <summary>
    /// Permet d'enlever le apply root motion de l'animator
    /// </summary>
    /// <returns></returns>
    private IEnumerator ClimbStop()
    {
        yield return new WaitForSeconds(1.2f);

        // On débloque la rotation du joueur car il n'est plus accroché
        //climbStateSwitcher = false;

        anim.applyRootMotion = false;

        // Reset des triggers d'animation
        anim.ResetTrigger("TrClimbCross");
        anim.ResetTrigger("TrClimbDropGround");
    }

    #region IK
    private void OnAnimatorIK()
    {
        if (leftHandIK)
        {
            // Position
            anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPos);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);

            // Rotation
            anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandRot);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        }


        if (rightHandIK)
        {
            // Position
            anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandPos);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);

            // Rotation
            anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandRot);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        }
    }
    #endregion

    #region Visual Debugger
    private void ShowRay()
    {
        // LeftRay
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(leftXOffset, yAxisHandsOffset, zAxisHandsOffset)), -transform.up + transform.TransformDirection(new Vector3(-0.5f, 0f, 0f)), Color.green);
        // RightRay
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(rightXOffset, yAxisHandsOffset, zAxisHandsOffset)), -transform.up + transform.TransformDirection(new Vector3(0.5f, 0f, 0f)), Color.green);

        // ZPosRay
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0f, zAxisOffset, 0f)), transform.forward + transform.TransformDirection(Vector3.zero), Color.cyan);
    }
    #endregion
}