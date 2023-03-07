using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : RespawnSystem
{
    private CharacterController cc;


    #region Customs Methods
    public override void Start()
    {
        base.Start();
        cc = GetComponent<CharacterController>();
    }

    public override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// Cree le respawn des objets au dernier points connu et à jour
    /// </summary>
    public override void Respawn()
    {
        Debug.Log("Respawn Player");

        cc.enabled = false;
        gameObject.transform.position = respawnPoint.transform.position;

        cc.enabled = true;
    }

    #endregion
}