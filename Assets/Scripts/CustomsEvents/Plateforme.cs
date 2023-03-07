using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateforme : MonoBehaviour
{
    #region Variables

    [Header("States")]
    [Tooltip("True = Bouge seul \n False = Bouge par levier \n ne pas oublier de renseigner un levier")]
    [SerializeField]
    private bool fullMove;
    private bool canMove;

    [Header("Lerp & Speed")]
    private float currentTimeLerp;
    [SerializeField]
    private float TimeToReach;

    [Header("WayPoint & Component")]
    [SerializeField]
    private Vector3 depart;
    [SerializeField]
    private Transform finish;
    private Vector3 destination;

    [Header("Waiting Time")]
    [SerializeField] private float timerWaiting = 1;

    [Header("Avec Levier")]
    //[Tooltip("Bool qui doit être mis sur true par un levier")]
    [HideInInspector] public bool isEnable;

    private Rigidbody rb;
    private Vector3 currentPos;
    private CharacterController cc;
    #endregion

    #region Built In methods

    void Start()
    {
        depart = destination = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        EditPlateforme();
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        //Securité
        StopAllCoroutines();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) cc = other.GetComponent<CharacterController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) cc.Move(rb.velocity * Time.deltaTime);
    }

    #endregion

    #region Customs Methods

    /// <summary>
    /// Gere les mouvements de la plateforme si elle bougent toute seul ou par levier
    /// </summary>
    private void EditPlateforme()
    {
        if (depart == null || finish == null) { Debug.Log("Pas de depart ni de finish"); return; }

        if (fullMove)
        {
            if (DestinationReached())
            {
                StartCoroutine(WaitingTime());
            }
        }
        else
        {
            if (isEnable)
            {
                StartCoroutine(WaitingTime());
                //isEnable = false;
            }
        }
        if (canMove)
            MovePlateforme();
    }

    /// <summary>
    /// Deplace la plateforme
    /// </summary>
    private void MovePlateforme()
    {
        currentTimeLerp += Time.deltaTime * TimeToReach / 1000;
        currentPos = Vector3.Lerp(transform.position, destination, currentTimeLerp);
        rb.MovePosition(currentPos);

        if (DestinationReached())
        {
            canMove = false;
        }
    }

    /// <summary>
    /// choisi la destination de la plateforme
    /// </summary>
    /// <returns></returns>
    private Vector3 SelectDestination()
    {
        if (Vector3.Distance(transform.position, destination) <= .1f)
        {
            currentTimeLerp = 0;
            canMove = false;

            if (Mathf.Approximately(destination.magnitude, depart.magnitude))
                return finish.position;
            else if (Mathf.Approximately(destination.magnitude, finish.position.magnitude))
                return depart;
            else return destination;

        }
        else return destination;
    }

    private bool DestinationReached()
    {
        return Vector3.Distance(transform.position, destination) <= .1f;
    }

    /// <summary>
    /// Timer que la plateforme attend pour continuer
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitingTime()
    {
        destination = SelectDestination();
        yield return new WaitForSeconds(timerWaiting);

        canMove = true;
    }

    /// <summary>
    /// Attend qu'un levier soit activer.
    /// Fonction appeler par un levier
    /// </summary>
    public void ActivateLevier()
    {
        isEnable = true;
    }

    #endregion
}