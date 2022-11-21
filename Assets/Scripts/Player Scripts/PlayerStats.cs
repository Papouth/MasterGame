using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    [Header("Push")]
    private PlayerPush playerPush;
    private PlayerSuperForce playerSuperForce;


    private void Awake()
    {
        playerPush = GetComponent<PlayerPush>();
        playerSuperForce = GetComponent<PlayerSuperForce>();
    }

    public void GetSuperForce()
    {
        playerPush.enabled = false;
        playerSuperForce.enabled = true;
    }
}