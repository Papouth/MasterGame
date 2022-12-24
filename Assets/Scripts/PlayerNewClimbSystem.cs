using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNewClimbSystem : MonoBehaviour
{
    #region Variables
    [Header("Variables")]
    [SerializeField] private bool leftHandIK;
    [SerializeField] private bool rightHandIK;

    public bool climbStateSwitcher;
    [SerializeField] private bool crossState;
    [SerializeField] private bool groundState;

    [Header("Freeze Position")]
    private Vector3 freezePos = new Vector3(0f, 0f, 0f);
    private bool frozen;
    private Transform playerTransform;

    [Header("Position de la main")]
    [SerializeField] private float leftXOffset;
    [SerializeField] private float rightXOffset;

    [SerializeField] private Vector3 rightPosOffset;
    [SerializeField] private Vector3 leftPosOffset;

    public Vector3 leftHandPos;
    public Vector3 rightHandPos;


    [Header("Rotation de la main")]
    public Quaternion leftHandRot;
    public Quaternion rightHandRot;

    [Header("Player Component")]
    private Animator anim;
    private PlayerMovement playerMovement;
    private CharacterController cc;

    #endregion

    #region Built In Methods
    private void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        cc = GetComponent<CharacterController>();
        playerTransform = GetComponent<Transform>();

        // Bool�ens permettant de v�rifier que l'action s'�x�cute une seule et unique fois
        crossState = true;
        groundState = true;
    }

    private void FixedUpdate()
    {
        HandClimbCheck();

        if (frozen)
        {
            //Debug.Log("AVANT" + transform.position.y);

            transform.position = new Vector3(transform.position.x, freezePos.y, transform.position.z);

            //Debug.Log("APRES" + transform.position.y);
        }
    }

    private void Update()
    {
        /*
        if (!playerMovement.isGrounded())
            ShowRay();
        */
        
        ShowRay();


        if (frozen)
        {
            //Debug.Log("AVANT" + transform.position.y);

            transform.position = new Vector3(transform.position.x, freezePos.y, transform.position.z);

            //Debug.Log("APRES" + transform.position.y);
        }

        ClimbState();

        ClimbMovement();
    }

    private void LateUpdate()
    {
        if (frozen)
        {
            //Debug.Log("AVANT" + transform.position.y);

            transform.position = new Vector3(transform.position.x, freezePos.y, transform.position.z);

            //Debug.Log("APRES" + transform.position.y);
        }
    }
    #endregion

    private void FreezePlayer()
    {
        /*
        var myPos = transform.position;
        myPos.y = transform.position.y;

        freezePos.y = myPos.y;
        */


        //freezePos = transform.position;

        playerTransform.position = transform.position;
        freezePos.y = playerTransform.position.y;


        Debug.Log("position de freeze" + freezePos.y);
        frozen = true;
    }


    private void HandClimbCheck()
    {
        RaycastHit LeftHit;
        RaycastHit RightHit;
        RaycastHit ZPosHit;


        // LeftHandIKCheck
        if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0f, 1.7f, 0f)), transform.forward + transform.TransformDirection(Vector3.zero), out ZPosHit, 0.5f) && Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(leftXOffset, 2f, 0.3f)), -transform.up + transform.TransformDirection(new Vector3(-0.5f, 0f, 0f)), out LeftHit, 1f))
        {
            /*
            // On v�rifie que le joueur n'est pas au sol
            if (!playerMovement.isGrounded())
            {
                leftHandIK = true;

                leftHandPos = new Vector3(LeftHit.point.x, LeftHit.point.y, ZPosHit.point.z) - transform.TransformDirection(leftPosOffset);

                leftHandRot = Quaternion.LookRotation(transform.forward, LeftHit.normal);
            }
            */
            
            leftHandIK = true;

            leftHandPos = new Vector3(LeftHit.point.x, LeftHit.point.y, ZPosHit.point.z) - transform.TransformDirection(leftPosOffset);

            leftHandRot = Quaternion.LookRotation(transform.forward, LeftHit.normal);
        }
        else
        {
            leftHandIK = false;
        }


        // RightHandIKCheck
        if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0f, 1.7f, 0f)), transform.forward + transform.TransformDirection(Vector3.zero), out ZPosHit, 0.5f) && Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(rightXOffset, 2f, 0.3f)), -transform.up + transform.TransformDirection(new Vector3(0.5f, 0f, 0f)), out RightHit, 1f))
        {
            /*
            // On v�rifie que le joueur n'est pas au sol
            if (!playerMovement.isGrounded())
            {
                rightHandIK = true;

                rightHandPos = new Vector3(RightHit.point.x, RightHit.point.y, ZPosHit.point.z) - transform.TransformDirection(rightPosOffset);

                rightHandRot = Quaternion.LookRotation(transform.forward, RightHit.normal);
            }
            */

            rightHandIK = true;

            rightHandPos = new Vector3(RightHit.point.x, RightHit.point.y, ZPosHit.point.z) - transform.TransformDirection(rightPosOffset);

            rightHandRot = Quaternion.LookRotation(transform.forward, RightHit.normal);
        }
        else
        {
            rightHandIK = false;
        }
    }

    /// <summary>
    /// G�re les animations
    /// </summary>
    private void ClimbState()
    {
        if (leftHandIK || rightHandIK)
        {
            // On bloque la rotation du joueur quand il est accroch�
            climbStateSwitcher = true;

            // On peut donc jouer l'animation d'idle de climb
            anim.applyRootMotion = true;
            anim.SetBool("ClimbBool", true);

            // On freeze le joueur
            FreezePlayer();
            //playerFreeze = true;

            // Capsule Collider du joueur ajustement
            cc.height = Mathf.Lerp(cc.height, 1.25f, 0.3f);
            cc.center = Vector3.Lerp(cc.center, new Vector3(0, 1.3f, 0), 0.3f);


            Debug.Log("On est en IdleClimb");
            // Si une des deux mains est en IK, alors �a veut dire que l'on est en train de grimper
        }

        if (!climbStateSwitcher)
        {
            Debug.Log("ClimbingState en faux car plus de contact avec l'IK");
        }

        if (!leftHandIK && !rightHandIK/* && climbStateSwitcher*/)
        {
            Invoke("ClimbingStateSecurity", 0.2f);
        }

        // 1) Le joueur � escalad� la structure
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


        // 2) Le joueur � d�cider de descendre de la structure
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
    }

    private void ClimbingStateSecurity()
    {
        climbStateSwitcher = false;
        frozen = false;
    }

    /// <summary>
    /// D�placement en �tat de climb via l'apply root motion
    /// </summary>
    private void ClimbMovement()
    {
        if (climbStateSwitcher)
        {
            // D�placements lat�raux
            anim.SetFloat("ClimbMove", playerMovement.directionInput.x);
        }
    }

    /// <summary>
    /// Permet d'enlever le apply root motion de l'animator
    /// </summary>
    /// <returns></returns>
    private IEnumerator ClimbStop()
    {
        yield return new WaitForSeconds(1.2f);

        groundState = true;
        crossState = true;

        // On d�bloque la rotation du joueur car il n'est plus accroch�
        climbStateSwitcher = false;
        frozen = false;

        anim.applyRootMotion = false;

        // Reset des triggers d'animation
        anim.ResetTrigger("TrClimbCross");
        anim.ResetTrigger("TrClimbDropGround");
    }

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

    #region Visual Debugger
    private void ShowRay()
    {
        // LeftRay
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(leftXOffset, 2f, 0.3f)), -transform.up + transform.TransformDirection(new Vector3(-0.5f, 0f, 0f)), Color.green);
        // RightRay
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(rightXOffset, 2f, 0.3f)), -transform.up + transform.TransformDirection(new Vector3(0.5f, 0f, 0f)), Color.green);

        // ZPosRay
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0f, 1.7f, 0f)), transform.forward + transform.TransformDirection(Vector3.zero), Color.cyan);
    }
    #endregion
}