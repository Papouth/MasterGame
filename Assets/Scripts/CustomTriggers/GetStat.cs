using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GetStat : CustomsTriggers
{
    public UnityEvent statEvent;
    private bool statState;
    public Animation statAnim;


    private PlayerStats playerStats;
    private bool unlockScript;

    [Tooltip("A cocher si c'est pour avoir le bracelet temporel")]
    [SerializeField] private bool tempoExternPlayer;



    private void Update()
    {
        TempoException();
    }

    public override void Interact()
    {
        if (!statState && statEvent != null) statEvent.Invoke();

        return;
    }

    public void AnimSuperForce()
    {
        // On joue l'animation de recuperation de la super force
        statState = true;
        //statAnim.Play();
    }

    public void AnimBraceletTempo()
    {
        // On joue l'animation de recuperation du bracelet de tempo
        statState = true;
        //statAnim.Play();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !unlockScript)
        {
            playerStats = other.GetComponent<PlayerStats>();
            unlockScript = true;
        }
    }

    private void TempoException()
    {
        if (tempoExternPlayer && unlockScript)
        {
            tempoExternPlayer = false;
            statEvent = new UnityEvent();
            statEvent.AddListener(playerStats.GetBraceletTempo);
        }
    }
}