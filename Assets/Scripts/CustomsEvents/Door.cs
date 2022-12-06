using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : CustomsEvent
{
    public Plaque[] plaques;
    private Animator doorAnimator;

    private void Awake()
    {
        doorAnimator = GetComponentInChildren<Animator>();
        doorAnimator.enabled = false;
    }

    public void Update()
    {
        UnlockDoor();
    }

    private bool UnlockDoor()
    {
        for (int i = 0; i < plaques.Length; i++)
        {
            if (plaques[i].valid == false)
            {
                return false;
            }
        }

        doorAnimator.enabled = true;
        return true;
    }
}