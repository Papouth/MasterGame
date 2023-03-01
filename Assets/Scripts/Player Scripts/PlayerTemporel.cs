using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTemporel : MonoBehaviour
{
    #region Variables
    [Header("Scènes")]
    public string scenesToLoad;
    public string scenesToUnload;
    public string past;
    public string present;
    public bool sceneState;


    [Header("Player Component")]
    private PlayerInput playerInput;
    private PlayerInteractor playerInteractor;
    private PlayerStats playerStats;
    #endregion

    #region Built In Methods
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInteractor = GetComponent<PlayerInteractor>();
        playerStats = GetComponent<PlayerStats>();

        if (past == null || present == null) return;

        //SceneManager.LoadScene(past, LoadSceneMode.Additive);
        //SceneManager.LoadScene(present, LoadSceneMode.Additive);
    }

    private void Start()
    {
        //PastSceneAtStart();

        //scenesToLoad = past;
        //scenesToUnload = present;
    }

    private void Update()
    {
        ChangeTempo();
    }
    #endregion

    public void ChangeStringName(string newPast, string newPresent)
    {
        past = newPast;
        present = newPresent;
    }

    public void PastSceneAtStart()
    {
        Scene pastScene = SceneManager.GetSceneByName(past);

        GameObject[] pastUnload = pastScene.GetRootGameObjects();

        foreach (var item in pastUnload)
        {
            item.SetActive(false);
        }
    }

    public void ChangeSceneToLoad(string newSceneToLoad, string newSceneToUnload)
    {
        scenesToLoad = newSceneToLoad;
        scenesToUnload = newSceneToUnload;
    }

    /// <summary>
    /// Change the scene to load/unload when player hit the input
    /// </summary>
    private void ChangeTempo()
    {
        if (playerInput.ChangeTempo && playerInteractor.hands.transform.childCount == 0 && playerStats.haveTempo)
        {
            // On change de temporalit�
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
        else if (playerInput.ChangeTempo && !playerStats.haveTempo) playerInput.ChangeTempo = false;
    }

    /// <summary>
    /// Loading the scenes 
    /// </summary>
    public void LoadingScene()
    {
        Scene scene = SceneManager.GetSceneByName(scenesToLoad);

        GameObject[] goSceneLoad = scene.GetRootGameObjects();
        foreach (var item in goSceneLoad)
        {
            item.SetActive(true);
        }


        Scene unloadScene = SceneManager.GetSceneByName(scenesToUnload);
        GameObject[] goSceneUnload = unloadScene.GetRootGameObjects();
        foreach (var item in goSceneUnload)
        {
            item.SetActive(false);
        }

        /*
        if (!scene.isLoaded)
        {
            SceneManager.LoadScene(scenesToLoad, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(scenesToUnload);
        }
        */
    }
}