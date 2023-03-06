using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSecurity : MonoBehaviour
{
    private Animator doorAnimator;


    private void Start()
    {
        doorAnimator = GetComponentInParent<Animator>();
    }

    private void Close()
    {
        doorAnimator.SetBool("IsValid", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) Close();
    }
}