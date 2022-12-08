using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNewClimbSystem : MonoBehaviour
{
    [SerializeField] private bool useIK;
    
    [SerializeField] private bool leftHandIK;
    [SerializeField] private bool rightHandIK;

    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        RaycastHit LeftHit;
        RaycastHit RightHit;


        // LeftHandIKCheck
        if (Physics.Raycast(transform.position + new Vector3(0f, 2f, 0.5f), -transform.up + new Vector3(-0.5f, 0f, 0f), out LeftHit, 1f))
        {
            leftHandIK = true;
        }
        else
        {
            leftHandIK = false;
        }


        // RightHandIKCheck
        if (Physics.Raycast(transform.position + new Vector3(0f, 2f, 0.5f), -transform.up + new Vector3(0.5f, 0f, 0f), out RightHit, 1f))
        {
            rightHandIK = true;
        }
        else
        {
            rightHandIK = false;
        }
    }

    private void Update()
    {
        // LeftRay
        Debug.DrawRay(transform.position + new Vector3(0f, 2f, 0.5f), -transform.up + new Vector3(-0.5f, 0f, 0f), Color.green);
        // RightRay
        Debug.DrawRay(transform.position + new Vector3(0f, 2f, 0.5f), -transform.up + new Vector3(0.5f, 0f, 0f), Color.green);
    }
}