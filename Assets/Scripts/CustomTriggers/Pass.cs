using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pass : CustomsTriggers
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public override void Interact()
    {
        gameManager.havePass = true;

        Destroy(gameObject);
    }
}