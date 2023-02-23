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


    [Header("Climb Hand Sphere")]
    [SerializeField] private Transform leftPos;
    [SerializeField] private Transform rightPos;
    [SerializeField] private float radiusHand;
    private Collider[] colliderHand = new Collider[1];
    private int leftCount;
    private int rightCount;


    [Header("ClimbStates")]
    public bool isClimbing;
    public bool haveClimbed;


    [Header("Capsule Collider")]
    [SerializeField] private float height = 1.3f;
    [SerializeField] private float center = 0.6f;
    [SerializeField] private float centerZ;


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
    }

    private void LateUpdate()
    {
        if (frozen && !haveClimbed)
        {
            transform.position = new Vector3(transform.position.x, freezePos.y, transform.position.z);
        }
    }
    #endregion


    #region IK Function Check
    private void HandClimbCheck()
    {
        leftCount = Physics.OverlapSphereNonAlloc(leftPos.position, radiusHand, colliderHand, climbLayer);

        rightCount = Physics.OverlapSphereNonAlloc(rightPos.position, radiusHand, colliderHand, climbLayer);


        if (leftCount > 0) leftHandIK = true;
        else leftHandIK = false;


        if (rightCount > 0) rightHandIK = true;
        else rightHandIK = false;


        if (leftHandIK && rightHandIK) isClimbing = true;
        else isClimbing = false;
    }
    #endregion

    private void ColliderModifier()
    {
        // Capsule Collider du joueur ajustement
        cc.height = Mathf.Lerp(cc.height, height, 0.3f);
        cc.center = Vector3.Lerp(cc.center, new Vector3(0, center, centerZ), 0.3f);
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

    #region Visual Debugger
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(leftPos.position, radiusHand);
        Gizmos.DrawWireSphere(rightPos.position, radiusHand);
    }
    #endregion
}