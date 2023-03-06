using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    #region Variables
    [Header("SuperForce")]
    public bool haveSuperForce;
    private PlayerPush playerPush;
    private PlayerSuperForce playerSuperForce;


    [Header("Bracelet Tempo")]
    public bool haveTempo;
    #endregion


    private void Awake()
    {
        playerPush = GetComponent<PlayerPush>();
        playerSuperForce = GetComponent<PlayerSuperForce>();

        playerSuperForce.enabled = false;
    }

    public void GetSuperForce()
    {
        //Debug.Log("SuperForce récupéré");
        playerPush.enabled = false;
        playerSuperForce.enabled = true;
        haveSuperForce = true;
    }

    public void GetBraceletTempo()
    {
        //Debug.Log("Bracelet Tempo récupéré");
        haveTempo = true;
    }
}