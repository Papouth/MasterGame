using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    #region Variable

    [SerializeField] private LayerMask respawnObject;
    [HideInInspector] public GameObject respawnPoint;
    [SerializeField] private float timeUpdate = 3;

    private float timer;
    #endregion


    #region Built In methods

    public virtual void Start()
    {
        respawnPoint = new GameObject("Respawn_" + name);
    }

    public virtual void Update()
    {
        MajRespawnPoint();
    }

    #endregion

    #region Customs Methods

    /// <summary>
    /// Check if it's grounded
    /// </summary>
    /// <returns></returns>
    public bool CheckGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 2f, respawnObject))
        {
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Maj le point de respawn pour qu'il soit pas trop trop loins du joueurs
    /// </summary>
    public void MajRespawnPoint()
    {
        timer += Time.deltaTime;

        if (CheckGrounded() && timer >= timeUpdate)
        {
            timer = 0;
            respawnPoint.transform.position = transform.position;
        }
    }

    /// <summary>
    /// Cree le respawn des objets au dernier points connu et à jour
    /// </summary>
    public virtual void Respawn()
    {
        Debug.Log("Respawn");
        gameObject.transform.position = respawnPoint.transform.position;
    }

    #endregion
}