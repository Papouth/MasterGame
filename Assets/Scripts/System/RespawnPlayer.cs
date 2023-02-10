using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : RespawnSystem
{
    #region Customs Methods

    /// <summary>
    /// Cree le respawn des objets au dernier points connu et à jour
    /// </summary>
    public override void Respawn()
    {
        Debug.Log("Respawn Player");

        this.GetComponent<CharacterController>().enabled = false;
        this.gameObject.transform.position = respawnPoint.transform.position;

        this.GetComponent<CharacterController>().enabled = true;
    }

    #endregion
}
