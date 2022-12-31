using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTelekinesie : MonoBehaviour
{
    #region Variables
    [Header("Telekinesy Parameters")]
    private bool objectSelected;
    private bool telekinesyOn;
    [SerializeField] private LayerMask telekinesyLayer;
    [SerializeField] private Renderer objectRend;
    [SerializeField] private Material storedMat;
    [SerializeField] private Material selectedMat;


    [Header("Player Component")]
    private PlayerInput playerInput;

    #endregion


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        ObjectDetection();

        ActivateLink();

        InputReset();

        Debug.Log(playerInput.CanTelekinesy);
    }

    private void ObjectDetection()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(playerInput.MousePosition), out hitInfo, Mathf.Infinity, telekinesyLayer))
        {
            objectRend = hitInfo.transform.gameObject.GetComponent<Renderer>();
            storedMat = objectRend.material;


            if (hitInfo.collider.gameObject.layer == telekinesyLayer)
            {
                Debug.Log("ehre");
                // On change le material pour montrer que l'objet peut être selected
                objectRend.material = selectedMat;
            }
            else
            {
                // Reset du material si n'est pas ou plus hit par le raycast
                objectRend.material = storedMat;
            }
        }

    }

    private void ActivateLink()
    {
        if (playerInput.CanTelekinesy && playerInput.CanSelect/* && objectSelected*/)
        {



            telekinesyOn = true;


        }
    }

    private void InputReset()
    {
        if (telekinesyOn)
        {
            telekinesyOn = false;
            playerInput.CanTelekinesy = false;
            playerInput.CanSelect = false;
        }
    }
}