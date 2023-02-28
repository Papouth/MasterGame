using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AscenceurTp : MonoBehaviour
{
    private PlayerTemporel playerTempo;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.GM.havePass)
        {
            playerTempo = other.GetComponent<PlayerTemporel>();

            SceneManager.UnloadSceneAsync(playerTempo.past);
            SceneManager.UnloadSceneAsync(playerTempo.present);

            playerTempo.past = "Hub_Passe";
            playerTempo.scenesToLoad = playerTempo.past;
            playerTempo.present = "Hub_Present";
            playerTempo.scenesToUnload = playerTempo.present;

            SceneManager.LoadScene(playerTempo.present, LoadSceneMode.Additive);
            SceneManager.LoadScene(playerTempo.past, LoadSceneMode.Additive);
        }
    }
}