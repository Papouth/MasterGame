using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    #region Variables
    [Header("Player Push")]
    public Rigidbody rbCol;
    public RaycastCheck[] raycastPush;
    public float rangeMaxPush = 0.4f;
    public LayerMask layersCanPush;

    private Rigidbody hitGO;

    [Header("Player Component")]
    protected CharacterController cc;
    protected Animator animator;
    protected PlayerMovement playerMovement;
    #endregion


    public virtual void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        foreach (RaycastCheck raycastCheck in raycastPush)
        {
            raycastCheck.layer = layersCanPush;
            raycastCheck.rangeMax = rangeMaxPush;
            raycastCheck.directionRaycast = transform.forward;
        }
    }

    public virtual void Update()
    {
        PushAnimator();
    }

    #region Push
    public virtual void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!enabled) return;

        rbCol = hit.collider.attachedRigidbody;

        if (rbCol != null && !rbCol.isKinematic && CanPush())
        {
            // On joue l'animation pour pousser qui correspond au poids de l'objet
            if (rbCol.mass < 12)
            {
                animator.SetBool("LowPush", true);

                // On règle à la bonne vitesse
                rbCol.velocity = new Vector3(hit.moveDirection.x * 2, 0, hit.moveDirection.z * 2);
            }
            else if (rbCol.mass >= 12 && rbCol.mass < 22)
            {
                animator.SetBool("MediumPush", true);

                // On règle à la bonne vitesse
                rbCol.velocity = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            }
        }
    }

    public virtual bool CanPush()
    {
        // Rajouter un Check avec un raycast
        int raycastPushGood = 0;
        foreach (RaycastCheck raycast in raycastPush)
        {
            raycast.directionRaycast = transform.forward;

            if (raycast.RaycastTest()) raycastPushGood++;
        }

        if (raycastPushGood == 1)
            return true;
        else
            return false;
    }

    public virtual void PushAnimator()
    {
        if (playerMovement.directionInput.magnitude == 0 || !CanPush())
        {
            animator.SetBool("LowPush", false);
            animator.SetBool("MediumPush", false);
            animator.SetBool("HardPush", false);
        }
    }
    #endregion


    public virtual void OnDrawGizmos()
    {
        foreach (RaycastCheck raycast in raycastPush)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(raycast.transform.position, raycast.transform.position + transform.forward * rangeMaxPush);
        }
    }
}