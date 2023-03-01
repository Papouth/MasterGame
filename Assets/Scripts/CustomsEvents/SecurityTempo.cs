using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecurityTempo : MonoBehaviour
{
    private PlayerTemporel playerTempo;
    [Tooltip("La scène a masquer quan celle ci apparait \n True = Passé \n False = Présent")]
    public bool past;


    private void Start()
    {
        playerTempo = FindObjectOfType<PlayerTemporel>();

        if (!past)
        {
            Scene unloadScene = SceneManager.GetSceneByName(playerTempo.present);
            GameObject[] goSceneUnload = unloadScene.GetRootGameObjects();
            foreach (var item in goSceneUnload)
            {
                item.SetActive(false);
            }
        }
        else if (past)
        {
            Scene pastScene = SceneManager.GetSceneByName(playerTempo.past);

            GameObject[] pastUnload = pastScene.GetRootGameObjects();

            foreach (var item in pastUnload)
            {
                item.SetActive(false);
            }
        }
    }
}