using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGrounded : MonoBehaviour
{
    public LayerMask layer;
    public float rangeMaxGrounded;
    public bool RaycastTest()
    {
        RaycastHit raycast;

        Debug.DrawRay(transform.position, Vector3.down * rangeMaxGrounded, Color.red, .1f);
        return Physics.Raycast(transform.position, Vector3.down, .25f,layer);
    }

    
}
