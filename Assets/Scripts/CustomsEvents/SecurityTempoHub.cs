using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecurityTempoHub : MonoBehaviour
{
    private PlayerTemporel playerTempo;


    private void Start()
    {
        playerTempo = FindObjectOfType<PlayerTemporel>();

        Scene unloadScene = SceneManager.GetSceneByName(playerTempo.present);
        GameObject[] goSceneUnload = unloadScene.GetRootGameObjects();
        foreach (var item in goSceneUnload)
        {
            item.SetActive(false);
        }
    }
}