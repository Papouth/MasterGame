using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTelekinesie : MonoBehaviour
{
    #region Variables
    [Header("Telekinesy Parameters")]
    private bool telekinesyOn;
    public static GameObject telekinesyObject;


    [Header("Player Component")]
    private PlayerInput playerInput;

    #endregion


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        ActivateLink();

        InputReset();

        // Montrer via de l'UI que la télékinésie est activé
        //Debug.Log(playerInput.CanTelekinesy);
    }

    private void ActivateLink()
    {
        if (playerInput.CanTelekinesy && telekinesyObject != null)
        {
            telekinesyOn = true;
            Debug.Log("TELEKINESY ON");
        }
    }

    private void InputReset()
    {
        if (telekinesyOn)
        {
            telekinesyOn = false;
            playerInput.CanTelekinesy = false;
            PlayerInput.telekinesyKeyOn = false;
        }
    }
}