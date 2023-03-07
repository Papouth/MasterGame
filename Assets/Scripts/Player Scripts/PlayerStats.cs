using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    [Header("SuperForce")]
    public bool haveSuperForce;

    private PlayerPush playerPush;
    private PlayerSuperForce playerSuperForce;


    private void Awake()
    {
        playerPush = GetComponent<PlayerPush>();
        playerSuperForce = GetComponent<PlayerSuperForce>();
    }

    public void GetSuperForce()
    {
        Debug.Log("SuperForce récupéré");
        playerPush.enabled = false;
        playerSuperForce.enabled = true;
        haveSuperForce = true;
    }
}