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
    public bool sceneState;


    [Header("Player Component")]
    private PlayerInput playerInput;


    #region Built In Methods
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        if(past == null || present == null) return;
        
        SceneManager.LoadScene(past, LoadSceneMode.Additive);
        SceneManager.LoadScene(present, LoadSceneMode.Additive);

        PastSceneAtStart();

        scenesToLoad = past;
        scenesToUnload = present;

    }

    private void Update()
    {
        ChangeTempo();
    }
    #endregion

    private void PastSceneAtStart()
    {
        Scene pastScene = SceneManager.GetSceneByName(past);

        GameObject[] pastUnload = pastScene.GetRootGameObjects();
        foreach (var item in pastUnload)
        {
            item.SetActive(false);
        }
    }


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
    /// Loading the scenes 
    /// </summary>
    private void LoadingScene()
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