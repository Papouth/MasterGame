using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSuperForce : PlayerPush
{
    public float superForceMultiplier = 1.5f;


    public override void Awake()
    {
        base.Awake();
    }

    public override void Update()
    {
        base.Update();
    }

    #region Push
    public override void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!this.enabled) return;
        rbCol = hit.collider.attachedRigidbody;

        if (rbCol.mass < 100)
        {
            rbCol.isKinematic = false;
        }

        if (rbCol != null && !rbCol.isKinematic)
        {
            // On joue l'animation pour pousser qui correspond au poids de l'objet
            if (rbCol.mass < 12)
            {
                animator.SetBool("LowPush", true);

                rbCol.velocity = hit.moveDirection * 2 * superForceMultiplier;
            }
            else if (rbCol.mass >= 12 && rbCol.mass < 22)
            {
                animator.SetBool("LowPush", true);

                rbCol.velocity = hit.moveDirection * superForceMultiplier;
            }
            else if (rbCol.mass >= 22 && rbCol.mass < 32)
            {
                animator.SetBool("HardPush", true);

                rbCol.velocity = hit.moveDirection * 0.5f;
            }
        }
    }

    public override bool CanPush()
    {
        base.CanPush();
        return false;
    }

    public override void PushAnimator()
    {
        base.PushAnimator();
    }
    #endregion

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}