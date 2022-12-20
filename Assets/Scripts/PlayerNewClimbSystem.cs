using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNewClimbSystem : MonoBehaviour
{
    #region Variables
    [Header("Variables")]
    [SerializeField] private bool useIK;
    [SerializeField] private bool leftHandIK;
    [SerializeField] private bool rightHandIK;

    [Header("Position de la main")]
    [SerializeField] private float leftXOffset;
    [SerializeField] private float rightXOffset;
    [SerializeField] private float ZPosOffset;

    [Header("Rotation de la main")]
    [SerializeField] private float XRotOffset = -15f;
    [SerializeField] private float YRotOffset = 180f;

    public Vector3 leftHandPos;
    public Vector3 rightHandPos;

    public Quaternion leftHandRot;
    public Quaternion rightHandRot;

    private Animator anim;
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        RaycastHit LeftHit;
        RaycastHit RightHit;
        RaycastHit ZPosHit;


        // LeftHandIKCheck
        if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0f, 1f, 0f)), transform.forward + transform.TransformDirection(Vector3.zero), out ZPosHit, 0.5f))
        {
            if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(leftXOffset, 2f, 0.5f)), -transform.up + transform.TransformDirection(new Vector3(-0.5f, 0f, 0f)), out LeftHit, 1f))
            {
                leftHandIK = true;
                leftHandPos = LeftHit.point;
                leftHandPos.z = ZPosHit.point.z + ZPosOffset;

                //leftHandRot = Quaternion.FromToRotation(Vector3.up, transform.forward);

                leftHandRot = Quaternion.Euler(-transform.up + new Vector3(XRotOffset, YRotOffset, 0f));
            }
        }
        else
        {
            leftHandIK = false;
        }


        // RightHandIKCheck
        if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0f, 1f, 0f)), transform.forward + transform.TransformDirection(Vector3.zero), out ZPosHit, 0.5f))
        {
            if (Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(rightXOffset, 2f, 0.5f)), -transform.up + transform.TransformDirection(new Vector3(0.5f, 0f, 0f)), out RightHit, 1f))
            {
                rightHandIK = true;
                rightHandPos = RightHit.point;
                rightHandPos.z = ZPosHit.point.z + ZPosOffset;

                //rightHandRot = Quaternion.FromToRotation(Vector3.up, -transform.up + transform.up);

                rightHandRot = Quaternion.Euler(-transform.up + new Vector3(XRotOffset, YRotOffset, 0f));
            }
        }
        else
        {
            rightHandIK = false;
        }
    }

    private void Update()
    {
        // LeftRay
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(leftXOffset, 2f, 0.5f)), -transform.up + transform.TransformDirection(new Vector3(-0.5f, 0f, 0f)), Color.green);
        // RightRay
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(rightXOffset, 2f, 0.5f)), -transform.up + transform.TransformDirection(new Vector3(0.5f, 0f, 0f)), Color.green);

        // ZPosRay
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0f, 1f, 0f)), transform.forward + transform.TransformDirection(Vector3.zero), Color.cyan);
    }

    private void OnAnimatorIK()
    {
        if (useIK)
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
    }
}