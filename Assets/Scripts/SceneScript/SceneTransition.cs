using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string scenesToLoad;
    public string scenesToUnload;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            Debug.Log("Here");
            LoadingScene();
        }
    }

    /// <summary>
    /// Loading the scenes 
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