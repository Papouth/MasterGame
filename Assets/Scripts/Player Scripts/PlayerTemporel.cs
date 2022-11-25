using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTemporel : MonoBehaviour
{
    [Header("Scènes")]
    [SerializeField] private string scenesToLoad;
    [SerializeField] private string scenesToUnload;
    private string past = "Passé";
    private string present = "Présent";

    private bool sceneState;


    [Header("Player Component")]
    private PlayerInput playerInput;
    private CharacterController cc;


    #region Built In Methods
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        cc = GetComponent<CharacterController>();

        scenesToLoad = "Passé";
        scenesToUnload = "Présent";
    }

    private void Update()
    {
        ChangeTempo();
    }
    #endregion


    /// <summary>
    /// Change the scene to load/unload when player hit the input to change scenes
    /// </summary>
    private void ChangeTempo()
    {
        if (playerInput.ChangeTempo)
        {
            Debug.Log("Tempo");



            // On change de temporalité
            LoadingScene();

            sceneState = !sceneState;

            // Une fois changé de tempo on inverse les scènes
            if (sceneState)
            {
                // On est dans le passé
                scenesToLoad = present;
                scenesToUnload = past;
            }
            else if (!sceneState)
            {
                // On est dans le présent
                scenesToLoad = past;
                scenesToUnload = present;
            }


            playerInput.ChangeTempo = false;
        }
    }

    /// <summary>
    /// Loading the scenes of the 
    /// </summary>
    private void LoadingScene()
    {
        Scene scene = SceneManager.GetSceneByName(scenesToLoad);

        if (!scene.isLoaded)
        {
            SceneManager.LoadScene(scenesToLoad, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(scenesToUnload);
        }
    }
}