using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCheck : MonoBehaviour
{
    public LayerMask layer;
    public float rangeMax;

    public Vector3 directionRaycast;


    public bool RaycastTest()
    {
        Debug.DrawRay(transform.position, directionRaycast * rangeMax, Color.red, .1f);
        return Physics.Raycast(transform.position, directionRaycast, rangeMax, layer);
    }
}