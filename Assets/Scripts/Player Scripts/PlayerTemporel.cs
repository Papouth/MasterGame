using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTemporel : MonoBehaviour
{
    [Header("Sc�nes")]
    [SerializeField] private string scenesToLoad;
    [SerializeField] private string scenesToUnload;
    public string past = "Pass�";
    public string present = "Pr�sent";
    private bool sceneState;

    [Header("Player Component")]
    private PlayerInput playerInput;


    #region Built In Methods
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        scenesToLoad = past;
        scenesToUnload = present;
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
            // On change de temporalit�
            LoadingScene();

            sceneState = !sceneState;

            // Une fois chang� de tempo on inverse les sc�nes
            if (sceneState)
            {
                // On est dans le pass�
                scenesToLoad = present;
                scenesToUnload = past;
            }
            else if (!sceneState)
            {
                // On est dans le pr�sent
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