using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plaque : MonoBehaviour
{
    public LayerMask cubeColor;
    public bool valid;

    public void Update()
    {
        RayChecker();
    }

    public void RayChecker()
    {
        Debug.DrawRay(transform.position, Vector3.up, Color.blue);

        if (Physics.Raycast(transform.position, Vector3.up, 0.2f, cubeColor))
        {
            valid = true;
        }
        else
        {
            valid = false;
        }
    }
    
}