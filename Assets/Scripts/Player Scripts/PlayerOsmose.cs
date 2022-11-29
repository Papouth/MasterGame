using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOsmose : MonoBehaviour
{
    private PlayerInput playerInput;
    public bool stateOsmose;


    // Démo Osmose Technologique
    public GameObject[] scenesPrefabs;



    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        UseOsmose();
    }

    private bool UseOsmose()
    {
        if (playerInput.CanOsmose)
        {
            Debug.Log("Utilisation de l'Osmose Technologique en cours !");

            stateOsmose = !stateOsmose;

            if (stateOsmose == true)
            {
                foreach (var prefab in scenesPrefabs)
                {
                    var rendMat = prefab.GetComponent<Renderer>().material;
                    Color color = rendMat.color;
                    color.a = 0.5f;
                    rendMat.color = color;
                }
            }
            else if (stateOsmose == false)
            {
                foreach (var prefab in scenesPrefabs)
                {
                    var rendMat = prefab.GetComponent<Renderer>().material;
                    Color color = rendMat.color;
                    color.a = 1f;
                    rendMat.color = color;
                }
            }

            playerInput.CanOsmose = false;
            return true;
        }

        return false;
    }


}